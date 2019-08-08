using System;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ.Connection
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
