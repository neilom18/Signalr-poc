using MediatR;
using Signalr_poc.DTOs;

namespace Signalr_poc.Commands.WebRTC.AddIceCandidate
{
    public class AddIceCandidateCommand : IRequest
    {
        public IceInfoDTO IceInfo { get; set; }
        public string ConnectionId { get; set; }
    }
}
