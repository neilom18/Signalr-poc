using MediatR;

namespace Signalr_poc.Events.User.UserCreated
{
    public class OnUserCreated : INotification
    {
        public string UserName { get; set; }
    }
}
