using Akka.Actor;
using Akka.Configuration;
using RemoteActor.Remote.Actors;

namespace RemoteActor.Remote
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
                        port = 8099
                        hostname = 127.0.0.1
                    }
                }
            }");
            
            var system = ActorSystem.Create("RemoteActor", config);

            var gateway = system.ActorOf<GatewayActor>("GatewayActor");

            system.WhenTerminated.Wait();
        }
    }
}
