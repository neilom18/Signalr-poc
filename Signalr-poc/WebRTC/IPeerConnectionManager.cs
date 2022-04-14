using Signalr_poc.Entity;
using SIPSorcery.Net;

namespace Signalr_poc.WebRTC;

public interface IPeerConnectionManager
{
    RTCPeerConnection CreatePeer(User user);
    RTCSessionDescription CreateOffer(RTCPeerConnection peerConnection);
    bool SetRemoteDescription(RTCSessionDescription sdp);

}

