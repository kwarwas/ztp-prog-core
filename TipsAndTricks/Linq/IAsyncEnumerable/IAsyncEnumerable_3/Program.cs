using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace IAsyncEnumerable_3
{
    class Program
    {
        static async Task Main()
        {
            var csvConfiguration = new CsvConfiguration(new CultureInfo("en-GB"))
            {
                HasHeaderRecord = false
            };

            using var reader = new StreamReader("employees.csv");
            using var csv = new CsvReader(reader, csvConfiguration);

            var records = csv
                .GetRecordsAsync<Employee>()
                .Skip(100)
                .Take(100);

            await foreach (var record in records)
            {
                Console.WriteLine(record);
            }
        }
    }
}