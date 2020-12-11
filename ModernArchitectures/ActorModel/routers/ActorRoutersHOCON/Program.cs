using System;
using System.Linq;
using ActorRouters.HOCON.Actors;
using ActorRouters.HOCON.Messages;
using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;

namespace ActorRouters.HOCON
{
    class Program
    {
        static void Main()
        {
            var config = ConfigurationFactory.ParseString(@"
            akka {
                actor {
                    deployment {
                        /OrderActor {
                            router = smallest-mailbox-pool
                            nr-of-instances = 5
                            resizer {
                                enabled = off
                                lower-bound = 1
                                upper-bound = 10
                            }
                        }
                    }
                }
            }");

            var system = ActorSystem.Create("ActorRouters", config);

            var props = Props.Create<OrderActor>().WithRouter(FromConfig.Instance);
            var actor = system.ActorOf(props, "OrderActor");

            Enumerable.Range(1, 10).ToList().ForEach
            (
                x => { actor.Tell(new OrderMessage(x, "Akka.NET Book")); }
            );

            //actor.Tell(PoisonPill.Instance);
            //vs
            //actor.Tell(new Broadcast(PoisonPill.Instance));

            Console.ReadLine();
        }
    }
}