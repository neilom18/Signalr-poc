using Microsoft.AspNetCore.SignalR;
using Signalr_poc.Entity;
using Signalr_poc.Repository;
using SIPSorcery.Net;

namespace Signalr_poc.Extensions.WebRTC
{
    public static class RTCPeer
    {
        public static void SubscribeEvents(this RTCPeerConnection peerConnection,
            Room room,
            string connectionId,
            IUserRepository userRepository,
            IHubContext<HubConnection> hubContext)
        {
            peerConnection.onconnectionstatechange += (state) =>
            {
                if (state == RTCPeerConnectionState.disconnected || state == RTCPeerConnectionState.closed || state == RTCPeerConnectionState.failed)
                {
                    room.RemovePeerConnection(connectionId);
                }
            };
            var user = userRepository.GetUsers().Where(x => x.RTCPeerConnections.Contains(peerConnection)).FirstOrDefault();
            peerConnection.OnRtpPacketReceived += (ip, media, pkt) => room.SendRtpPacket(pkt, peerConnection, user?.Name is null ? String.Empty : user.Name);

            // Diagnostics.
            peerConnection.OnReceiveReport += (re, media, rr) => Console.WriteLine($"RTCP Receive for {media} from {re}\n{rr.GetDebugSummary()}");
            peerConnection.OnSendReport += (media, sr) => Console.WriteLine($"RTCP Send for {media}\n{sr.GetDebugSummary()}");
            peerConnection.GetRtpChannel().OnStunMessageReceived += (msg, ep, isRelay) => Console.WriteLine($"STUN {msg.Header.MessageType} received from {ep}.");
            peerConnection.oniceconnectionstatechange += (state) => Console.WriteLine($"ICE connection state change to {state}.");

            peerConnection.onicecandidate += (candidate) => hubContext.Clients.Client(connectionId).SendAsync("IceCandidateAdded", candidate);
        }
    }
}
