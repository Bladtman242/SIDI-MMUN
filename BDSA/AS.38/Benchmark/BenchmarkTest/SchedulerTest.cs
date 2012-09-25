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
        private Job job1;
        private Job job2;
        private Job job3;

        /// <summary>
        /// Create some dummy jobs.
        /// </summary>
        public SchedulerTest() {
            job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                4,
                40,
                new Owner { name = "Michael" }
            );
            job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                3,
                20,
                new Owner { name = "Michael" }
            );
            job3 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                1,
                60,
                new Owner { name = "Michael" }
            );
        }

        /// <summary>
        /// Test AddJob method of Scheduler. Add 3 jobs and test whether 
        /// they're all there in the correct order.
        /// </summary>
        [TestMethod]
        public void TestAddJob() {
            scheduler.AddJob(job1);
            scheduler.AddJob(job2);
            scheduler.AddJob(job3);

            Assert.AreEqual(true, scheduler.HasJobs(), "Scheduler did not contain jobs");

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual("3,20,Michael", jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("4,40,Michael", jobs[1].ToString(), "Job1 did not match");
            Assert.AreEqual("1,60,Michael", jobs[2].ToString(), "Job3 did not match");
        }

        /// <summary>
        /// Test RemoveJob method of Scheduler. Add 3 jobs, remove 1 job and
        /// test whether the two remaining jobs are the correct ones in the 
        /// correct order.
        /// </summary>
        [TestMethod]
        public void TestRemoveJob() {
            scheduler.AddJob(job1);
            scheduler.AddJob(job2);
            scheduler.AddJob(job3);

            scheduler.RemoveJob(job2);

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual("4,40,Michael", jobs[0].ToString(), "Job1 did not match");
            Assert.AreEqual("1,60,Michael", jobs[1].ToString(), "Job3 did not match");
        }

        /// <summary>
        /// Test PopJob method of Scheduler. Add 3 jobs, pop 1 job and test 
        /// both whether the popped job is the correct one and whether the 
        /// two remaining jobs are the correct ones in the correct order.
        /// </summary>
        [TestMethod]
        public void TestPopJob() {
            scheduler.AddJob(job1);
            scheduler.AddJob(job2);
            scheduler.AddJob(job3);

            Job job = scheduler.PopJob();
            Assert.AreEqual("4,40,Michael", job.ToString(), "Popped job did not match");

            Job[] jobs = scheduler.GetJobs();
            Assert.AreEqual("3,20,Michael", jobs[0].ToString(), "Job2 did not match");
            Assert.AreEqual("1,60,Michael", jobs[1].ToString(), "Job3 did not match");
        }
    }
}
