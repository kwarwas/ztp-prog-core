using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PLinq_1
{
    [MemoryDiagnoser]
    public class Program
    {
        [Params( 1_000_000, 10_000_000)]
        public int ListSize { get; set; }

        [Benchmark(Baseline = true)]
        public void Version1()
        {
            double sum = 0;
            for (var n = 1; n <= ListSize; n++)
            {
                var a = n * 2;
                var b = Math.Sin((2 * Math.PI * a) / 1000);
                var c = Math.Pow(b, 2);
                sum += c;
            }

            Console.WriteLine($"for loop sum: {sum}");
        }

        [Benchmark]
        public void Version2()
        {
            var sum = Enumerable.Range(1, ListSize)
                .Select(n => n * 2)
                .Select(n => Math.Sin((2 * Math.PI * n) / 1000))
                .Select(n => Math.Pow(n, 2))
                .Sum();

            Console.WriteLine($"LINQ sum: {sum}");
        }
        
        [Benchmark]
        public void Version3()
        {
            var sum = Enumerable.Range(1, ListSize)
                .AsParallel()
                .Select(n => n * 2)
                .Select(n => Math.Sin((2 * Math.PI * n) / 1000))
                .Select(n => Math.Pow(n, 2))
                .Sum();

            Console.WriteLine($"PLINQ sum: {sum}");
        }
        
        [Benchmark]
        public void Version4()
        {
            var sum = Enumerable.Range(1, ListSize)
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .Select(n => n * 2)
                .Select(n => Math.Sin((2 * Math.PI * n) / 1000))
                .Select(n => Math.Pow(n, 2))
                .Sum();

            Console.WriteLine($"PLINQ(2) sum: {sum}");
        }
        
        public static void Main()
        {
            BenchmarkRunner.Run<Program>();
        }
    }
}