﻿
namespace Signalr_poc.Extensions.MediatR
{
    public interface ICustomPublisher
    {
        Task Publish<TNotification>(TNotification notification);
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken);
        Task Publish<TNotification>(TNotification notification, PublishStrategy strategy);
        Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
    }
}