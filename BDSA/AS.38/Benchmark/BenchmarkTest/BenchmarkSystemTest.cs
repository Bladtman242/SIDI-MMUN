using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmark;

namespace BenchmarkTest {
    [TestClass]
    public class BenchmarkSystemTest {
        private BenchmarkSystem system = new BenchmarkSystem();
        private Scheduler scheduler;
        private Job job1;
        private Job job2;
        private Job job3;
        private Job job4;

        /// <summary>
        /// Create some necessary parts and some dummy jobs.
        /// </summary>
        public BenchmarkSystemTest() {
            scheduler = system.GetScheduler();
            Logger logger = new Logger(system);

            job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                4,
                40,
                new Owner { name = "Michael" }
            );
            job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                4,
                5,
                new Owner { name = "Michael" }
            );
            job3 = new Job(
                p => { Console.WriteLine("Job3 is running"); return 0; },
                1,
                5,
                new Owner { name = "Michael" }
            );
            job4 = new Job(
                p => { Console.WriteLine("Job4 is running"); return 0; },
                6,
                10,
                new Owner { name = "Michael" }
            );
        }

        /// <summary>
        /// Test Submit method of BenchmarkSystem. Add 4 jobs to system 
        /// and test whether they're all there in the correct order.
        /// </summary>
        [TestMethod]
        public void TestSubmit() {
            system.Submit(job1);
            system.Submit(job2);
            system.Submit(job3);
            system.Submit(job4);

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual("4,5,Michael", jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("1,5,Michael", jobs[1].ToString(), "Job3 did not match");
            Assert.AreEqual("6,10,Michael", jobs[2].ToString(), "Job4 did not match");
            Assert.AreEqual("4,40,Michael", jobs[3].ToString(), "Job1 did not match");
        }

        /// <summary>
        /// Test Cancel method of BenchmarkSystem. Add 4 jobs to system,
        /// cancel 1 job and test whether the three remaining jobs are 
        /// the correct ones in the correct order.
        /// </summary>
        [TestMethod]
        public void TestCancel() {
            system.Submit(job1);
            system.Submit(job2);
            system.Submit(job3);
            system.Submit(job4);

            system.Cancel(job2);

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual("1,5,Michael", jobs[0].ToString(), "Job3 did not match");
            Assert.AreEqual("6,10,Michael", jobs[1].ToString(), "Job4 did not match");
            Assert.AreEqual("4,40,Michael", jobs[2].ToString(), "Job1 did not match");
        }

        /// <summary>
        /// Test ExecuteAll method of BenchmarkSystem. Add 4 jobs to
        /// system, execute all and test whether the system has no
        /// jobs remaining in queue.
        /// </summary>
        [TestMethod]
        public void TestExecuteAll() {
            system.Submit(job1);
            system.Submit(job2);
            system.Submit(job3);
            system.Submit(job4);

            system.ExecuteAll();

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual(0, jobs.Length, "System did not execute all jobs");
        }
    }
}
