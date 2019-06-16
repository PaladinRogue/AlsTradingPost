﻿using System;
using RabbitMQ.Client;

namespace Messaging.Broker.Connection.Interfaces
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}