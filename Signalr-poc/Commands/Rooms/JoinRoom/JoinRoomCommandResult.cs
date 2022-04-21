using Signalr_poc.Entity;

namespace Signalr_pocRooms.Commands.Rooms
{
    public class JoinRoomCommandResult
    {
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
