using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmarksystem;

namespace BenchmarksystemTest {
    [TestClass]
    public class UserTest {
        [TestMethod]
        public void TestCreateRandomJob() {
            Scheduler scheduler = new Scheduler();

            User user = new User(scheduler);

            Job job1 = user.CreateRandomJob(1);
            Job job2 = user.CreateRandomJob(2);

            Assert.AreEqual("job1", job1.ToString(), "Job did not match");
            Assert.AreEqual("job2", job2.ToString(), "Job did not match");
        }
    }
}
