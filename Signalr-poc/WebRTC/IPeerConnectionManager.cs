using Signalr_poc.Entity;
using SIPSorcery.Net;

namespace Signalr_poc.WebRTC;

public interface IPeerConnectionManager
{
    RTCPeerConnection CreatePeer();
    RTCSessionDescriptionInit? CreateOffer(RTCPeerConnection peerConnection, Room room, string hubConnectionId);
    bool SetRemoteDescription(RTCSessionDescription sdp);
    void AddIceCandidate(RTCIceCandidate candidate);
}

