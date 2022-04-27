using MediatR;

namespace Signalr_poc.Commands.WebRTC.CreateServerOffer
{
    public class CreateServerOfferCommand : IRequest<CreateServerOfferCommandResult>
    {
        public string RoomName { get; set; }
        public string ConnectionId { get; set; }
    }
}
