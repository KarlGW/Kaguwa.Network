namespace Kaguwa.Network.Enums
{
    // enum for available protocols.
    public enum Protocol
    {
        TCP,
        UDP
    }

    // enum for IPVersion.
    public enum IPVersion
    {
        IPv4,
        IPv6
    }

    // enum for TPC_TABLE_CLASS.
    public enum TcpTableClass
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWMER_MODULE_ALL
    }

    // enum for UDP_TABLE_CLASS.
    public enum UdpTableClass
    {
        UDP_TABLE_BASIC,
        UDP_TABLE_OWNER_PID,
        UDP_TABLE_OWNER_MODULE
    }

    // enum for the different states of a connection.
    public enum MibTcpState
    {
        CLOSED = 1,
        LISTENING = 2,
        SYN_SENT = 3,
        ESTABLISHED = 5,
        FIN_WAIT1 = 6,
        FIN_WAOT2 = 7,
        CLOSE_WAIT = 8,
        CLOSING = 9,
        LAST_ACK = 10,
        TIME_WAIT = 11,
        DELETE_TCP = 12,
        NONE = 0
    }
}
