using SIPSorcery.Net;
using SIPSorceryMedia.Abstractions;

namespace Signalr_poc.Extensions.WebRTC
{
    public static class MediaTrack
    {
        public static void AddMediaTrack(this RTCPeerConnection peerConnection)
        {
            var audioTrack = new MediaStreamTrack(new AudioFormat(AudioCodecsEnum.OPUS, 101, 48000, 2,
            "ptime=60;maxptime=120;maxplaybackrate=8000;sprop-maxcapturerate=8000;maxaveragebitrate=8000;cbr=1;useinbandfec=0;"));

            peerConnection.addTrack(audioTrack);
        }
    }
}
