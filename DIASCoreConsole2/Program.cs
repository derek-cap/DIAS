using DIASCoreConsole.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DIASCoreConsole2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string message = "Worker ";
                if (args.Length > 0)
                {
                    message = string.Join(" ", args);
                }
                Console.WriteLine($"Try run worker {message}");
                //   Worker.RunAsync(message);
                //Worker.RunReceive();
                Worker worker = new Worker(Worker.CreateLocalConnectionFactory(), message);
                worker.RoutingReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
