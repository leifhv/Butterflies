using Butterflies.Shared;
using System;
using System.Diagnostics;

namespace BubbleSort.ConsoleApp
{
    class Program
    {
        private const int NumNumbers = 10000;

        static void Main(string[] args)
        {
            DoSort(true);
            DoSort(false);
            Console.ReadKey();
        }

        private static void DoSort(bool isWarmUp)
        {
            var numbers = new int[NumNumbers];
            for (int t = 0; t < NumNumbers; t++)
            {
                numbers[t] = NumNumbers - t;
            }

            var sortTask = new SortTask();
            sortTask.Integers = numbers;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            BubbleSorter.Sort(sortTask);

            stopWatch.Stop();
            if(!isWarmUp)
            {
                for (int t = 0; t < 100; t++)
                {
              //      Console.Write(numbers[t] + ",");
                }
                Console.WriteLine();
                Console.WriteLine($"Sorterte { numbers.Length} tall på {stopWatch.ElapsedMilliseconds} ms med .net");
            }
        }
    }
}
