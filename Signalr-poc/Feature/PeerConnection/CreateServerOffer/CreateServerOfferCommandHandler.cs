using MediatR;
using Microsoft.AspNetCore.SignalR;
using Signalr_poc.Extensions.WebRTC;
using Signalr_poc.Repository;

namespace Signalr_poc.Commands.WebRTC.CreateServerOffer
{
    public class CreateServerOfferCommandHandler : IRequestHandler<CreateServerOfferCommand, CreateServerOfferCommandResult>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<HubConnection> _hubContext;

        public CreateServerOfferCommandHandler(IRoomRepository roomRepository, IUserRepository userRepository, IHubContext<HubConnection> hubContext)
        {
            _roomRepository ??= roomRepository;
            _userRepository ??= userRepository;
            _hubContext ??= hubContext;
        }

        public Task<CreateServerOfferCommandResult> Handle(CreateServerOfferCommand request, CancellationToken cancellationToken)
        {
            var room = _roomRepository.GetRoom(request.RoomName);
            var peerConnection = room.GetPeerConection(request.ConnectionId);
            peerConnection.AddMediaTrack();
            peerConnection.SubscribeEvents(room, request.ConnectionId, _userRepository, _hubContext);
            return Task.FromResult(new CreateServerOfferCommandResult() 
            {
                Sdp = peerConnection.createOffer(null) 
            });
        }
    }
}
