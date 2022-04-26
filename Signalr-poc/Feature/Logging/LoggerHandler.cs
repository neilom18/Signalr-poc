using MediatR;
using Signalr_poc.DomainEvents;
using Signalr_poc.DomainEvents.DTOs;
using Signalr_poc.Events.Room.JoinedRoom;
using Signalr_poc.Events.Room.RoomCreated;
using Signalr_poc.Events.User.UserCreated;

namespace Signalr_poc.Events
{
    public class LoggerHandler : INotificationHandler<OnUserJoinedRoom>, INotificationHandler<OnUserCreated>
    {
        private readonly ILogger<LoggerHandler> _logger;
        private readonly IMessageBus<Log> _messageBus;

        public LoggerHandler(ILogger<LoggerHandler> logger, IMessageBus<Log> messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
            Init();
        }

        private void Init()
        {
            _messageBus.MessageRecieved += MessageRecieved;
        }

        private void MessageRecieved(object? sender, MessageBusEventArgs<Log> e)
        {
            Console.WriteLine(sender?.GetType());
            _logger.LogInformation(e.Message.LogMessage);
        }

        /*public Task Handle(OnRoomCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Grupo {notification.RoomName} criado com sucesso | {DateTimeOffset.UtcNow.UtcDateTime}");
            return Task.CompletedTask;
        }*/
        public Task Handle(OnUserJoinedRoom notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuário {notification.UserName} entrou no grupo {notification.RoomName} | {DateTimeOffset.UtcNow.UtcDateTime}");
            return Task.CompletedTask;
        }

        public Task Handle(OnUserCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuário {notification.UserName} foi cadastrado | {DateTimeOffset.UtcNow.UtcDateTime}");
            return Task.CompletedTask;
        }
    }
}
