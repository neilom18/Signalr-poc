using SIPSorcery.Net;

namespace Signalr_poc.DTOs
{
    public class IceInfoDTO
    {
        public RTCIceCandidateInit IceCandidateInit { get; set; }
        public string RoomName { get; set; }
    }
}
