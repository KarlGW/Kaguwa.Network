using System.Diagnostics;
using System.Net;
using Kaguwa.Network.Enums;
using Kaguwa.Network.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaguwa.Network.Tests
{
    /// <summary>
    /// Summary description for ComponentTests
    /// </summary>
    [TestClass]
    public class ComponentTests
    {
        /// <summary>
        /// Tests the creation of a TcpConnection object. With and without processes.
        /// </summary>
        [TestMethod]
        public void CreateTcpConnectionTest()
        {
            // Prepare existing processes.
            Process[] processes = Process.GetProcesses();
            var existingPid = processes[0].Id;
            var existingProcessName = processes[0].ProcessName;

            // Create a TcpConnection without processes.
            TcpConnection connection1 = new TcpConnection(Protocol.TCP,
                                                         new IPAddress(new byte[] { 192, 168, 1, 1 }),
                                                         new IPAddress(new byte[] { 192, 168, 10, 1 }),
                                                         2222, 2222, existingPid, MibTcpState.Established);

            // Create a TcpConnection with processes.
            TcpConnection connection2 = new TcpConnection(Protocol.TCP,
                                             new IPAddress(new byte[] { 192, 168, 1, 1 }),
                                             new IPAddress(new byte[] { 192, 168, 10, 1 }),
                                             2222, 2222, existingPid, MibTcpState.Established,
                                             processes);


            // Test all the properties on the TcpConnection object.
            Assert.AreEqual("Established", connection1.State.ToString());
            Assert.AreEqual("192.168.1.1", connection1.LocalAddress.ToString());
            Assert.AreEqual("192.168.10.1", connection1.RemoteAddress.ToString());
            Assert.AreEqual(2222, connection1.LocalPort);
            Assert.AreEqual(2222, connection1.RemotePort);
            Assert.AreEqual(existingPid, connection1.ProcessId);
            Assert.AreEqual(existingProcessName, connection2.ProcessName);
        }

        /// <summary>
        /// Tests the creation of an UdpConnection object. With and without processes.
        /// </summary>
        [TestMethod]
        public void CreateUdpConnectionTest()
        {
            // Prepare existing processes.
            Process[] processes = Process.GetProcesses();
            var existingPid = processes[0].Id;
            var existingProcessName = processes[0].ProcessName;

            // Create an UdpConnection without processes.
            UdpConnection connection1 = new UdpConnection(Protocol.UDP,
                                                         new IPAddress(new byte[] { 192, 168, 1, 1 }),
                                                         2222, existingPid);

            // Create an UdpConnection with processes.
            UdpConnection connection2 = new UdpConnection(Protocol.UDP,
                                             new IPAddress(new byte[] { 192, 168, 1, 1 }),
                                             2222, existingPid, processes);

            // Test all the properties on the UcpConnection object(s).
            Assert.AreEqual("192.168.1.1", connection1.LocalAddress.ToString());
            Assert.AreEqual(2222, connection1.LocalPort);
            Assert.AreEqual(existingPid, connection1.ProcessId);
            Assert.AreEqual(existingProcessName, connection2.ProcessName);
        }
    }
}
