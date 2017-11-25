using ActorScheduler.Actors;
using Akka.Actor;
using System;

namespace ActorScheduler
{
    public class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorScheduler");

            system.ActorOf<OrderActor>();

            Console.ReadLine();

            system
                .Terminate()
                .ContinueWith(x => Console.WriteLine("Program finish"));

            system.WhenTerminated.Wait();

            Console.ReadLine();
        }
    }
}
