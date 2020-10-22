using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    class Program
    {
        static void Main()
        {
            var words = CreateWordArray(@"http://www.gutenberg.org/files/2009/2009.txt");

            Parallel.Invoke(() =>
                {
                    Console.WriteLine("Begin first task...");
                    GetLongestWord(words);
                },
                () =>
                {
                    Console.WriteLine("Begin second task...");
                    GetMostCommonWords(words);
                },
                () =>
                {
                    Console.WriteLine("Begin third task...");
                    GetCountForWord(words, "species");
                });

            Console.WriteLine("Returned from Parallel.Invoke");
        }

        private static void GetCountForWord(string[] words, string term)
        {
            var findWord = from word in words
                where word.ToUpper().Contains(term.ToUpper())
                select word;

            Console.WriteLine($@"Task 3 -- The word ""{term}"" occurs {findWord.Count()} times.");
        }

        private static void GetMostCommonWords(string[] words)
        {
            var frequencyOrder = from word in words
                where word.Length > 6
                group word by word
                into g
                orderby g.Count() descending
                select g.Key;

            var commonWords = frequencyOrder.Take(10);

            var sb = new StringBuilder();
            sb.AppendLine("Task 2 -- The most common words are:");
            foreach (var v in commonWords)
            {
                sb.AppendLine("  " + v);
            }

            Console.WriteLine(sb.ToString());
        }

        private static string GetLongestWord(string[] words)
        {
            var longestWord = (from w in words
                orderby w.Length descending
                select w).First();

            Console.WriteLine("Task 1 -- The longest word is {0}", longestWord);
            return longestWord;
        }

        // An http request performed synchronously for simplicity.
        static string[] CreateWordArray(string uri)
        {
            Console.WriteLine($"Retrieving from {uri}");

            // Download a web page the easy way.
            var s = new WebClient().DownloadString(uri);

            // Separate string into an array of words, removing some common punctuation.
            return s.Split(new[]
                {
                    ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/'
                },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}