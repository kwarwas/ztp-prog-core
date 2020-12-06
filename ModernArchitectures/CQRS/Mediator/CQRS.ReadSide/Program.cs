using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Core.Infrastructure.Cqrs.Queries;
using CQRS.Core.Query;
using CQRS.Model.ReadModel;
using CQRS.ReadSide.Query;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RepoDb;

namespace CQRS.ReadSide
{
    class Program
    {
        public static string ConnectionString = @"Server=localhost;port=3307;database=people;uid=root;pwd=password;";
        
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddSingleton<IQueryBus, QueryBus>()
                .BuildServiceProvider();

            MySqlBootstrap.Initialize();
            
            var queryBus = serviceProvider.GetService<IQueryBus>();

            if (queryBus is null)
                return;

            Console.WriteLine("People");

            var people = await queryBus.Send<GetPersonList, IEnumerable<PersonListItemRecord>>(new GetPersonList());
            
            foreach (var person in people)
            {
                Console.WriteLine($"{person.Id} {person.LastName} {person.AddressesCount}");
            }

            Console.WriteLine("Person with id = 2");
            
            var personDetails = await queryBus.Send<GetPersonDetails, PersonDetailsRecord>(new GetPersonDetails(2));

            Console.WriteLine($"{personDetails.FirstName} {personDetails.LastName}");
        }
    }
}