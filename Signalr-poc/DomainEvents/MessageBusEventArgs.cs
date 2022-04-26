namespace Signalr_poc.DomainEvents
{
    public class MessageBusEventArgs<T> : EventArgs
    {
        private T _message;

        public MessageBusEventArgs(T Message)
        {
            _message = Message;
        }

        public T Message
        {
            get
            {
                return _message;
            }
        }
    }
}
