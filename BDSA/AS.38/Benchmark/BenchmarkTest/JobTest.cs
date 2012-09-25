using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmark;

namespace BenchmarkTest {
    [TestClass]
    public class JobTest {
        /// <summary>
        /// Test creation of Job object. Create 3 jobs and test whether 
        /// the jobs has the correct content.
        /// </summary>
        [TestMethod]
        public void TestJob() {
            Job job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                4,
                40,
                new Owner { name = "Michael" }
            );
            Job job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                6,
                20,
                new Owner { name = "Sigurt" }
            );
            Job job3 = new Job(
                p => { Console.WriteLine("Job3 is running"); return 0; },
                1,
                60,
                new Owner { name = "Dude" }
            );
            Assert.AreEqual("4,40,Michael", job1.ToString(), "Job did not match");
            Assert.AreEqual("6,20,Sigurt", job2.ToString(), "Job did not match");
            Assert.AreEqual("1,60,Dude", job3.ToString(), "Job did not match");
        }
    }
}
