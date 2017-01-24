using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Kaguwa.Network.Types
{
    /// <summary>
    /// A Host object. Contains a  HostName and an IPAddress.
    /// </summary>
    public class Host
    {
        private string hostName;
        private IPAddress ipAddress;

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        public IPAddress IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public Host() {}

        public Host(string hostName, IPAddress ipAddress)
        {
            HostName = hostName;
            IPAddress = ipAddress;
        }

        public Host(string hostName, string ipAddress)
        {
            HostName = hostName;
            IPAddress = IPAddress.Parse(ipAddress);
        }
    }
}
