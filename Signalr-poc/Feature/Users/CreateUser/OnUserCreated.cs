using MediatR;
using Signalr_poc.Event.Logging;

namespace Signalr_poc.Feature.Rooms.CreateRoom
{
    public class OnUserCreated : LogBase, INotification
    {
        public string UserName { get;private set; }
        public OnUserCreated(string userName) : base($"Usuário {userName} criado com sucesso! | {DateTimeOffset.UtcNow.UtcDateTime}")
        {
            UserName = userName;
        }

    }
}
