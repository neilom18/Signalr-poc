using SIPSorcery.Net;

namespace Signalr_poc.Commands.WebRTC.CreateServerOffer
{
    public class CreateServerOfferCommandResult
    {
        public RTCSessionDescriptionInit Sdp { get; set; }
    }
}
