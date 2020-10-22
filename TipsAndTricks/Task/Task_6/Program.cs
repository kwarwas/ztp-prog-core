using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static async Task Main()
        {
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;

            var task = Task.Run(async () =>
            {
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        break;
                    }

                    Console.WriteLine(DateTime.Now);
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }, cancellationToken);

            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            tokenSource.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Now: {0}, CanBeCanceled: {1}", DateTime.Now, e.CancellationToken.CanBeCanceled);
            }
            finally
            {
                tokenSource.Dispose();
            }

            Console.WriteLine("Finish");
        }
    }
}