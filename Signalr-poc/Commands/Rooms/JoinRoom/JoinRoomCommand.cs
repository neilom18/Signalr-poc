using MediatR;

namespace Signalr_pocRooms.Commands.Rooms
{
    public class JoinRoomCommand : IRequest<JoinRoomCommandResult>
    {
        public string RoomName { get; set; }
        public string ConnectionId { get; set; }
    }
}
