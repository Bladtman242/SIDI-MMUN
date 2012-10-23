using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Benchmarksystem {
    public class Benchmarksystem {
        private readonly object _cpuLocker = new object();
        private readonly object _runningListLocker = new object();
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
            Console.WriteLine("##########");
            Console.WriteLine("Running jobs:");
            int cpusInUse = 0;
            lock (_runningListLocker) {
                foreach (Job job in runningList) {
                    cpusInUse += job.cpus;
                    Console.WriteLine(job + ": " + job.cpus + " CPUs");
                }
            }
            Console.WriteLine("CPUs in use: " + cpusInUse);

            Console.WriteLine("");
            Job[] jobs = scheduler.GetJobs();
            Console.WriteLine("Queued jobs:");
            foreach (Job job in jobs) {
                Console.WriteLine(job + ": " + job.cpus + " CPUs");
            }
            Console.WriteLine("##########");
        }

        /// <summary>
        /// Print status every 1 second.
        /// </summary>
        public void PrintStatus() {
            while (true) {
                Status();
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Run each job.
        /// </summary>
        /// <param name="job"></param>
        public void RunJob(Job job) {
            job.state = State.Running;
            //DatabaseModule.LogAction(job, "running");
            int runtimeMs = (int) job.runtime * 1000;
            Thread.Sleep(runtimeMs);
            lock (_cpuLocker) {
                cpus += job.cpus;
            }
            runningList.Remove(job);
            job.state = State.Terminated;
            //DatabaseModule.LogAction(job, "terminated");
        }

        /// <summary>
        /// Execute all jobs in the scheduler.
        /// </summary>
        public void ExecuteAll() {
            while (true) {
                Execute();
            }
        }

        /// <summary>
        /// Execute a single job in the scheduler.
        /// </summary>
        public void Execute() {
            if (scheduler.HasJobs()) {
                if (waiting2.Count > 0) {
                    Job job = waiting2.Dequeue();
                    while (job.cpus > cpus) {
                        // Wait until 
                    }
                    running.Enqueue(job);
                    lock (_runningListLocker) {
                        runningList.Add(job);
                    }
                    lock (_cpuLocker) {
                        cpus -= job.cpus;
                    }
                } else {
                    try {
                        Job job = scheduler.PopJob(cpus);
                        if (job.cpus <= cpus) {
                            running.Enqueue(job);
                            lock (_runningListLocker) {
                                runningList.Add(job);
                            }
                            lock (_cpuLocker) {
                                cpus -= job.cpus;
                            }
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

        /// <summary>
        /// Run all jobs in the running queue.
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

        static void Main(string[] args) {
            Benchmarksystem system = new Benchmarksystem();
            User user = new User(system.GetScheduler());

            Thread userThread = new Thread(new ThreadStart(user.Run));
            Thread executeThread = new Thread(new ThreadStart(system.ExecuteAll));
            Thread runningThread = new Thread(new ThreadStart(system.RunAll));
            Thread statusThread = new Thread(new ThreadStart(system.PrintStatus));

            try {
                userThread.Start();
                executeThread.Start();
                runningThread.Start();
                statusThread.Start();

                userThread.Join();
                executeThread.Join();
                runningThread.Join();
                statusThread.Join();
            } catch (ThreadStateException e) {
                Console.WriteLine(e);
            } catch (ThreadInterruptedException e) {
                Console.WriteLine(e);
            }
        }
    }
}
