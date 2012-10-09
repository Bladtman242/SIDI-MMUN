using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    public class BenchmarkSystem {
        private Scheduler scheduler = new Scheduler();
        private List<Job> running = new List<Job>();

        /// <summary>
        /// Add a job to scheduler, change state of job and trigger event.
        /// </summary>
        /// <param name="job"></param>
        public void Submit(Job job) {
            scheduler.AddJob(job);
            job.State = State.Submitted;
            DatabaseModule.LogAction(job, "submitted");
        }

        /// <summary>
        /// Remove a job from scheduler, change state of job and trigger event.
        /// </summary>
        /// <param name="job">Job object</param>
        public void Cancel(Job job) {
            scheduler.RemoveJob(job);
            job.State = State.Cancelled;
            DatabaseModule.LogAction(job, "cancelled");
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
                Console.WriteLine(job.Name + ", " + job.Cpus + ", " + job.ExpRuntime + ": " + job.State);
            }
        }

        /// <summary>
        /// Execute all jobs in the scheduler.
        /// </summary>
        public void ExecuteAll() {
            while (scheduler.HasJobs()) {
                Job job = scheduler.PopJob();
                running.Add(job);
                job.State = State.Running;
                DatabaseModule.LogAction(job, "running");

                job.Process(new[] { "Lars" });
                running.Remove(job);
                job.State = State.Terminated;
                DatabaseModule.LogAction(job, "terminated");
            }
        }

        /// <summary>
        /// Get scheduler (only for testing purposes)
        /// </summary>
        /// <returns>Scheduler object</returns>
        public Scheduler GetScheduler() {
            return scheduler;
        }

        static void Main(string[] args) {
            BenchmarkSystem system = new BenchmarkSystem();

            Job job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                "job1",
                "Michael",
                4,
                40
            );

            Job job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; },
                "job2",
                "Michael",
                4, 
                5
            );

            Job job3 = new Job(
                p => { Console.WriteLine("Job3 is running"); return 0; },
                "job3",
                "Michael",
                4,
                5
            );

            Job job4 = new Job(
                p => { Console.WriteLine("Job4 is running"); return 0; },
                "job4",
                "Michael",
                6,
                10
            );

            system.Submit(job1);
            system.Submit(job2);
            system.Submit(job3);
            system.Submit(job4);

            system.Status();

            system.ExecuteAll();

            Console.ReadKey();
        }
    }
}
