using DIASCoreConsole2;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIASCoreConsole.RabbitMQ
{
    class Worker
    {
        private string EXCHNAGE_NAME = "direct_images";
        private string QUEUE_NAME = "task_queue";
        private IConnectionFactory _connectionFactory;
        private string _name;

        public Worker(IConnectionFactory connectionFactory, string name)
        {
            _connectionFactory = connectionFactory;
            _name = name;
        }

        public async Task RunAsync(string name)
        {
            await Task.Delay(1);
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicQos(0, 1, false);

                Console.WriteLine($" [{name}] Waiting for message.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    await PrintBody(body);
                    Console.WriteLine($"  --[{name}] Done");

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: QUEUE_NAME,
                    autoAck: false,
                    consumer: consumer);

                Console.ReadLine();
            }
        }

        public async Task RoutingReceive()
        {
            await Task.Delay(1);
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(EXCHNAGE_NAME, type: "direct");

                //    var queueName = channel.QueueDeclare().QueueName;
                channel.QueueDeclare(queue: _name,
                  durable: true,
                  exclusive: false,
                  autoDelete: false,
                  arguments: null);

                channel.QueueBind(queue: _name,
                    exchange: EXCHNAGE_NAME,
                    routingKey: "DImage");

                Console.WriteLine($" [{_name}] Waiting for message.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    PrintBody(body).Wait();
                    Console.WriteLine($"  --[{_name}] Done");
                };
                channel.BasicConsume(queue: _name,
                    autoAck: true,
                    consumer: consumer);
                channel.CallbackException += (sender, ea) =>
                {
                    Console.WriteLine($"Call back exception, {ea.Exception}");
                };

                Console.ReadLine();
            }
        }

        static async Task PrintBody(byte[] body)
        {
            await Task.Delay(1);
            var message = Encoding.UTF8.GetString(body);
            var image = JsonConvert.DeserializeObject<DImage>(message);          
            Console.WriteLine($"Image {image.Index}: \t {image.Rows} x {image.Columns}");
            if (image.Index == 100)
            {
                throw new Exception("Enough");
            }
        }

        public async Task RunReceive()
        {
            await Task.Delay(1);
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                Console.WriteLine($" Waiting for message.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
               //     var body = ea.Body;
               //     var message = Encoding.UTF8.GetString(body);
              //      Console.WriteLine($" Received {message}");
                 //   await Task.Delay(2000);
                    Console.WriteLine($"  -- Done");
                };
                channel.BasicConsume(queue: "hello",
                    autoAck: true,
                    consumer: consumer);

                Console.ReadLine();
            }
        }

        private static IConnectionFactory CreateConnectionFactory()
        {
            return new ConnectionFactory() { HostName = "10.10.22.119", UserName = "derek", Password = "123456" };
        }

        public static IConnectionFactory CreateLocalConnectionFactory()
        {
            return new ConnectionFactory() { HostName = "localhost" };
        }
    }
}
