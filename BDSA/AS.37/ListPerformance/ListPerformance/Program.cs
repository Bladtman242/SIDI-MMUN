using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ListPerformance
{
    class PerformanceTest
    {
        private static int total = 10000000; // Number of insertions
        private static int check = total / 10; // Check 10 times

        public void testList()
        {
            List<int> list = new List<int>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= total; i++)
            {
                list.Add(i);
                if (i % check == 0)
                {
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                }
            }
        }

        public void testDictionary()
        {
            Dictionary<int, int> list = new Dictionary<int, int>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= total; i++)
            {
                list.Add(i, i);
                if (i % check == 0)
                {
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                }
            }
        }

        public void testSortedDictionary()
        {
            SortedDictionary<int, int> list = new SortedDictionary<int, int>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= total; i++)
            {
                list.Add(i, i);
                if (i % check == 0)
                {
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("{0};{1}", i, ts.TotalMilliseconds);
                }
            }
        }

        static void Main(string[] args)
        {
            // Print as semi-colon separated CSV
            Console.WriteLine("#;ms");

            PerformanceTest performanceTest = new PerformanceTest();

            performanceTest.testList();
            //performanceTest.testDictionary();
            //performanceTest.testSortedDictionary();

            Console.ReadKey();
        }
    }
}
