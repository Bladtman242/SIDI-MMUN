using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Benchmarksystem {
    class Benchmarksystem {
        private int cpus = 30;

        private Scheduler scheduler = new Scheduler();
        private Queue<Job> running = new Queue<Job>();
        private List<Job> runningList = new List<Job>();
        private Queue<Job> waiting1 = new Queue<Job>();
        private Queue<Job> waiting2 = new Queue<Job>();

        /// <summary>
        /// Add a job to scheduler.
        /// </summary>
        /// <param name="job"></param>
        public void Submit(Job job) {
            scheduler.AddJob(job, false);
        }

        /// <summary>
        /// Remove a job from scheduler.
        /// </summary>
        /// <param name="job">Job object</param>
        public void Cancel(Job job) {
            scheduler.RemoveJob(job);
        }

        /// <summary>
        /// Print a status including running and queued jobs.
        /// </summary>
        public void Status() {
            Console.WriteLine("Running jobs:");

            Console.WriteLine("");
            Job[] jobs = scheduler.GetJobs();
            Console.WriteLine("Queued jobs:");
            foreach (Job job in jobs) {
                Console.WriteLine(job.id + ": " + job.cpus + ", " + job.runtime + ": " + job.state);
            }
        }

        public void RunJob(Job job) {
            job.state = State.Running;
            DatabaseModule.LogAction(job, "running");
            int runtimeMs = (int) job.runtime * 1000;
            Thread.Sleep(runtimeMs);
            cpus += job.cpus;
            runningList.Remove(job);
            job.state = State.Terminated;
            DatabaseModule.LogAction(job, "terminated");
            Debug("terminated", job); // debug
            //job.Process(new[] { "Lars" });
        }

        /// <summary>
        /// Execute all jobs in the scheduler.
        /// </summary>
        public void ExecuteAll() {
            while (true) {
                if (scheduler.HasJobs()) {
                    if (waiting2.Count > 0) {
                        Job job = waiting2.Dequeue();
                        while (job.cpus > cpus) {
                            //
                        }
                        running.Enqueue(job);
                        runningList.Add(job);
                        cpus -= job.cpus;
                        Debug("running", job); // debug
                    } else {
                        try {
                            Job job = scheduler.PopJob(cpus);
                            if (job.cpus <= cpus) {
                                running.Enqueue(job);
                                runningList.Add(job);
                                cpus -= job.cpus;
                                Debug("running", job); // debug
                            } else {
                                if (job.wait == 0) {
                                    scheduler.AddJob(job, true);
                                } else if (job.wait == 1) {
                                    waiting2.Enqueue(job);
                                }
                                job.wait++;
                            }
                        } catch (InvalidOperationException e) {
                            // Do nothing
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Execute all jobs in the scheduler.
        /// </summary>
        public void RunAll() {
            while (true) {
                try {
                    RunJob(running.Dequeue());
                } catch (InvalidOperationException e) {
                    // Do nothing
                }
            }
        }

        public Scheduler GetScheduler() {
            return scheduler;
        }

        public void Debug(String action, Job job) {
            Console.WriteLine(String.Format("{0,10} ID:{1,3} Job:{2,2} Free:{3,2} Wait:{4}", action, job.id, job.cpus, cpus, job.wait));
        }

        static void Main(string[] args) {
            Benchmarksystem system = new Benchmarksystem();
            User user = new User(system.GetScheduler());

            Thread userThread = new Thread(new ThreadStart(user.Run));
            Thread executeThread = new Thread(new ThreadStart(system.ExecuteAll));
            Thread runningThread = new Thread(new ThreadStart(system.RunAll));

            try {
                userThread.Start();
                executeThread.Start();
                runningThread.Start();

                userThread.Join();
                executeThread.Join();
                runningThread.Join();
            } catch (ThreadStateException e) {
                Console.WriteLine(e);
            } catch (ThreadInterruptedException e) {
                Console.WriteLine(e);
            }
        }
    }
}
