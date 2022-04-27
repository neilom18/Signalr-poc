using MediatR;

namespace Signalr_poc.Event.Logging
{
    public class LogBase : INotification
    {
        public string LogMessage { get;private set; }
        public LogBase(string message)
        {
            LogMessage = message;
        }
    }
}
