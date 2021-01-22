using Akka.Actor;
using RemoteActor.Common.Messages;
using System;
using Akka.Configuration;

namespace RemoteActor.Local
{
    class Program
    {
        static void Main()
        {
            var config = ConfigurationFactory.ParseString(@"
            akka {
                actor {
                    provider = remote
                }

                remote {
                    dot-netty.tcp {
                        port = 0
                        hostname = 127.0.0.1
                    }
                }
            }");
            
            var system = ActorSystem.Create("LocalActor", config);
            
            var actor = system.ActorSelection("akka.tcp://RemoteActor@127.0.0.1:8099/user/GatewayActor");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(actor.Ask<int>(new OrderMessage(i, $"Akka.NET Book {i}")).Result);
            }

            Console.WriteLine("All messages have been sent");

            system.WhenTerminated.Wait();
        }
    }
}
