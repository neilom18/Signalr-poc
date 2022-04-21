using MediatR;

namespace Signalr_poc.Commands.Rooms
{
    public class CreateRoomCommand : IRequest<bool>
    {
        public string RoomName { get; set; }
    }
}
