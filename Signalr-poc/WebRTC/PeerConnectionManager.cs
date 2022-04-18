using Microsoft.AspNetCore.SignalR;
using Signalr_poc.Entity;
using Signalr_poc.Repository;
using SIPSorcery.Net;
using SIPSorceryMedia.Abstractions;

namespace Signalr_poc.WebRTC;

public class PeerConnectionManager : IPeerConnectionManager
{

    private readonly IUserRepository _userRepository;
    private readonly IHubContext<Hub> _hubContext;

    public PeerConnectionManager(IUserRepository userRepository, IHubContext<Hub> hubContext)
    {
        _userRepository ??= userRepository;
        _hubContext ??= hubContext;
    }
    public RTCPeerConnection CreatePeer()
    {
        var config = GetRTCConfiguration();
        return new RTCPeerConnection(config);
    }

    private RTCConfiguration GetRTCConfiguration()
    {
        return new RTCConfiguration()
        {
            iceServers = new List<RTCIceServer>
            {
                new RTCIceServer
                {
                    urls = "stun:stun1.l.google.com:19302"
                }
            }
        };
    }

    public RTCSessionDescriptionInit? CreateOffer(RTCPeerConnection peerConnection, Room room, string hubConnectionId)
    {
        AddMediaTrack(peerConnection);
        ConfigureRTCPeer(peerConnection, room, hubConnectionId);
        return peerConnection.createOffer(null);
    }

    public void AddMediaTrack(RTCPeerConnection peerConnection)
    {
        var audioTrack = new MediaStreamTrack(new AudioFormat(AudioCodecsEnum.OPUS, 101, 48000, 2,
            "ptime=60;maxptime=120;maxplaybackrate=8000;sprop-maxcapturerate=8000;maxaveragebitrate=8000;cbr=1;useinbandfec=0;"));

        peerConnection.addTrack(audioTrack);
    }

    public void ConfigureRTCPeer(RTCPeerConnection peerConnection, Room room, string hubConnectionId)
    {
        peerConnection.onconnectionstatechange += (state) =>
        {
            if (state == RTCPeerConnectionState.disconnected || state == RTCPeerConnectionState.closed || state == RTCPeerConnectionState.failed)
            {
                room.RemovePeerConnection(hubConnectionId);
            }
        };
        var user = _userRepository.GetUsers().Where(x => x.RTCPeerConnections.Contains(peerConnection)).FirstOrDefault();
        peerConnection.OnRtpPacketReceived += (ip, media, pkt) => room.SendRtpPacket(pkt, peerConnection, user?.Name is null ? String.Empty : user.Name);

        // Diagnostics.
        peerConnection.OnReceiveReport += (re, media, rr) => Console.WriteLine($"RTCP Receive for {media} from {re}\n{rr.GetDebugSummary()}");
        peerConnection.OnSendReport += (media, sr) => Console.WriteLine($"RTCP Send for {media}\n{sr.GetDebugSummary()}");
        peerConnection.GetRtpChannel().OnStunMessageReceived += (msg, ep, isRelay) => Console.WriteLine($"STUN {msg.Header.MessageType} received from {ep}.");
        peerConnection.oniceconnectionstatechange += (state) => Console.WriteLine($"ICE connection state change to {state}.");

        peerConnection.onicecandidate += (candidate) => _hubContext.Clients.Client(hubConnectionId).SendAsync("IceCandidateAdded", candidate);
    }

    public void SetRemoteDescription(RTCSessionDescriptionInit sdp, RTCPeerConnection peerConnection)
    {
        peerConnection.setRemoteDescription(sdp);
    }

    public void AddIceCandidate(RTCIceCandidateInit candidate, RTCPeerConnection peerConnection)
    {
        peerConnection.addIceCandidate(candidate);
    }
}

