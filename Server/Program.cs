﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greet;
using Calc;
using Grpc.Core;
using Sqrt;

namespace Server
{
    class Program
    {
    const int port = 50051;
        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;
            try
            {
                server = new Grpc.Core.Server()
                {
                    //Services = {SqrtService.BindService(new SqrtServiceImpl())},
                    Services = {GreetingService.BindService(new GreetingServiceImpl())},
                    //Services = {CalcService.BindService(new CalculatorServiceImpl())},
                    Ports = {new ServerPort("localhost", port, ServerCredentials.Insecure) }
                };
                server.Start();
                Console.WriteLine("Server is listening on port: " + port);
                Console.ReadKey();
            }
            catch(IOException ex)
            {
                Console.WriteLine("server did not start on port: " + port);
                throw;
            }
            finally
            {
                if(server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }
        }
    }
}
