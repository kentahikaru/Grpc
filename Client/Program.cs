using Dummy;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greet;
using Calc;
using Sqrt;

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
            //var client = new SqrtService.SqrtServiceClient(channel);


            var greeting = new Greeting()
            {
                FirstName = "Lada",
                LastName = "Hruska"
            };




            //var request = new GreetingRequest() { Greeting = greeting };
            //var response = client.Greet(request);

            //var request = new GreetManyTimesRequest() { Greeting = greeting };
            //var response = client.GreetManyTimes(request);

            //Console.WriteLine(response.Result);

            //while(await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200); 
            //}




            //var request = new LongGreetRequest() { Greeting = greeting };
            //var stream = client.LongGreet();

            //foreach(int i in Enumerable.Range(1,10))
            //{
            //    await stream.RequestStream.WriteAsync(request);
            //}

            //await stream.RequestStream.CompleteAsync();

            //var response = await stream.ResponseAsync;

            //Console.WriteLine(response.Result);




            //var stream = client.GreetEveryone();
            //var responseReaderTask = Task.Run(async () => { 
            //    while(await stream.ResponseStream.MoveNext())
            //    {
            //        Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
            //    }
            //});

            //Greeting[] greetings =
            //{
            //    new Greeting() { FirstName = "John", LastName = "Doh"},
            //    new Greeting() { FirstName = "Hikaru" , LastName = "Kenta"},
            //    new Greeting() { FirstName = "Sayaka", LastName = "Mori"}
            //};

            //foreach(var oneGreeting in greetings)
            //{
            //    Console.WriteLine("Sending : " + oneGreeting.ToString());
            //    await stream.RequestStream.WriteAsync(new GreetEveryoneRequest() { Greeting = oneGreeting });
            //    await Task.Delay(2000);
            //}

            //await stream.RequestStream.CompleteAsync();
            //await responseReaderTask;



            //int number = 16;
            //int number = -1;
            //try
            //{
            //    var response = client.sqrt(new SqrtRequest() { Number = number });
            //    Console.WriteLine(response.SquareRoot);
            //}
            //catch (RpcException ex)
            //{
            //    Console.WriteLine("Error : " + ex.Status.Detail);
            //}




            try
            {
                //var request = new GreetingRequest() { Greeting = greeting };
                //var response = client.Greet(request);
                var response = client.GreetWithDeadline(new GreetingRequest() { Greeting = greeting }, deadline: DateTime.UtcNow.AddMilliseconds(100));
                Console.WriteLine(response.Result);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Error: " + ex.Status.Detail);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
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
