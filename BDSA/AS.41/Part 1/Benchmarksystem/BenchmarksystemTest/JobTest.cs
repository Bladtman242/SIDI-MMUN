using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmarksystem;

namespace BenchmarksystemTest {
    [TestClass]
    public class JobTest {
        /// <summary>
        /// Test creation of Job object. Create 3 Jobs and test whether 
        /// the Jobs has the correct content.
        /// </summary>
        [TestMethod]
        public void TestJob() {
            Job Job1 = new Job(
                p => { return 0; },
                1,
                "Michael",
                4,
                4
            );
            Job Job2 = new Job(
                p => { return 0; },
                2,
                "Michael",
                6,
                2
            );
            Job Job3 = new Job(
                p => { return 0; },
                3,
                "Michael",
                1,
                5
            );
            Assert.AreEqual("job1", Job1.ToString(), "Job did not match");
            Assert.AreEqual("job2", Job2.ToString(), "Job did not match");
            Assert.AreEqual("job3", Job3.ToString(), "Job did not match");
        }
    }
}
