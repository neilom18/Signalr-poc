
namespace Signalr_poc.DomainEvents
{
    public interface IMessageBus<T>
    {
        event EventHandler<MessageBusEventArgs<T>> MessageRecieved;

        void SendMessage(object sender, T Message);
    }
}