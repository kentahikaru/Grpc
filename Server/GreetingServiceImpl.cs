//-----------------------------------------------------------------------
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
    }
}
