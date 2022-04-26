using MediatR;
using Signalr_poc.Repository;
using Signalr_poc.WebRTC;

namespace Signalr_poc.Commands.WebRTC.SetAnswer
{
    public class SetAnswerCommandHandler : IRequestHandler<SetAnswerCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public SetAnswerCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository ??= roomRepository;
        }

        public Task<Unit> Handle(SetAnswerCommand request, CancellationToken cancellationToken)
        {
            var room = _roomRepository.GetRoom(request.RoomName);
            if (room is null) return Unit.Task;
            var peerConnection = room.GetPeerConection(request.ConnectionId);
            if (peerConnection is null) return Unit.Task;
            peerConnection.setRemoteDescription(request.Sdp);

            return Unit.Task;
        }
    }
}
