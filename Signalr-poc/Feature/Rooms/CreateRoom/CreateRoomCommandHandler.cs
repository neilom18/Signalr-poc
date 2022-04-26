using MediatR;
using Signalr_poc.DomainEvents;
using Signalr_poc.DomainEvents.DTOs;
using Signalr_poc.Events.Room.RoomCreated;
using Signalr_poc.Extensions.MediatR;
using Signalr_poc.Repository;

namespace Signalr_poc.Commands.Rooms
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessageBus<Log> _messageBus;

        public CreateRoomCommandHandler(IRoomRepository roomRepository, IMessageBus<Log> messageBus)
        {
            _roomRepository = roomRepository;
            _messageBus = messageBus;
        }

        public Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var result = _roomRepository.CreateRoom(request.RoomName);
            if (result) _messageBus.SendMessage(this, new OnRoomCreated(request.RoomName));
            return Task.FromResult(result);
        }
    }
}
