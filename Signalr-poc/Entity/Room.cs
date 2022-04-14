using SIPSorcery.Net;

namespace Signalr_poc.Entity;

public class Room
{
    public string Name { get;private set; }
    public List<RTCPeerConnection> Peers { get; set; }


    public Room(string name)
    {
        Name = name;
        Peers = new List<RTCPeerConnection>();
    }

    public void AddPeerConnection(RTCPeerConnection peerConnection)
    {
        Peers.Add(peerConnection);
    }

    public void RemovePeerConnection(RTCPeerConnection peerConnection)
    {
        Peers.Remove(peerConnection);
    }

    public void SendRtpPacket(RTPPacket packet, RTCPeerConnection peerConnection)
    {
        foreach(var peer in Peers)
        {
            if(peer != peerConnection)
            {
                peer.SendRtpRaw
                    (SDPMediaTypesEnum.audio,
                    packet.Payload,
                    packet.Header.Timestamp,
                    packet.Header.MarkerBit,
                    packet.Header.PayloadType);
            }
        }
    }
}

