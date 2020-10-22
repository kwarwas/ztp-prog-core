using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace PLinq_2
{
    class Program
    {
        static void Main()
        {
            var csvConfiguration = new CsvConfiguration(new CultureInfo("en-GB"))
            {
                HasHeaderRecord = false
            };

            using var textReader = new StreamReader("employees.csv");
            using var csv = new CsvReader(textReader, csvConfiguration);
            var records = csv.GetRecords<Employee>();

            var men = records
                .AsParallel()
                .AsOrdered()
                .WithDegreeOfParallelism(6)
                .Where(x => x.Gender == 'M')
                .Take(1000);

            foreach (var item in men)
            {
                Console.WriteLine($"{item.Id} {item.FirstName} {item.LastName}");
            }
        }
    }
}