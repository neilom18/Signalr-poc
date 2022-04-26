namespace Signalr_poc.DomainEvents.DTOs
{
    public class Log
    {
        public string LogMessage { get;private set; }
        public Log(string logMessage)
        {
            LogMessage = logMessage;
        }
    }
}
