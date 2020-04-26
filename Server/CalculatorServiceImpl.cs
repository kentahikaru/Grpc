//-----------------------------------------------------------------------
// <copyright file="CalculatorServiceImpl.cs" company="IXTENT s.r.o.">
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
    using Calc;
    using static Calc.CalcService;
    using Grpc.Core;

    /// <summary>
    /// 
    /// </summary>
    public class CalculatorServiceImpl : CalcServiceBase
    {
        public override Task<CalcResponse> Calculate(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcResponse() { Sum = request.A + request.B });
        }
    }
}
