using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benchmark;

namespace BenchmarkTest {
    [TestClass]
    public class QueueExtensionsTest {

        /// <summary>
        /// Test the Remove method of QueueExtensions. Add 3 objects, test their presence, remove one and test again.
        /// </summary>
        [TestMethod]
        public void TestRemove() {
            Queue<String> queue = new Queue<String>();

            String first = "one";
            String second = "two";
            String third = "three";

            queue.Enqueue(first);
            queue.Enqueue(second);
            queue.Enqueue(third);

            Assert.AreEqual(true, queue.Contains(first), "Queue did not contain one");
            Assert.AreEqual(true, queue.Contains(second), "Queue did not contain two");
            Assert.AreEqual(true, queue.Contains(third), "Queue did not contain three");

            queue = queue.Remove(first);

            Assert.AreEqual(true, queue.Contains(second), "Queue did not contain two");
            Assert.AreEqual(true, queue.Contains(third), "Queue did not contain three");
        }
    }
}
