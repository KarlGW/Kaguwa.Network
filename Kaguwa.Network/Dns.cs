using System.Linq;
using System.Net;
using Kaguwa.Network.Types;

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
            Host host = new Host();
            string name;
            IPHostEntry entry;
            // Get the HostEntry.
            try
            {
                entry = System.Net.Dns.GetHostEntry(hostName);
                IPAddress ipAddr = entry.AddressList.Where(e => e.AddressFamily.ToString() == "InterNetwork").FirstOrDefault();
                name = entry.HostName;

                host.HostName = name;
                host.IPAddress = ipAddr;
            }
            catch(System.Net.Sockets.SocketException)
            {
                host.HostName = "No such host is known";
                host.IPAddress = null;
            }
            catch(System.Exception)
            {
                host.HostName = "IPv4 address 0.0.0.0 and IPv6 address ::0 cannot be used.";
                host.IPAddress = null;
            }

            // Return a new Host object.
            return host;
        }
    }
}
