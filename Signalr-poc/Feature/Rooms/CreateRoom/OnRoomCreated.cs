using MediatR;
using Signalr_poc.DomainEvents.DTOs;

namespace Signalr_poc.Events.Room.RoomCreated
{
    public class OnRoomCreated : Log
    {
        public string RoomName { get; set; }
        public OnRoomCreated(string roomName)
        {
            RoomName = roomName;
            SetLogMessage();
        }

        private void SetLogMessage() => base.SetLogMessage($"Grupo {RoomName} criado com sucesso | {DateTimeOffset.UtcNow.UtcDateTime}");
    }
}
