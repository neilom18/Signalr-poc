using MediatR;
using Signalr_poc.Extensions.MediatR;
using Signalr_poc.Repository;

namespace Signalr_poc.Commands.Rooms
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        public CreateRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository ??= roomRepository;
        }

        public Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var existRoom = _roomRepository.GetRoom(request.RoomName);
            if (existRoom != null) return Task.FromResult(false);
            var result = _roomRepository.CreateRoom(request.RoomName);
            return Task.FromResult(result);
        }
    }
}
