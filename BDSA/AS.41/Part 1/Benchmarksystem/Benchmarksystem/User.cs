using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Benchmarksystem {
    public class User {
        Random random = new Random();
        private Scheduler scheduler;
        private int nextId = 1;

        public User(Scheduler scheduler) {
            this.scheduler = scheduler;
        }

        public Job CreateRandomJob(int id) {
            String[] owners = { "Sigurt", "Michael", "God", "Homer" };
            double runtime = random.Next(1, 50);
            return new Job(
                p => { return 0; },
                id,
                owners[random.Next(0, 3)],
                random.Next(1, 10),
                runtime / 10
            );
        }

        private void SubmitRandomJob() {
            Job job = CreateRandomJob(nextId++);
            try {
                scheduler.AddJob(job, false);
            } catch (NotSupportedException e) {
                Console.WriteLine(e.Message + ": " + job.runtime);
            }
        }

        internal void Run() {
            while (true) {
                SubmitRandomJob();
                Thread.Sleep(random.Next(100, 1000));
            }
        }
    }
}
