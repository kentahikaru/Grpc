//-----------------------------------------------------------------------
// <copyright file="SqrtImpl.cs" company="IXTENT s.r.o.">
//     Copyright 2020 IXTENT s.r.o.
// </copyright>
//-----------------------------------------------------------------------

namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Sqrt;
    using static Sqrt.SqrtService;

    /// <summary>
    /// 
    /// </summary>
    public class SqrtServiceImpl : SqrtServiceBase
    {
        public override async Task<SqrtResponse> sqrt(SqrtRequest request, ServerCallContext context)
        {
            int number = request.Number;
            if(number >= 0)
            {
                return new SqrtResponse() { SquareRoot = Math.Sqrt(number) };
            }
            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Number is smalller than 0"));
            }
        }
    }
}
