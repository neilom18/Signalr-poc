using Signalr_poc.Entity;
using Signalr_poc.Repository;
using SIPSorcery.Net;
using SIPSorceryMedia.Abstractions;

namespace Signalr_poc.WebRTC;

public class PeerConnectionManager : IPeerConnectionManager
{

    private readonly IUserRepository _userRepository;

    public PeerConnectionManager(IUserRepository userRepository)
    {
        _userRepository ??= userRepository;
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

    public RTCSessionDescription CreateOffer(RTCPeerConnection peerConnection, Room room)
    {
        AddMediaTrack(peerConnection);
        ConfigureRTCPeer(peerConnection, room);
    }

    public void AddMediaTrack(RTCPeerConnection peerConnection)
    {
        var audioTrack = new MediaStreamTrack(new AudioFormat(AudioCodecsEnum.OPUS, 101, 48000, 2,
            "ptime=60;maxptime=120;maxplaybackrate=8000;sprop-maxcapturerate=8000;maxaveragebitrate=8000;cbr=1;useinbandfec=0;"));

        peerConnection.addTrack(audioTrack);
    }

    public void ConfigureRTCPeer(RTCPeerConnection peerConnection, Room room)
    {
        peerConnection.onconnectionstatechange += (state) =>
        {
            if(state == RTCPeerConnectionState.disconnected || state == RTCPeerConnectionState.closed || state == RTCPeerConnectionState.failed)
            {
                var user = _userRepository.GetUsers()
                .Where(u => u.RTCPeerConnections.Contains(peerConnection))
                .First();
                room.RemoveUser(user);
            }
        };


    }
    public bool SetRemoteDescription(RTCSessionDescription sdp)
    {
    }
}

