using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmarksystem {
    public class Scheduler {
        private Queue<Job> shortQ = new Queue<Job>();
        private Queue<Job> longQ = new Queue<Job>();
        private Queue<Job> veryLongQ = new Queue<Job>();

        /// <summary>
        /// Adds a job to the correct queue. Correct queue is based on job length.
        /// </summary>
        /// <param name="job">Job object</param>
        public void AddJob(Job job, bool silent) {
            if (job.runtime >= 0.1 && job.runtime <= 0.5) {
                shortQ.Enqueue(job);
                if (!silent) {
                    job.state = State.Submitted;
                    //DatabaseModule.LogAction(job, "submitted");
                }
            } else if (job.runtime >= 0.5 && job.runtime <= 2) {
                longQ.Enqueue(job);
                if (!silent) {
                    job.state = State.Submitted;
                    //DatabaseModule.LogAction(job, "submitted");
                }
            } else if (job.runtime >= 2.1 && job.runtime <= 5) {
                veryLongQ.Enqueue(job);
                if (!silent) {
                    job.state = State.Submitted;
                    //DatabaseModule.LogAction(job, "submitted");
                }
            } else {
                throw new NotSupportedException("Not a supported runtime");
            }
        }

        /// <summary>
        /// Remove a job from the correct queue. Correct queue is based on job length.
        /// </summary>
        /// <param name="job">Job object</param>
        public void RemoveJob(Job job) {
            if (job.runtime >= 0.1 && job.runtime <= 0.5) {
                shortQ = shortQ.Remove(job);
                job.state = State.Cancelled;
                //DatabaseModule.LogAction(job, "cancelled");
            } else if (job.runtime >= 0.5 && job.runtime <= 2) {
                longQ = longQ.Remove(job);
                job.state = State.Cancelled;
                //DatabaseModule.LogAction(job, "cancelled");
            } else if (job.runtime >= 2.1 && job.runtime <= 5) {
                veryLongQ = veryLongQ.Remove(job);
                job.state = State.Cancelled;
                //DatabaseModule.LogAction(job, "cancelled");
            } else {
                throw new NotSupportedException("Not a supported runtime");
            }
        }

        /// <summary>
        /// Get the job inserted first. Based on timestamp attribute in Job object.
        /// </summary>
        /// <returns>Job object</returns>
        public Job PopJob() {
            List<Queue<Job>> list = new List<Queue<Job>>();
            if (shortQ.Count > 0) list.Add(shortQ);
            if (longQ.Count > 0) list.Add(longQ);
            if (veryLongQ.Count > 0) list.Add(veryLongQ);

            DateTime tmpTimestamp = DateTime.MaxValue;
            Queue<Job> tmpQueue = shortQ;
            foreach (Queue<Job> queue in list) {
                Job job = queue.Peek();
                if (tmpTimestamp > job.timestamp) {
                    tmpTimestamp = job.timestamp;
                    tmpQueue = queue;
                }
            }

            Job poppedJob = tmpQueue.Dequeue();
            if (poppedJob == null) {
                throw new InvalidOperationException("No eligible job found");
            }

            return poppedJob;
        }

        /// <summary>
        /// Return bool based on containing jobs or not in either queue.
        /// </summary>
        /// <returns>bool</returns>
        public bool HasJobs() {
            return !(shortQ.Count == 0 && longQ.Count == 0 && veryLongQ.Count == 0);
        }

        /// <summary>
        /// Return an array containing all jobs from all queues.
        /// </summary>
        /// <returns>Array of Job objects</returns>
        public Job[] GetJobs() {
            var queues = shortQ.Concat(longQ).Concat(veryLongQ);
            return queues.ToArray();
        }
    }
}