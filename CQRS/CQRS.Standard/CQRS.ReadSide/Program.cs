using System;
using System.Collections.Generic;
using CQRS.Core.Common;
using CQRS.Core.Query;
using CQRS.ReadSide.Database;
using CQRS.ReadSide.Query;

namespace CQRS.ReadSide
{
    public class Program
    {
        static void Main(string[] args)
        {
            var queryHandlerRegistrator = new HandlerRegistrator<IQueryHandler>();
            queryHandlerRegistrator.Register<GetPersonList>(new PersonHandler());
            queryHandlerRegistrator.Register<GetPersonDetails>(new PersonHandler());
            var queryBus = new QueryBus(queryHandlerRegistrator);

            var people = queryBus.Query<GetPersonList, IEnumerable<PersonListItemRecord>>(new GetPersonList());

            Console.WriteLine("People");
            
            foreach (var person in people.Result)
            {
                Console.WriteLine($"{person.Id} {person.LastName} {person.AddressesCount}");
            }

            Console.WriteLine("Person with id = 1");
            
            var personDetails = queryBus.Query<GetPersonDetails, PersonDetailsRecord>(new GetPersonDetails(1));

            Console.WriteLine($"{personDetails.Result.FirstName} {personDetails.Result.LastName}");
        }
    }
}