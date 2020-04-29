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
        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            //var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Lada",
                LastName = "Hruska"
            };
            //var request = new GreetingRequest() { Greeting = greeting };
            //var response = client.Greet(request);

            var request = new GreetManyTimesRequest() { Greeting = greeting };
            var response = client.GreetManyTimes(request);

            //Console.WriteLine(response.Result);

            while(await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }

            //var client = new CalcService.CalcServiceClient(channel);
            //var request = new CalcRequest() { A = 10, B = 3 };
            //var response = client.Calculate(request);
            //Console.WriteLine(response.Sum);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
