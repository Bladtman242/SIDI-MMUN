using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmark;

namespace BenchmarkTest {
    [TestClass]
    public class SchedulerTest {
        private Scheduler scheduler = new Scheduler();
        private Job Job1;
        private Job Job2;
        private Job Job3;

        /// <summary>
        /// Create some dummy Jobs.
        /// </summary>
        public SchedulerTest() {
            Job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                "job1",
                "Michael",
                4,
                40
            );
            Job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                "job2",
                "Michael",
                3,
                20
            );
            Job3 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                "job3",
                "Michael",
                1,
                60
            );
        }

        /// <summary>
        /// Test AddJob method of Scheduler. Add 3 Jobs and test whether 
        /// they're all there in the correct order.
        /// </summary>
        [TestMethod]
        public void TestAddJob() {
            scheduler.AddJob(Job1);
            scheduler.AddJob(Job2);
            scheduler.AddJob(Job3);

            Assert.AreEqual(true, scheduler.HasJobs(), "Scheduler did not contain Jobs");

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual("job2", Jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("job1", Jobs[1].ToString(), "Job1 did not match");
            Assert.AreEqual("job3", Jobs[2].ToString(), "Job3 did not match");
        }

        /// <summary>
        /// Test RemoveJob method of Scheduler. Add 3 Jobs, remove 1 Job and
        /// test whether the two remaining Jobs are the correct ones in the 
        /// correct order.
        /// </summary>
        [TestMethod]
        public void TestRemoveJob() {
            scheduler.AddJob(Job1);
            scheduler.AddJob(Job2);
            scheduler.AddJob(Job3);

            scheduler.RemoveJob(Job2);

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual("job1", Jobs[0].ToString(), "Job1 did not match");
            Assert.AreEqual("job3", Jobs[1].ToString(), "Job3 did not match");
        }

        /// <summary>
        /// Test PopJob method of Scheduler. Add 3 Jobs, pop 1 Job and test 
        /// both whether the popped Job is the correct one and whether the 
        /// two remaining Jobs are the correct ones in the correct order.
        /// </summary>
        [TestMethod]
        public void TestPopJob() {
            scheduler.AddJob(Job1);
            scheduler.AddJob(Job2);
            scheduler.AddJob(Job3);

            Job Job = scheduler.PopJob();
            Assert.AreEqual("job1", Job.ToString(), "Popped Job did not match");

            Job[] Jobs = scheduler.GetJobs();
            Assert.AreEqual("job2", Jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("job3", Jobs[1].ToString(), "Job3 did not match");
        }
    }
}
