using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmarksystem;

namespace BenchmarksystemTest {
    [TestClass]
    public class BenchmarkSystemTest {
        private Benchmarksystem.Benchmarksystem system = new Benchmarksystem.Benchmarksystem();
        private Scheduler scheduler;
        private Job Job1;
        private Job Job2;
        private Job Job3;
        private Job Job4;

        /// <summary>
        /// Create some necessary parts and some dummy Jobs.
        /// </summary>
        public BenchmarkSystemTest() {
            scheduler = system.GetScheduler();

            Job1 = new Job(
                p => { return 0; },
                1,
                "Michael",
                4,
                4
            );
            Job2 = new Job(
                p => { return 0; },
                2,
                "Michael",
                4,
                0.5
            );
            Job3 = new Job(
                p => { return 0; },
                3,
                "Michael",
                1,
                0.5
            );
            Job4 = new Job(
                p => { return 0; },
                4,
                "Michael",
                6,
                1
            );
        }

        /// <summary>
        /// Test Submit method of BenchmarkSystem. Add 4 Jobs to system 
        /// and test whether they're all there in the correct order.
        /// </summary>
        [TestMethod]
        public void TestSubmit() {
            system.Submit(Job1);
            system.Submit(Job2);
            system.Submit(Job3);
            system.Submit(Job4);

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual("job2", Jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("job3", Jobs[1].ToString(), "Job3 did not match");
            Assert.AreEqual("job4", Jobs[2].ToString(), "Job4 did not match");
            Assert.AreEqual("job1", Jobs[3].ToString(), "Job1 did not match");
        }

        /// <summary>
        /// Test Cancel method of BenchmarkSystem. Add 4 Jobs to system,
        /// cancel 1 Job and test whether the three remaining Jobs are 
        /// the correct ones in the correct order.
        /// </summary>
        [TestMethod]
        public void TestCancel() {
            system.Submit(Job1);
            system.Submit(Job2);
            system.Submit(Job3);
            system.Submit(Job4);

            system.Cancel(Job2);

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual("job3", Jobs[0].ToString(), "Job3 did not match");
            Assert.AreEqual("job4", Jobs[1].ToString(), "Job4 did not match");
            Assert.AreEqual("job1", Jobs[2].ToString(), "Job1 did not match");
        }

        /// <summary>
        /// Test Execute method of BenchmarkSystem. Add 4 Jobs to
        /// system, execute four times and test whether the system has no
        /// Jobs remaining in queue.
        /// </summary>
        [TestMethod]
        public void TestExecute() {
            system.Submit(Job1);
            system.Submit(Job2);
            system.Submit(Job3);
            system.Submit(Job4);

            system.Execute();
            system.Execute();
            system.Execute();
            system.Execute();

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual(0, Jobs.Length, "System did not execute all Jobs");
        }
    }
}