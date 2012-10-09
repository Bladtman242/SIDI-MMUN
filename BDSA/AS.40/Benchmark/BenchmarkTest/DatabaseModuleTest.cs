using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmark;

namespace BenchmarkTest {
    [TestClass]
    public class DatabaseModuleTest {
        [TestMethod]
        public void TestSelectAllUsers() {
            String[] users = DatabaseModule.SelectAllUsers();
            Assert.AreEqual("Michael", users[0]);
        }

        [TestMethod]
        public void TestSelectAllJobs() {
            String[] jobs = DatabaseModule.SelectAllJobs("Michael");
            Assert.AreEqual("job1", jobs[0]);
            Assert.AreEqual("job2", jobs[1]);
            Assert.AreEqual("job3", jobs[2]);
            Assert.AreEqual("job4", jobs[3]);


            String[] jobs2 = DatabaseModule.SelectAllJobs("Michael", new DateTime(2012, 09, 30));
            Assert.AreEqual("job1", jobs2[0]);
            Assert.AreEqual("job2", jobs2[1]);
            Assert.AreEqual("job3", jobs2[2]);
            Assert.AreEqual("job4", jobs2[3]);


            String[] jobs3 = DatabaseModule.SelectAllJobs("Michael", new DateTime(2012, 09, 30), new DateTime(2012, 10, 10));
            Assert.AreEqual("job1", jobs3[0]);
            Assert.AreEqual("job2", jobs3[1]);
            Assert.AreEqual("job3", jobs3[2]);
            Assert.AreEqual("job4", jobs3[3]);

            String[] jobs4 = DatabaseModule.SelectAllJobs("Michael", new DateTime(2012, 12, 10));
            Assert.AreEqual(0, jobs4.Length);
        }

        [TestMethod]
        public void TestSelectJobStatuses() {
            char[] array = { ':' };

            String[] jobStatuses = DatabaseModule.SelectJobStatuses(new DateTime(2012, 09, 30), new DateTime(2012, 10, 10));
            String[] cancelled = jobStatuses[0].Split(array);
            Assert.AreEqual("cancelled", cancelled[0]);
            String[] running = jobStatuses[1].Split(array);
            Assert.AreEqual("running", running[0]);
            String[] submitted = jobStatuses[2].Split(array);
            Assert.AreEqual("submitted", submitted[0]);
            String[] terminated = jobStatuses[3].Split(array);
            Assert.AreEqual("terminated", terminated[0]);

            String[] jobStatuses2 = DatabaseModule.SelectJobStatuses("Michael", new DateTime(2012, 09, 30), new DateTime(2012, 10, 10));
            String[] cancelled2 = jobStatuses[0].Split(array);
            Assert.AreEqual("cancelled", cancelled2[0]);
            String[] running2 = jobStatuses[1].Split(array);
            Assert.AreEqual("running", running2[0]);
            String[] submitted2 = jobStatuses[2].Split(array);
            Assert.AreEqual("submitted", submitted2[0]);
            String[] terminated2 = jobStatuses[3].Split(array);
            Assert.AreEqual("terminated", terminated2[0]);
        }
    }
}
