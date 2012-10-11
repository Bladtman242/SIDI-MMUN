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
        /// Test creation of Job object. Create 3 Jobs and test whether 
        /// the Jobs has the correct content.
        /// </summary>
        [TestMethod]
        public void TestJob() {
            Job Job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                "job1",
                "Michael",
                4,
                40
            );
            Job Job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                "job2",
                "Michael",
                6,
                20
            );
            Job Job3 = new Job(
                p => { Console.WriteLine("Job3 is running"); return 0; },
                "job3",
                "Michael",
                1,
                60
            );
            Assert.AreEqual("job1", Job1.ToString(), "Job did not match");
            Assert.AreEqual("job2", Job2.ToString(), "Job did not match");
            Assert.AreEqual("job3", Job3.ToString(), "Job did not match");
        }
    }
}
