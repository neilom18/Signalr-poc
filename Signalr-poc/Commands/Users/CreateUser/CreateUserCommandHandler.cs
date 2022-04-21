using MediatR;
using Signalr_poc.Repository;

namespace Signalr_poc.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = _userRepository.CreateUser(request.UserName, request.ConnectionId);
            return Task.FromResult(result);
        }
    }
}
