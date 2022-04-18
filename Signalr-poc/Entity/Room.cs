using SIPSorcery.Net;
using System.Collections.Generic;

namespace Signalr_poc.Entity;

public class Room
{
    public string Name { get;private set; }
    public Dictionary<string, RTCPeerConnection> Peers { get; set; }


    public Room(string name)
    {
        Name = name;
        Peers = new Dictionary<string, RTCPeerConnection>();
    }

    public void AddPeerConnection(string connectionId, RTCPeerConnection peerConnection)
    {
        Peers.TryAdd(connectionId, peerConnection);
    }

    public void RemovePeerConnection(string connectionId)
    {
        Peers.Remove(connectionId);
    }

    public void SendRtpPacket(RTPPacket packet, RTCPeerConnection peerConnection, string userName)
    {
        foreach(var peer in Peers.Values)
        {
            //if(peer != peerConnection)
            //{
                Console.WriteLine("Usuario " + userName + "enviou um RTPPacket");
                peer.SendRtpRaw
                    (SDPMediaTypesEnum.audio,
                    packet.Payload,
                    packet.Header.Timestamp,
                    packet.Header.MarkerBit,
                    packet.Header.PayloadType);
            //}
        }
    }

    public RTCPeerConnection? GetPeerConection(string connectionId)
    {
        return Peers.Where(x => x.Key == connectionId).Select(v => v.Value).FirstOrDefault();
    }
}

