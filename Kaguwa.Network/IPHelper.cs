using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Kaguwa.Network.Enums;
using Kaguwa.Network.Structs;
using Kaguwa.Network.Types;

namespace Kaguwa.Network
{
    // This class was written and greatly inspired (taken in full length) from
    // https://code.msdn.microsoft.com/windowsdesktop/C-Sample-to-list-all-the-4817b58f
    // and was made by the OneCode Team but has been modified to remove all the GUI/Form parts.
    // I take no credit on it what so ever.
    // I did have to do some research on how to implement IPv6.
    // http://pinvoke.net/default.aspx/Structures/MIB_TCP6ROW_OWNER_PID.html
    // http://pinvoke.net/default.aspx/Structures/MIB_TCP6TABLE_OWNER_PID.html
    // http://pinvoke.net/default.aspx/iphlpapi/GetExtendedTcpTable.html
    // These pages provided me with the knowledge to also do the same for UDP.

    /// <summary>
    /// Class with static methods to call the system with iphlpapi.dll for information
    /// about network connections.
    /// </summary>
    public class IPHelper
    {
        // Version of IP used. 
        private const int AF_INET = 2;
        private const int AF_INET6 = 23;

        // Import external helper DLLs and methods.
        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize,
            bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize,
            bool bOrder, int ulAf, UdpTableClass tableClass, uint reserved = 0);

        /// <summary>
        /// This function reads and parses the active TCP socket connections available.
        /// </summary>
        /// <returns>
        /// It returns the current set of TCP socket connections which are active.
        /// </returns>
        /// <exception cref="OutOfMemoryException">
        /// This exception may be thrown by the function Marshal.AllocHGlobal when
        /// there is insufficient memory to satisfy the request.
        /// </exception>
        public static List<TcpConnection> GetTcpConnections(IPVersion ipVersion, Process[] processes = null)
        {
            int bufferSize = 0;
            List<TcpConnection> tcpTableRecords = new List<TcpConnection>();

            int ulAf = AF_INET;

            if (ipVersion == IPVersion.IPv6)
            {
                ulAf = AF_INET6;
            }

            // Getting the initial size of TCP table.
            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, ulAf,
                TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

            // Allocating memory as an IntPtr with the bufferSize.
            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // The IntPtr from last call, tcpTableRecoresPtr must be used in the subsequent
                // call and passed as the first parameter.
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true,
                    ulAf, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                // If not zero, the call failed.
                if (result != 0)
                    return new List<TcpConnection>();

                // Marshals data fron an unmanaged block of memory to the
                // newly allocated managed object 'tcpRecordsTable' of type
                // 'MIB_TCPTABLE_OWNER_PID' to get number of entries of TCP
                // table structure.

                // Determine if IPv4 or IPv6.
                if (ipVersion == IPVersion.IPv4)
                {
                    MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)
                        Marshal.PtrToStructure(tcpTableRecordsPtr, typeof(MIB_TCPTABLE_OWNER_PID));

                    IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr +
                                            Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

                    // Read and parse the TCP records from the table and store them in list 
                    // 'TcpConnection' structure type objects.
                    for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                    {
                        MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.
                            PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));
                        // Add row to list of TcpConnetions.
                        tcpTableRecords.Add(new TcpConnection(
                                                Protocol.TCP,
                                                new IPAddress(tcpRow.localAddr),
                                                new IPAddress(tcpRow.remoteAddr),
                                                BitConverter.ToUInt16(new byte[2] {
                                                tcpRow.localPort[1],
                                                tcpRow.localPort[0] }, 0),
                                                BitConverter.ToUInt16(new byte[2] {
                                                tcpRow.remotePort[1],
                                                tcpRow.remotePort[0] }, 0),
                                                tcpRow.owningPid,
                                                tcpRow.state,
                                                processes));
                        tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                    }
                }
                else if (ipVersion == IPVersion.IPv6)
                {
                    MIB_TCP6TABLE_OWNER_PID tcpRecordsTable = (MIB_TCP6TABLE_OWNER_PID)
                        Marshal.PtrToStructure(tcpTableRecordsPtr, typeof(MIB_TCP6TABLE_OWNER_PID));

                    IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr +
                                            Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

                    // Read and parse the TCP records from the table and store them in list 
                    // 'TcpConnection' structure type objects.
                    for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                    {
                        MIB_TCP6ROW_OWNER_PID tcpRow = (MIB_TCP6ROW_OWNER_PID)Marshal.
                            PtrToStructure(tableRowPtr, typeof(MIB_TCP6ROW_OWNER_PID));

                        tcpTableRecords.Add(new TcpConnection(
                                                Protocol.TCP,
                                                new IPAddress(tcpRow.localAddr, tcpRow.localScopeId),
                                                new IPAddress(tcpRow.remoteAddr, tcpRow.remoteScopeId),
                                                BitConverter.ToUInt16(new byte[2] {
                                                tcpRow.localPort[1],
                                                tcpRow.localPort[0] }, 0),
                                                BitConverter.ToUInt16(new byte[2] {
                                                tcpRow.remotePort[1],
                                                tcpRow.remotePort[0] }, 0),
                                                tcpRow.owningPid,
                                                tcpRow.state,
                                                processes));
                        tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                    }
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                throw outOfMemoryException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }

            return tcpTableRecords != null ? tcpTableRecords.Distinct()
                .ToList() : new List<TcpConnection>();
        }

        /// <summary>
        /// This function reads and parses the active UDP socket connections available.
        /// </summary>
        /// <returns>
        /// It returns the current set of TCP socket connections which are active.
        /// </returns>
        /// <exception cref="OutOfMemoryException">
        /// This exception may be thrown by the function Marshal.AllocHGlobal when
        /// there is insufficient memory to satisfy the request.
        /// </exception>
        public static List<UdpConnection> GetUdpConnections(IPVersion ipVersion, Process[] processes = null)
        {
            int bufferSize = 0;
            List<UdpConnection> udpTableRecords = new List<UdpConnection>();

            int ulAf = AF_INET;

            if (ipVersion == IPVersion.IPv6)
            {
                ulAf = AF_INET6;
            }

            // Getting the initial size of UDP table.
            uint result = GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, true,
                ulAf, UdpTableClass.UDP_TABLE_OWNER_PID);

            // Allocating memory as an IntPtr with the bufferSize.
            IntPtr udpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // The IntPtr from last call, udpTableRecoresPtr must be used in the subsequent
                // call and passed as the first parameter.
                result = GetExtendedUdpTable(udpTableRecordsPtr, ref bufferSize, true,
                    ulAf, UdpTableClass.UDP_TABLE_OWNER_PID);

                // If not zero, call failed.
                if (result != 0)
                    return new List<UdpConnection>();

                // Marshals data fron an unmanaged block of memory to the
                // newly allocated managed object 'udpRecordsTable' of type
                // 'MIB_UDPTABLE_OWNER_PID' to get number of entries of TCP
                // table structure.

                // Determine if IPv4 or IPv6.
                if (ipVersion == IPVersion.IPv4)
                {
                    MIB_UDPTABLE_OWNER_PID udpRecordsTable = (MIB_UDPTABLE_OWNER_PID)
                        Marshal.PtrToStructure(udpTableRecordsPtr, typeof(MIB_UDPTABLE_OWNER_PID));
                    IntPtr tableRowPtr = (IntPtr)((long)udpTableRecordsPtr +
                        Marshal.SizeOf(udpRecordsTable.dwNumEntries));

                    // Read and parse the UDP records from the table and store them in list 
                    // 'UdpConnection' structure type objects.
                    for (int i = 0; i < udpRecordsTable.dwNumEntries; i++)
                    {
                        MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)
                            Marshal.PtrToStructure(tableRowPtr, typeof(MIB_UDPROW_OWNER_PID));
                        udpTableRecords.Add(new UdpConnection(
                                                Protocol.UDP,
                                                new IPAddress(udpRow.localAddr),
                                                BitConverter.ToUInt16(new byte[2] { udpRow.localPort[1],
                                                udpRow.localPort[0] }, 0),
                                                udpRow.owningPid,
                                                processes));
                        tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(udpRow));
                    }
                }
                else if (ipVersion == IPVersion.IPv6)
                {
                    MIB_UDP6TABLE_OWNER_PID udpRecordsTable = (MIB_UDP6TABLE_OWNER_PID)
                        Marshal.PtrToStructure(udpTableRecordsPtr, typeof(MIB_UDP6TABLE_OWNER_PID));
                    IntPtr tableRowPtr = (IntPtr)((long)udpTableRecordsPtr +
                        Marshal.SizeOf(udpRecordsTable.dwNumEntries));

                    // Read and parse the UDP records from the table and store them in list 
                    // 'UdpConnection' structure type objects.
                    for (int i = 0; i < udpRecordsTable.dwNumEntries; i++)
                    {
                        MIB_UDP6ROW_OWNER_PID udpRow = (MIB_UDP6ROW_OWNER_PID)
                            Marshal.PtrToStructure(tableRowPtr, typeof(MIB_UDP6ROW_OWNER_PID));
                        udpTableRecords.Add(new UdpConnection(
                                                Protocol.UDP,
                                                new IPAddress(udpRow.localAddr, udpRow.localScopeId),
                                                BitConverter.ToUInt16(new byte[2] {
                                                udpRow.localPort[1],
                                                udpRow.localPort[0] }, 0),
                                                udpRow.owningPid,
                                                processes));
                        tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(udpRow));
                    }
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                throw outOfMemoryException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                Marshal.FreeHGlobal(udpTableRecordsPtr);
            }

            return udpTableRecords != null ? udpTableRecords.Distinct()
                .ToList() : new List<UdpConnection>();
        }
    }
}
