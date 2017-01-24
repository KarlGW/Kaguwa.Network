using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaguwa.Network.Types;
using System.Net;

namespace Kaguwa.Network.Tests
{
    [TestClass]
    public class DnsTests
    {
        [TestMethod]
        public void GetHostTests()
        {
            // Get Host.
            var expectedName = "goliath";
            var expectedIpAddr = "192.168.0.10";

            // Get host by HostName.
            Host host1 = Dns.GetHost("goliath");

            Assert.AreEqual(expectedName, host1.HostName);
            Assert.AreEqual(IPAddress.Parse(expectedIpAddr), host1.IPAddress);

            // Get host by IPAddress.
            Host host2 = Dns.GetHost("192.168.0.10");
            Assert.AreEqual(expectedName, host2.HostName);
            Assert.AreEqual(IPAddress.Parse(expectedIpAddr), host2.IPAddress);
        }
    }
}
