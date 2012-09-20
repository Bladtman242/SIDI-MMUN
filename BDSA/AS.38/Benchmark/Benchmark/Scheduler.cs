using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    class Scheduler {
        private Queue<Job> shortQ = new Queue<Job>();
        private Queue<Job> longQ = new Queue<Job>();
        private Queue<Job> veryLongQ = new Queue<Job>();

        public void AddJob(Job job) {
            if (job.ExpRuntime < 30) {
                shortQ.Enqueue(job);
            } else if (job.ExpRuntime <= 120) {
                longQ.Enqueue(job);
            } else {
                veryLongQ.Enqueue(job);
            }
        }

        public void RemoveJob(Job job) {
            if (job.ExpRuntime < 30) {
                shortQ = shortQ.Remove(job);
            } else if (job.ExpRuntime <= 120) {
                longQ = longQ.Remove(job);
            } else {
                veryLongQ = veryLongQ.Remove(job);
            }
        }

        public Job PopJob() {
            if (!HasJobs()) {
                throw new Exception("No jobs found, dude...");
            }

            List<Queue<Job>> list = new List<Queue<Job>>();
            if (shortQ.Count > 0) list.Add(shortQ);
            if (longQ.Count > 0) list.Add(longQ);
            if (veryLongQ.Count > 0) list.Add(veryLongQ);

            DateTime tmpTimestamp = DateTime.MaxValue;
            Queue<Job> tmpQueue = shortQ;
            foreach (Queue<Job> queue in list) {
                Job job = queue.Peek();
                if (tmpTimestamp > job.Timestamp) {
                    tmpTimestamp = job.Timestamp;
                    tmpQueue = queue;
                }
            }

            return tmpQueue.Dequeue();
        }

        public bool HasJobs() {
            return !(shortQ.Count == 0 && longQ.Count == 0 && veryLongQ.Count == 0);
        }

        public Job[] GetJobs() {
            var queues = shortQ.Concat(longQ).Concat(veryLongQ);
            return queues.ToArray();
        }
    }
}
