﻿//-----------------------------------------------------------------------
// <copyright file="GreetingServiceImpl.cs" company="IXTENT s.r.o.">
//     Copyright 2020 IXTENT s.r.o.
// </copyright>
//-----------------------------------------------------------------------

namespace Server
{
    using Greet;
    using Grpc.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Greet.GreetingService;

    /// <summary>
    /// 
    /// </summary>
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = String.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            return Task.FromResult(new GreetingResponse() { Result = result });
        }

        public override async Task GreetManyTimes(GreetManyTimesRequest request, IServerStreamWriter<GreetManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.Write("Server received the request : ");
            Console.WriteLine(request.ToString());

            string result = String.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            foreach(int i in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetManyTimesResponse() { Result = result });
            }
        }

        public override async Task<LongGreetResponse> LongGreet(IAsyncStreamReader<LongGreetRequest> requestStream, ServerCallContext context)
        {
            string result = "";

            while(await requestStream.MoveNext())
            {
                result += String.Format("Hello {0} {1} {2}", 
                    requestStream.Current.Greeting.FirstName,
                    requestStream.Current.Greeting.LastName,
                    Environment.NewLine);
            }

            return new LongGreetResponse() { Result = result };
        }

        public override async Task GreetEveryone(IAsyncStreamReader<GreetEveryoneRequest> requestStream, IServerStreamWriter<GreetEveryoneResponse> responseStream, ServerCallContext context)
        {
            while(await requestStream.MoveNext())
            {
                var result = String.Format("Hello {0} {1}", requestStream.Current.Greeting.FirstName, requestStream.Current.Greeting.LastName);
                Console.WriteLine("Received: " + result);
                await responseStream.WriteAsync(new GreetEveryoneResponse() { Result = result });
            }
        }

        public override async Task<GreetingResponse> GreetWithDeadline(GreetingRequest request, ServerCallContext context)
        {
            Console.WriteLine("Received call");
            await Task.Delay(300);
            return new GreetingResponse() { Result = "Hellooo " + request.Greeting.FirstName + " " + request.Greeting.LastName };
        }

    }
}
