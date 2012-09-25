using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    class BenchmarkSystem {
        private Scheduler scheduler = new Scheduler();
        private List<Job> running = new List<Job>();

        public event EventHandler Submitted;
        public event EventHandler Cancelled;
        public event EventHandler Running;
        public event EventHandler Terminated;
        public event EventHandler Failed;
        protected virtual void JobSubmitted(EventArgs e) {
            if (Submitted != null)
                Submitted(this, e);
        }
        protected virtual void JobCancelled(EventArgs e) {
            if (Cancelled != null)
                Cancelled(this, e);
        }
        protected virtual void JobRunning(EventArgs e) {
            if (Running != null)
                Running(this, e);
        }
        protected virtual void JobTerminated(EventArgs e) {
            if (Terminated != null)
                Terminated(this, e);
        }
        protected virtual void JobFailed(EventArgs e) {
            if (Failed != null)
                Failed(this, e);
        }

        /// <summary>
        /// Add a job to scheduler, change state of job and trigger event.
        /// </summary>
        /// <param name="job"></param>
        public void Submit(Job job) {
            scheduler.AddJob(job);
            job.State = State.Submitted;
            JobSubmitted(EventArgs.Empty);
        }

        /// <summary>
        /// Remove a job from scheduler, change state of job and trigger event.
        /// </summary>
        /// <param name="job">Job object</param>
        public void Cancel(Job job) {
            scheduler.RemoveJob(job);
            job.State = State.Cancelled;
            JobCancelled(EventArgs.Empty);
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
                Console.WriteLine(job.Owner.name + ", " + job.Cpus + ", " + job.ExpRuntime + ": " + job.State);
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
                JobRunning(EventArgs.Empty);

                job.Process(new[] { "Lars" });
                running.Remove(job);
                job.State = State.Terminated;
                JobTerminated(EventArgs.Empty);
            }
        }

        static void Main(string[] args) {
            BenchmarkSystem system = new BenchmarkSystem();
            Logger logger = new Logger(system);

            Job job1 = new Job(
                p => { Console.WriteLine("Job1 is running"); return 0; },
                4,
                40,
                new Owner { name = "Michael" }
            );
            Job job2 = new Job(
                p => { Console.WriteLine("Job2 is running"); return 0; }, 
                4, 
                5,
                new Owner { name = "Michael" }
            );
            Job job3 = new Job(
                p => { Console.WriteLine("Job3 is running"); return 0; },
                4,
                5,
                new Owner { name = "Michael" }
            );
            Job job4 = new Job(
                p => { Console.WriteLine("Job4 is running"); return 0; },
                6,
                10,
                new Owner { name = "Michael" }
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
