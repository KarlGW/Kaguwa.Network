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
        Closed = 1,
        Listening = 2,
        Syn_Sent = 3,
        Established = 5,
        Fin_Wait1 = 6,
        Fin_Wait2 = 7,
        Close_Wait = 8,
        Closing = 9,
        Last_Ack = 10,
        Time_Wait = 11,
        Delete_TCP = 12,
        None = 0
    }
}
