using MediatR;
using Signalr_poc.Feature.Rooms.CreateRoom;

namespace Signalr_poc.Event.Logging
{
    public class LoggerHandler : INotificationHandler<LogBase>
    {
        private readonly ILogger<LoggerHandler> _logger;

        public LoggerHandler(ILogger<LoggerHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(LogBase notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.LogMessage);
            return Task.CompletedTask;
        }
    }
}
