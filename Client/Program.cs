using Dummy;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greet;
using Calc;

namespace Client
{
    class Program
    {
        const string target = "127.0.0.1:50051";
        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            //var client = new DummyService.DummyServiceClient(channel);
            //var client = new GreetingService.GreetingServiceClient(channel);

            //var greeting = new Greeting()
            //{
            //    FirstName = "Lada",
            //    LastName = "Hruska"
            //};
            //var request = new GreetingRequest() { Greeting = greeting };
            //var response = client.Greet(request);

            //Console.WriteLine(response.Result);

            var client = new CalcService.CalcServiceClient(channel);
            var request = new CalcRequest() { A = 10, B = 3 };
            var response = client.Calculate(request);
            Console.WriteLine(response.Sum);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
