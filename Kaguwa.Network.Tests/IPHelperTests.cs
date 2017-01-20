using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Kaguwa.Network.Enums;
using Kaguwa.Network.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaguwa.Network.Tests
{
    [TestClass]
    public class IPHelperTests
    {
        [TestMethod]
        public void GetTcpConnectionsTest()
        {
            // Get processes from the local system, since they are used to map NetworkConnections
            // to processes and process names.
            var processes = Process.GetProcesses();
            // The first result should under most circumstances yield an entry with 'TCP'.
            var expectedProtocol = "TCP";
            var expectedProcessId = processes[0].Id;
            var expectedProcessName = processes[0].ProcessName;
            // Get the connections from the static method.
            //var connections = IPHelper.GetTcpConnections(processes);
            var connections = IPHelper.GetTcpConnections(IPVersion.IPv4, processes);

            // Assertions

            // The resulting list should be of type TcpConnection.
            Assert.IsInstanceOfType(connections, typeof(List<TcpConnection>));

            // The protocol property should be TCP (when casted to a string).
            Assert.AreEqual(expectedProtocol, connections[0].Protocol.ToString());
            // Should be of type IPAddress.
            Assert.IsInstanceOfType(connections[0].LocalAddress, typeof(IPAddress));
            Assert.IsInstanceOfType(connections[0].RemoteAddress, typeof(IPAddress));
            // Should be of type UInt16
            Assert.IsInstanceOfType(connections[0].LocalPort, typeof(UInt16));
            Assert.IsInstanceOfType(connections[0].RemotePort, typeof(UInt16));
            // Should be of enum MibTcpState
            Assert.IsInstanceOfType(connections[0].State, typeof(MibTcpState));
            // The resulting collection of TcpConnections should have an entry with a ProcessName and ProcessId that exists in the
            // processess collection.
            Assert.IsNotNull(processes.Where(process => process.ProcessName == connections[0].ProcessName).FirstOrDefault().ProcessName);
            Assert.IsNotNull(processes.Where(process => process.Id == connections[0].ProcessId).FirstOrDefault().ProcessName);
        }

        [TestMethod]
        public void GetTcp6ConnectionsTest()
        {
            // Get processes from the local system, since they are used to map NetworkConnections
            // to processes and process names.
            var processes = Process.GetProcesses();
            // The first result should under most circumstances yield an entry with 'TCP'.
            var expectedProtocol = "TCP";
            var expectedProcessName = processes[0].ProcessName;
            // Get the connections from the static method.
            //var connections = IPHelper.GetTcp6Connections(processes);
            var connections = IPHelper.GetTcpConnections(IPVersion.IPv6, processes);

            // Assertions

            // The resulting list should be of type TcpConnection.
            Assert.IsInstanceOfType(connections, typeof(List<TcpConnection>));
            // The protocol property should be TCP (when casted to a string).
            Assert.AreEqual(expectedProtocol, connections[0].Protocol.ToString());

            // Should be of type IPAddress.
            Assert.IsInstanceOfType(connections[0].LocalAddress, typeof(IPAddress));
            Assert.IsInstanceOfType(connections[0].RemoteAddress, typeof(IPAddress));
            // Should be of type UInt16
            Assert.IsInstanceOfType(connections[0].LocalPort, typeof(UInt16));
            Assert.IsInstanceOfType(connections[0].RemotePort, typeof(UInt16));
            // Should be of enum MibTcpState
            Assert.IsInstanceOfType(connections[0].State, typeof(MibTcpState));
            // The resulting collection of TcpConnections should have an entry with a ProcessName and ProcessId that exists in the
            // processess collection.
            Assert.IsNotNull(processes.Where(process => process.ProcessName == connections[0].ProcessName).FirstOrDefault().ProcessName);
            Assert.IsNotNull(processes.Where(process => process.Id == connections[0].ProcessId).FirstOrDefault().ProcessName);
        }

        [TestMethod]
        public void GetUdpConnectionsTest()
        {
            // Get processes from the local system, since they are used to map NetworkConnections
            // to processes and process names.
            var processes = Process.GetProcesses();
            // The first result should under most circumstances yield an entry with 'UDP'.
            var expectedProtocol = "UDP";
            var expectedProcessName = processes[0].ProcessName;
            // Get the connections from the static method.
            //var connections = IPHelper.GetUdpConnections(processes);
            var connections = IPHelper.GetUdpConnections(IPVersion.IPv4, processes);

            // Assertions

            // The resulting list should be of type TcpConnection.
            Assert.IsInstanceOfType(connections, typeof(List<UdpConnection>));
            // The protocol property should be TCP (when casted to a string).
            Assert.AreEqual(expectedProtocol, connections[0].Protocol.ToString());
            // Should be of type IPAddress.
            Assert.IsInstanceOfType(connections[0].LocalAddress, typeof(IPAddress));
            // Should be of type UInt16
            Assert.IsInstanceOfType(connections[0].LocalPort, typeof(UInt16));
            // The resulting collection of TcpConnections should have an entry with a ProcessName and ProcessId that exists in the
            // processess collection.
            Assert.IsNotNull(processes.Where(process => process.ProcessName == connections[0].ProcessName).FirstOrDefault().ProcessName);
            Assert.IsNotNull(processes.Where(process => process.Id == connections[0].ProcessId).FirstOrDefault().ProcessName);
        }

        [TestMethod]
        public void GetUdp6ConnectionsTest()
        {
            // Get processes from the local system, since they are used to map NetworkConnections
            // to processes and process names.
            var processes = Process.GetProcesses();
            // The first result should under most circumstances yield an entry with 'UDP'.
            var expectedProtocol = "UDP";
            var expectedProcessName = processes[0].ProcessName;
            // Get the connections from the static method.
            //var connections = IPHelper.GetUdp6Connections(processes);
            var connections = IPHelper.GetUdpConnections(IPVersion.IPv6, processes);

            // Assertions

            // The resulting list should be of type TcpConnection.
            Assert.IsInstanceOfType(connections, typeof(List<UdpConnection>));
            // The protocol property should be TCP (when casted to a string).
            Assert.AreEqual(expectedProtocol, connections[0].Protocol.ToString());
            // Should be of type IPAddress.
            Assert.IsInstanceOfType(connections[0].LocalAddress, typeof(IPAddress));
            // Should be of type UInt16
            Assert.IsInstanceOfType(connections[0].LocalPort, typeof(UInt16));
            // The resulting collection of TcpConnections should have an entry with a ProcessName and ProcessId that exists in the
            // processess collection.
            Assert.IsNotNull(processes.Where(process => process.ProcessName == connections[0].ProcessName).FirstOrDefault().ProcessName);
            Assert.IsNotNull(processes.Where(process => process.Id == connections[0].ProcessId).FirstOrDefault().ProcessName);
        }
    }
}
