using MediatR;
using Signalr_poc.Repository;
using SIPSorcery.Net;

namespace Signalr_poc.Commands.WebRTC.AddIceCandidate
{
    public class AddIceCandidateCommandHandler : IRequestHandler<AddIceCandidateCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public AddIceCandidateCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<Unit> Handle(AddIceCandidateCommand request, CancellationToken cancellationToken)
        {
            var room = _roomRepository.GetRoom(request.IceInfo.RoomName);
            if (room == null) return Unit.Task;
            var peerConnection = room.GetPeerConection(request.ConnectionId);
            if (peerConnection == null) return Unit.Task; 
            peerConnection.addIceCandidate(request.IceInfo.IceCandidateInit);
            return Unit.Task;
        }
    }
}
