using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ListPerformance
{
    class ContainsTest
    {
        private static int total = 10000000; // Number of insertions
        private static int total2 = 10000;
        private static int check = total2 / 10; // Check 10 times

        private int[] numbers = new int[total];

        private List<int> list = new List<int>();
        private Dictionary<int, int> dictionary = new Dictionary<int,int>();
        private SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int,int>();

        public ContainsTest()
        {
            generateNumberArray();

            for (int i = 0; i < total; i++)
            {
                int value = numbers[i];
                list.Add(value);
                dictionary.Add(value, value);
                sortedDictionary.Add(value, value);
            }
        }

        private void generateNumberArray()
        {
            Random random = new Random();

            for (int i = 0; i < total; i++)
            {
                numbers[i] = i;
            }

            // Every day I'm (Knuth) shuf-fe-ling!
            for (int i = 0; i < total; i++)
            {
                int j = random.Next(i, total);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }
        }

        public void testList(bool sort)
        {
            if (sort)
            {
                Stopwatch sortTime = new Stopwatch();
                sortTime.Start();
                list.Sort();
                TimeSpan ts2 = sortTime.Elapsed;
                Console.WriteLine("Sorting time: {0}", ts2.TotalMilliseconds);

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                for (int i = 0; i <= total2; i++)
                {
                    list.BinarySearch(i);
                    if (i % check == 0)
                    {
                        TimeSpan ts = stopWatch.Elapsed;
                        Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                    }
                }
            }
            else
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                for (int i = 0; i <= total2; i++)
                {
                    list.Contains(i);
                    if (i % check == 0)
                    {
                        TimeSpan ts = stopWatch.Elapsed;
                        Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                    }
                }
            }
        }

        public void testDictionary()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= total2; i++)
            {
                dictionary.ContainsValue(i);
                if (i % check == 0)
                {
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                }
            }
        }

        public void testSortedDictionary()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= total2; i++)
            {
                sortedDictionary.ContainsValue(i);
                if (i % check == 0)
                {
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Generating lists");
            ContainsTest containsTest = new ContainsTest();
            Console.WriteLine(" - DONE");

            //Console.WriteLine("Testing List");
            //containsTest.testList(false);
            //Console.WriteLine("Testing Dictionary");
            //containsTest.testDictionary();
            //Console.WriteLine("Testing SortedDictionary");
            //containsTest.testSortedDictionary();

            Console.WriteLine("Testing sorted List");
            containsTest.testList(true);

            Console.ReadKey();
        }
    }
}
