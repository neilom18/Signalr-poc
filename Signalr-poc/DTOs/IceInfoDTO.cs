using SIPSorcery.Net;

namespace Signalr_poc.DTOs
{
    public class IceInfoDTO
    {
        public RTCIceCandidateInit iceCandidateInit { get; set; }
        public string roomName { get; set; }
    }
}
