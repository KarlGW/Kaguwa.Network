#Kaguwa.Network

A library containing network related classes.

##About

This library has Classes, Structs, and Enums that handles networking. At the time of this writing it's primary
function is to provide the class `Kaguwa.Network.IPHelper` that utilizez the IPHelper API (`iphlpapi.dll`) to
query the system on active TCP/UDP connections and return them with the `iphlpapi.dll`s two methods:

`GetExtendedTcpTable()` and `GetExtendedUdpTable()`.

##Build

Clone the repository to your system.

`git clone https://github.com/KarlGW/Kaguwa.Network`

Open it in Visual Studio and build the solution.

Include it in a library of your choice.

##Classes

###IPHelper

####Methods

This class provides two static methods. `GetTcpConnections()` and `GetUdpConnections()`.

```
Process[] processes = Process.GetProcesses();

To get all TCPv4 connections:
var connections = IPHelper.GetTcpConnections(IPVersion.IPv4, processes);

To get all TCPv6 connections:
var connections = IPHelper.GetTcpConnections(IPVersion.IPv6, processes);

To get all UDPv4 connections:
var connections = IPHelper.GetUdpConnections(IPVersion.IPv4, processes);

To get all UDPv6 connections:
var connections = IPHelper.GetUdpConnections(IPVersion.IPv6, processes);

```

###NetworkConnection
Abstract class to represent network connections.

###TcpConnection
Subclass of NetworkConnections. Represents TCP connections.

###UdpConnection
Subclass of NetworkConnections. Represents UDP connections.

##Structs

###MIB_TCPROW_OWNER_PID
Struct that represents TCP connections.

###MIB_TCPTABLE_OWNER_PID
Struct to hold `MIB_TCPROW_OWNER_PID` entries.

###MIB_TCP6ROW_OWNER_PID
Struct that represent TCP (IPv6) connections.

###MIB_TCP6TABLE_OWNER_PID
Struct to hold `MIB_TCP6ROW_OWNER_PID` entries.

###MIB_UDPROW_OWNER_PID
Struct that represents UDP connections.

###MIB_UDPTABLE_OWNER_PID
Struct to hold `MIB_UDPROW_OWNER_PID` entries.

###MIB_UDP6ROW_OWNER_PID
Struct that represents UDP (IPv6) connections.

###MIB_UDP6TABLE_OWNER_PID
Struct to hold `MIB_UDP6ROW_OWNER_PID` entries.


##Enums
Enumerations used by the classes.

###Protocol
Enum to represent TCP and UDP connections.

###IPVersion
Enum to represent IP versions, IPv4 and IPv6.

###TcpTableClass
Enum that represents TCP tables.

###UdpTableClass
Enum that represents UDP tables.

###MibTcpState
Enum that represents TCP states.

##Updates and verions

###0.1.6233.37253
Changed MibTcpState to use uppercase in first letter only.

###0.1.6229.38178
First used version.