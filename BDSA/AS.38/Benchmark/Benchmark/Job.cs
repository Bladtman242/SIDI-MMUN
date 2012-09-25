using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {

    class Job {
        private Func<string[], int> process;
        internal int Cpus = 1;
        internal int ExpRuntime;
        internal State State = State.New;
        internal Owner Owner;
        internal DateTime Timestamp;

        /// <summary>
        /// Create a job.
        /// </summary>
        /// <param name="process">Method to be processed</param>
        /// <param name="cpus">int cpus (1-6)</param>
        /// <param name="expRuntime">int expected runtime (in minutes)</param>
        /// <param name="owner">Owner owner of the job</param>
        internal Job(Func<string[], int> process, int cpus, int expRuntime, Owner owner) {
            this.process = process;
            if (cpus < 7 && cpus > 0) {
                Cpus = cpus;
            }
            ExpRuntime = expRuntime;
            Owner = owner;
            Timestamp = DateTime.Now;
            System.Threading.Thread.Sleep(1); // Ensure different timestamp
        }

        internal void Process(string[] args) {
            process(args);
        }
    }
}
