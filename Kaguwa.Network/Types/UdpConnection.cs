using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Kaguwa.Network.Enums;

namespace Kaguwa.Network.Types
{
    /// <summary>
    /// Class that represents the incorporation of MIB_UDPROW_OWNER_PID and
    /// MIB_UDP6ROW_OWNER_PID. Inherits from NetworkConnection.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class UdpConnection : NetworkConnection
    {
        public override Protocol Protocol { get; set; }
        public override IPAddress LocalAddress { get; set; }
        public override ushort LocalPort { get; set; }
        public override int ProcessId { get; set; }
        public override string ProcessName { get; set; }

        public UdpConnection(Protocol protocol, IPAddress localAddress, ushort localPort, int pId)
        {
            Protocol = protocol;
            LocalAddress = localAddress;
            LocalPort = localPort;
            ProcessId = pId;
        }

        public UdpConnection(Protocol protocol, IPAddress localAddress, ushort localPort, int pId, Process[] processes)
        {
            Protocol = protocol;
            LocalAddress = localAddress;
            LocalPort = localPort;
            ProcessId = pId;

            ProcessName = processes.Where(process => process.Id == pId).FirstOrDefault().ProcessName;
        }
    }
}
