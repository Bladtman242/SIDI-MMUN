using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    class Logger {
        private BenchmarkSystem System;

        /// <summary>
        /// Add event handlers for each state and assign them to methods in this class.
        /// </summary>
        /// <param name="system">BenchmarkSystem object</param>
        public Logger(BenchmarkSystem system) {
            System = system;
            System.Submitted += new EventHandler(OnJobSubmitted);
            System.Cancelled += new EventHandler(OnJobCancelled);
            System.Running += new EventHandler(OnJobRunning);
            System.Terminated += new EventHandler(OnJobTerminated);
            System.Failed += new EventHandler(OnJobFailed);
        }

        public void OnJobSubmitted(object sender, EventArgs e) {
            Console.WriteLine("Job submitted.");
        }

        public void OnJobCancelled(object sender, EventArgs e) {
            Console.WriteLine("Job cancelled.");
        }

        public void OnJobRunning(object sender, EventArgs e) {
            Console.WriteLine("Job running.");
        }

        public void OnJobTerminated(object sender, EventArgs e) {
            Console.WriteLine("Job terminated.");
        }

        public void OnJobFailed(object sender, EventArgs e) {
            Console.WriteLine("Job failed.");
        }
    }
}
