using MediatR;

namespace Signalr_poc.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
    }
}
