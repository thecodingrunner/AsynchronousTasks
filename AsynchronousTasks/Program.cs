using System.Diagnostics;
using System.Numerics;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AsynchronousTasks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //Thread.Sleep(2000);
            //Console.WriteLine("Hello, World!");

            //    await PrintHelloWorld();
            await BigNums();
            static async Task PrintHelloWorld()
            {
                var rand = new Random();
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                var stopwatch = Stopwatch.StartNew();
                cts.CancelAfter(5000);
                token.ThrowIfCancellationRequested();

                Task hello = Task.Run(async () =>
                {
                    await Task.Delay(rand.Next(1000, 10000));
                    Console.WriteLine("Hello...");
                });

                Task world = Task.Run(async () =>
                {
                    await Task.Delay(rand.Next(1000, 10000));
                    Console.WriteLine("...World!");
                });

                try
                {
                    //    await Task.WhenAny(Task.WhenAll(PrintHelloWorld()));
                    await Task.WhenAll([hello, world]).WaitAsync(token);

                    // var combinedTask = Task.WhenAll([hello, world]);
                    // await combinedTask;
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("took too long");

                }

                stopwatch.Stop();
                Console.WriteLine($"This code executed in {stopwatch.ElapsedMilliseconds}ms");

            }



            static async Task BigNums()
            {
                string data = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760";
                List<BigInteger> bigInteger = new List<BigInteger>();
                string[] arr = data.Split(" ");
                foreach (string s in arr)
                {
                    bigInteger.Add(BigInteger.Parse(s));
                    Console.WriteLine(s);
                }
              
            foreach (var num in bigInteger)
            {
                    Task<BigInteger> CalcFactorial = Task.Run(async () =>
                    {
                        return Exercises.CalculateFactorial(num);
                    });

                 //   Task CalcFactorial = Task.Run(async () =>
                //    {

                 //       Exercises.CalculateFactorial(num);

                 //   });
                    await CalcFactorial.ContinueWith(ante => Console.WriteLine(ante.Result));
            }
           

            //
                
            }


        }
    } 
}
