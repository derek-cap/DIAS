using RabbitMQ.Client;
using System;

namespace DIAS.EventBusRabbitMQ
{
    public interface IRabbitMQPersisterConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
