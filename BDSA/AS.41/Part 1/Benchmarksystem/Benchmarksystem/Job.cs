using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Benchmarksystem {
    public class Job {
        private Func<string[], int> process;
        internal int id;
        internal String owner;
        internal int cpus;
        internal double runtime;
        internal State state = State.New;
        internal DateTime timestamp;
        internal int wait = 0;

        /// <summary>
        /// Create a job.
        /// </summary>
        /// <param name="process">Method to be processed</param>
        /// <param name="cpus">int cpus (1-6)</param>
        /// <param name="expRuntime">int expected runtime (in minutes)</param>
        /// <param name="owner">Owner owner of the job</param>
        public Job(Func<string[], int> process, int id, String owner, int cpus, double runtime) {
            this.process = process;
            this.id = id;
            this.owner = owner;
            if (cpus >= 1 && cpus <= 10) {
                this.cpus = cpus;
            } else {
                throw new NotSupportedException("Between 1 and 10 CPUs");
            }
            this.runtime = runtime;
            this.timestamp = DateTime.Now;
            Thread.Sleep(1); // Ensure different timestamp
        }

        /// <summary>
        /// Return a string of the job.
        /// </summary>
        /// <returns>string "job" + id</returns>
        public override string ToString() {
            return "job" + id;
        }

        /// <summary>
        /// Run the process.
        /// </summary>
        /// <param name="args">string[] args</param>
        internal void Process(string[] args) {
            process(args);
        }
    }
}
