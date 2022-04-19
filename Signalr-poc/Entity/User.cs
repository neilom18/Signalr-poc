using SIPSorcery.Net;

namespace Signalr_poc.Entity;

public class User
{
    public string ConnectionId { get;private set; }
    public string Name { get;private set; }
    public List<Room> Rooms { get; private set; }
    public List<RTCPeerConnection> RTCPeerConnections { get; set; }


    public User()
    {
        Rooms = new List<Room>();
        RTCPeerConnections = new List<RTCPeerConnection>();
    }

    public User(string userName, string connectionId)
    {
        Name = userName;
        Rooms = new List<Room>();
        ConnectionId = connectionId;
        RTCPeerConnections = new List<RTCPeerConnection>();
    }
}

