using System.Diagnostics;

namespace AsynchronousTasks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //Thread.Sleep(2000);
            //Console.WriteLine("Hello, World!");

            await PrintHelloWorld();

            static async Task PrintHelloWorld()
            {
                var stopwatch = Stopwatch.StartNew();

                Task hello = Task.Run(async () => {
                    await Task.Delay(3000);
                    Console.WriteLine("Hello...");
                });

                Task world = Task.Run(async () => {
                    await Task.Delay(3000);
                    Console.WriteLine("...World!");
                });

                var combinedTask = Task.WhenAll([hello, world]);
                await combinedTask;

                stopwatch.Stop();
                Console.WriteLine($"This code executed in {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
