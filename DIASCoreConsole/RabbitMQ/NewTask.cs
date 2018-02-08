using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DIASCoreConsole.RabbitMQ
{
    class NewTask
    {
        private string EXCHNAGE_NAME = "direct_images";

        private string QUEUE_NAME = "task_queue";
        private IConnectionFactory _connectionFactory;
        private IImgaeFactory _imageFactory;

        public NewTask(IConnectionFactory connectionFactory, IImgaeFactory imageFactory)
        {
            _connectionFactory = connectionFactory;
            _imageFactory = imageFactory;
        }

        public async Task RunAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                Stopwatch stopwatch = new Stopwatch();               
                Console.WriteLine($"Start to send...");
                stopwatch.Start();
                while (_imageFactory.IsCompleted == false)
                {
                    var body = await _imageFactory.NextImageAsync();

                    channel.BasicPublish(exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: properties,
                        body: body);
                }
                stopwatch.Stop();
                Console.WriteLine($"    Send completed. {stopwatch.Elapsed}");
            }
        }

        public async Task RoutingAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: EXCHNAGE_NAME, type: "direct");               

                Stopwatch stopwatch = new Stopwatch();
                Console.WriteLine($"Start to send...");
                stopwatch.Start();
                while (_imageFactory.IsCompleted == false)
                {
                    var body = await _imageFactory.NextImageAsync();

                    channel.BasicPublish(exchange: EXCHNAGE_NAME,
                        routingKey: "DImage",
                        basicProperties: null,
                        body: body);
                }
                stopwatch.Stop();
                Console.WriteLine($"    Send completed. {stopwatch.Elapsed}");
            }
        }

        public async Task RunSend()
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                for (int i = 0; i < 500; i++)
                {
                    //var message = $"messgae {i}";
                    //var body = Encoding.UTF8.GetBytes(message);
                    var body = await _imageFactory.NextImageAsync();

                    channel.BasicPublish(exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($" [x] Sent {i}");
                }
            }
        }

        public static IConnectionFactory CreateRemoteConnectionFactory()
        {
            return new ConnectionFactory() { HostName = "10.10.22.119", UserName = "derek", Password = "123456" };
        }

        public static IConnectionFactory CreateLocalConnectionFactory()
        {
            return new ConnectionFactory() { HostName = "localhost" };
        }
    }
}
