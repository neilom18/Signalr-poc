using MediatR;
using Signalr_poc.Repository;
using Signalr_poc.WebRTC;

namespace Signalr_pocRooms.Commands.Rooms
{
    public class JoinRoomCommandHandler : IRequestHandler<JoinRoomCommand, JoinRoomCommandResult>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPeerConnectionManager _peerConnectionManager;
        public JoinRoomCommandHandler(IRoomRepository roomRepository, IUserRepository userRepository, IPeerConnectionManager peerConnectionManager)
        {
            _roomRepository ??= roomRepository;
            _userRepository ??= userRepository;
            _peerConnectionManager ??= peerConnectionManager;
        }

        public Task<JoinRoomCommandResult> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUser(request.ConnectionId);
            var room = _roomRepository.GetRoom(request.RoomName);
            if (!user.Rooms.Contains(room))
            {
                var peerConnection = _peerConnectionManager.CreatePeer();
                room.AddPeerConnection(request.ConnectionId, peerConnection);
                user.Rooms.Add(room);
            }
            return Task.FromResult(new JoinRoomCommandResult() { Room = room, User = user});
        }
    }
}
