using MediatR;

namespace Signalr_poc.Events.Room.JoinedRoom
{
    public class OnUserJoinedRoom : INotification
    {
        public string UserName { get; set; }
        public string RoomName { get; set; }
    }
}
