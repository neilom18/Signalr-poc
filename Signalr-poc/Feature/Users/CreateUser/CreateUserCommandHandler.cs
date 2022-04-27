﻿using MediatR;
using Signalr_poc.Extensions.MediatR;
using Signalr_poc.Feature.Rooms.CreateRoom;
using Signalr_poc.Repository;

namespace Signalr_poc.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private IUserRepository _userRepository;
        private readonly ICustomPublisher _publisher;
        public CreateUserCommandHandler(IUserRepository userRepository, ICustomPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher ??= publisher;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = _userRepository.CreateUser(request.UserName, request.ConnectionId);
            if(result) _publisher.Publish(new OnUserCreated(request.UserName),cancellationToken);
            return Task.FromResult(result);
        }
    }
}