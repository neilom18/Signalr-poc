using SIPSorcery.Net;

namespace Signalr_poc.DTOs
{
    public class AnswerDTO
    {
        public RTCSessionDescriptionInit Sdp { get; set; }
        public string RoomName { get; set; }
    }
}
