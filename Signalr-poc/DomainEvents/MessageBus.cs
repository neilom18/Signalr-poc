namespace Signalr_poc.DomainEvents
{
    public class MessageBus<T> : IMessageBus<T>
    {
        private static MessageBus<T> _instance = null;
        private static readonly object _lock = new object();

        public MessageBus()
        {
        }

        public static MessageBus<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageBus<T>();
                    }
                    return _instance;
                }
            }
        }

        public event EventHandler<MessageBusEventArgs<T>> MessageRecieved;

        public void SendMessage(object sender, T Message)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(sender, new MessageBusEventArgs<T>(Message));
            }
        }
    }
}
