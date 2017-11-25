using Akka.Actor;
using Akka.Configuration;

namespace RemoteActor.Remote
{
    class Program
    {
        static void Main(string[] args)
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

            system.WhenTerminated.Wait();
        }
    }
}
