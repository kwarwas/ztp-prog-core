using System;
using System.Collections.Generic;
using Akka.Actor;
using CQRS.ReadSide.Database;
using CQRS.ReadSide.Query;

namespace CQRS.ReadSide
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var actorSystem = ActorSystem.Create("CQRS"))
            {
                var rootQueryActor = actorSystem.ActorOf<QueryRootActor>();
                
                var people = rootQueryActor.Ask<IEnumerable<PersonListItemRecord>>(new GetPersonList());

                Console.WriteLine("People");
            
                foreach (var person in people.Result)
                {
                    Console.WriteLine($"{person.Id} {person.LastName} {person.AddressesCount}");
                }

                Console.WriteLine("Person with id = 1");
            
                var personDetails = rootQueryActor.Ask<PersonDetailsRecord>(new GetPersonDetails(1));

                Console.WriteLine($"{personDetails.Result.FirstName} {personDetails.Result.LastName}");
            }
        }
    }
}