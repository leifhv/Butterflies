using System;
using System.Collections.Generic;
using System.Text;

namespace Butterflies.Shared
{
    public class BubbleSorter
    {
        public static void Sort(SortTask sortTask)
        {
            var numbers = sortTask.Integers;
            int highValue;
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        highValue = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = highValue;
                    }
                }
           }
        }
    }
}
