using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
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

           //  await PrintHelloWorld();

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
             //  await hello;
             //   await world;
                try
                {
                   //    await Task.WhenAny(Task.WhenAll(PrintHelloWorld()));
                  await Task.WhenAll([hello, world]).WaitAsync(token);

                    var combinedTask = Task.WhenAll([hello, world]);
                     await combinedTask;
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
                   // Console.WriteLine(s);
                }
              
                foreach (var num in bigInteger)
                {
                    Task<BigInteger> CalcFactorial = Task.Run(async () =>
                    {
                        return Exercises.CalculateFactorial(num);
                    });

                    await CalcFactorial.ContinueWith(ante => Console.WriteLine(ante.Result));
                }
            }

            static async Task PrintStory()
            {
                string story = "Mary had a little lamb, its fleece was white as snow.";

                List<string> storyWords = story.Split(" ").ToList();

                foreach (string word in storyWords)
                {
                    Task PrintWord = Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        Console.WriteLine(word);
                    });
                    await PrintWord;
                }
            }

         //   await Task.WhenAll(BigNums(),PrintStory());



            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            cts.CancelAfter(5000);
            token.ThrowIfCancellationRequested();

            static async Task<string> decryptMessage(string fileContents)
            {
                string decryptedMessage = "";
                string messageToDecrypt = fileContents.ToLower();
                foreach (char c in fileContents)
                {
                    if (Char.IsPunctuation(c) || c == ' ')
                    {
                        decryptedMessage += c;
                    } else if (c + 1 < 123)
                    {
                        decryptedMessage += (char)(c + 1);
                    } else
                    {
                        decryptedMessage += (char)(97);
                    }
                }
                return decryptedMessage;
            }

            static async Task<string> ReadFile(string filepath)
            {
                string fileContents = AsyncFileManager.ReadFile(filepath);
                return fileContents;
            }

            static async Task WriteFile(string decryptedMessage)
            {
                AsyncFileManager.WriteFile("DecryptedMessage.txt", decryptedMessage);
            }

           
            static async Task ReadDecryptWriteAll(string filePath)
            {
                string fileContents = await ReadFile(filePath);
                string decryptedMessage = await decryptMessage(fileContents);
                await WriteFile(decryptedMessage);
            }
/*
            string fileContents = await ReadFile("SuperSecretFile.txt");
            string fileContents2 = await ReadFile("ReallySuperSecretTextFile.txt");
            string fileContents3 = await ReadFile("SuperTopSecretTextFile.txt");
            string decryptedMessage = await decryptMessage(fileContents);
            string decryptedMessage2 = await decryptMessage(fileContents2);
            string decryptedMessage3 = await decryptMessage(fileContents3);


            await WriteFile(decryptedMessage);
            await WriteFile(decryptedMessage2);
            await WriteFile(decryptedMessage3);
*/

           await Task.WhenAll([ReadDecryptWriteAll("ReallySuperSecretTextFile.txt"), ReadDecryptWriteAll("SuperSecretFile.txt"), ReadDecryptWriteAll("SuperTopSecretTextFile.txt"),]);

            

        }
    } 
}
