namespace Signalr_poc.EventAbstract
{
    public interface IHandlerBase
    {
        public Task Handler<TNotification>(TNotification handler, CancellationToken cancellationToken);
    }
}
