using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaguwa.Network.Types;
using System.Net;

namespace Kaguwa.Network
{
    public class Dns
    {
        /// <summary>
        /// Gets the host entry and creates a Host object from it.
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns>Returns a Host object.</returns>
        public static Host GetHost(string hostName)
        {
            // Get the HostEntry.
            var entry = System.Net.Dns.GetHostEntry(hostName);
            // Get the IPv4 address.
            IPAddress ipAddr = entry.AddressList.Where(e => e.AddressFamily.ToString() == "InterNetwork").FirstOrDefault();
            // Return a new Host object.
            return new Host(entry.HostName, ipAddr);
        }
    }
}
