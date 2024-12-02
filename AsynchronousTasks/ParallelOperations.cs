using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousTasks
{
    public  class ParallelOperations
    {

        // ParallelOperations.cs

        static void ProcessNumber(int number)
        {
            var random = new Random();
            Console.WriteLine($"Processing number: {number}");
         //   number += 100;
            Task.Delay(random.Next(3000, 10000)).Wait();
            Console.WriteLine($"{number} has been processed");
        }

        public static async Task RunParallelTasksAsync(List<int> number) {
            //Async Run the above function
            //to get numbers
         
            var result = Parallel.For(0, number.Count, (i) =>
            {

                Task.Delay(2000);
                ProcessNumber(number[i]);


            });

         
            }


        }

    }
