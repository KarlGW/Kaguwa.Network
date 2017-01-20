using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Kaguwa.Network.Enums;

namespace Kaguwa.Network.Types
{
    /// <summary>
    /// Class that represents the incorporation of MIB_TCPROW_OWNER_PID and
    /// MIB_TCP6ROW_OWNER_PID. Inherits from NetworkConnection.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TcpConnection : NetworkConnection
    {
        public override Protocol Protocol { get; set; }
        public override IPAddress LocalAddress { get; set; }
        public override ushort LocalPort { get; set; }
        public override IPAddress RemoteAddress { get; set; }
        public override ushort RemotePort { get; set; }
        public override MibTcpState State { get; set; }
        public override int ProcessId { get; set; }
        public override string ProcessName { get; set; }

        public TcpConnection(Protocol protocol, IPAddress localIp, IPAddress remoteIp, ushort localPort,
            ushort remotePort, int pId, MibTcpState state)
        {
            Protocol = protocol;
            LocalAddress = localIp;
            RemoteAddress = remoteIp;
            LocalPort = localPort;
            RemotePort = remotePort;
            State = state;
            ProcessId = pId;
        }

        public TcpConnection(Protocol protocol, IPAddress localIp, IPAddress remoteIp, ushort localPort,
            ushort remotePort, int pId, MibTcpState state, Process[] processes)
        {
            Protocol = protocol;
            LocalAddress = localIp;
            RemoteAddress = remoteIp;
            LocalPort = localPort;
            RemotePort = remotePort;
            State = state;
            ProcessId = pId;
            ProcessName = processes.Where(process => process.Id == pId).FirstOrDefault().ProcessName;
        }
    }
}
