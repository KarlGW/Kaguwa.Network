using System.Net;
using System.Runtime.InteropServices;
using Kaguwa.Network.Enums;

namespace Kaguwa.Network.Types
{
    /// <summary>
    /// Abstract class to represent the corporation of TCP/UDP structs.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class NetworkConnection
    {
        public virtual Protocol Protocol { get; set; }
        public virtual IPAddress LocalAddress { get; set; }
        public virtual ushort LocalPort { get; set; }
        public virtual IPAddress RemoteAddress { get; set; }
        public virtual ushort RemotePort { get; set; }
        public virtual MibTcpState State { get; set; }
        public virtual int ProcessId { get; set; }
        public virtual string ProcessName { get; set; }
    }
}
