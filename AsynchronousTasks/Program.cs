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

                //await hello;
                //await world;

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

            //await PrintHelloWorld();

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
                    await Task.Delay(1000).ContinueWith(Result => Console.WriteLine(word));
                    //Task PrintWord = Task.Run(async () =>
                    //{
                    //    await Task.Delay(1000);
                    //    Console.WriteLine(word);
                    //});
                    //await PrintWord;
                }
            }

            //await Task.WhenAll(BigNums(),PrintStory());


            static async Task<string> decryptMessage(string fileContents)
            {
                string decryptedMessage = "";
                string messageToDecrypt = fileContents.ToLower();
                foreach (char c in fileContents)
                {
                    if (Char.IsPunctuation(c) || c == ' ')
                    {
                        decryptedMessage += c;
                    }
                    else if (c + 1 < 123)
                    {
                        decryptedMessage += (char)(c + 1);
                    }
                    else
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

            static async Task DecryptionTask()
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                cts.CancelAfter(1000);
                token.ThrowIfCancellationRequested();

                static async Task ReadDecryptWriteAll(string filePath)
                {
                    try
                    {
                        string fileContents = await ReadFile(filePath);
                        string decryptedMessage = await decryptMessage(fileContents);
                        await WriteFile(decryptedMessage);
                        Console.WriteLine(filePath + "Decripted and Written successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    } 
                };

                try
                {
                    var stopwatch = Stopwatch.StartNew();
                    await Task.WhenAll([
                        ReadDecryptWriteAll("ReallySuperSecretTextFile"),
                        ReadDecryptWriteAll("SuperSecretFile.txt"),
                        ReadDecryptWriteAll("SuperTopSecretTextFile.txt")
                    ]).WaitAsync(token);
                    Console.WriteLine($"This code executed in {stopwatch.ElapsedMilliseconds}ms");
                    stopwatch.Stop();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("took too long");
                }
            }

            await DecryptionTask();

        }
    } 
}
