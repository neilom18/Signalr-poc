using MediatR;
using SIPSorcery.Net;

namespace Signalr_poc.Commands.WebRTC.SetAnswer
{
    public class SetAnswerCommand : IRequest
    {
        public RTCSessionDescriptionInit Sdp { get; set; }
        public string ConnectionId { get; set; }
        public string RoomName { get; set; }
    }
}
