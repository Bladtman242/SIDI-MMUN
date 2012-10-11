using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    public partial class Job {
        private Func<string[], int> process;
        internal String Name;
        internal String Owner;
        internal int Cpus = 1;
        internal int ExpRuntime;
        internal State State = State.New;
        internal DateTime Timestamp;

        /// <summary>
        /// Create a job.
        /// </summary>
        /// <param name="process">Method to be processed</param>
        /// <param name="cpus">int cpus (1-6)</param>
        /// <param name="expRuntime">int expected runtime (in minutes)</param>
        /// <param name="owner">Owner owner of the job</param>
        public Job(Func<string[], int> process, String name, String owner, int cpus, int expRuntime) {
            this.process = process;
            Name = name;
            Owner = owner;
            if (cpus < 7 && cpus > 0) {
                Cpus = cpus;
            } else {
                throw new NotSupportedException("Between 1 and 6 cpus, damnet!");
            }
            ExpRuntime = expRuntime;
            Timestamp = DateTime.Now;
            System.Threading.Thread.Sleep(1); // Ensure different timestamp
        }

        public Job() {

        }

        /// <summary>
        /// Return a string defining the object.
        /// </summary>
        /// <returns>String containing Cpus, ExpRuntime and Owner name</returns>
        public override String ToString() {
            return Name;
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
