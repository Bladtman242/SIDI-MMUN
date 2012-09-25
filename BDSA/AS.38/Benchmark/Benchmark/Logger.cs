using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    public class Logger {
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

        /// <summary>
        /// Log a job submission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJobSubmitted(object sender, EventArgs e) {
            Console.WriteLine("Job submitted.");
        }

        /// <summary>
        /// Log a job cancellation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJobCancelled(object sender, EventArgs e) {
            Console.WriteLine("Job cancelled.");
        }

        /// <summary>
        /// Log a job running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJobRunning(object sender, EventArgs e) {
            Console.WriteLine("Job running.");
        }

        /// <summary>
        /// Log a job termination.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJobTerminated(object sender, EventArgs e) {
            Console.WriteLine("Job terminated.");
        }

        /// <summary>
        /// Log a job failure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJobFailed(object sender, EventArgs e) {
            Console.WriteLine("Job failed.");
        }
    }
}
