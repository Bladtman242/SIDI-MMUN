using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Benchmark {
    public class DatabaseModule {
        public static void LogAction(Job job, String action) {
            using (var dbContext = new Model1Container()) {
                Log log = new Log();
                log.Action = action;
                log.JobName = job.Name;
                log.OwnerName = job.Owner;
                log.Time = DateTime.Now;

                dbContext.Logs.AddObject(log);
                dbContext.SaveChanges();
            }
        }

        public static String[] SelectAllUsers() {
            using (var dbContext = new Model1Container()) {
                var users = (from log in dbContext.Logs
                             select log.OwnerName).Distinct();
                return users.ToArray();
            }
        }

        public static String[] SelectAllJobs(String user) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.OwnerName == user
                            select log.JobName).Distinct();
                return jobs.ToArray();
            }
        }

        public static String[] SelectAllJobs(String user, DateTime fromDate) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.OwnerName == user && log.Action == "submitted" && log.Time > fromDate
                            select log.JobName).Distinct();
                return jobs.ToArray();
            }
        }

        public static String[] SelectAllJobs(String user, DateTime fromDate, DateTime toDate) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.OwnerName == user && log.Action == "submitted" && log.Time > fromDate && log.Time < toDate
                            select log.JobName).Distinct();
                return jobs.ToArray();
            }
        }

        public static String[] SelectJobStatuses(DateTime fromDate, DateTime toDate) {
            using (var dbContext = new Model1Container()) {
                var actions = from c in dbContext.Logs
                              where c.Time > fromDate && c.Time < toDate
                              group c by c.Action into g
                              select new { g.Key, Count = g.Count() };
                List<String> list = new List<String>();
                foreach (var action in actions) {
                    list.Add(action.Key + ": " + action.Count);
                }

                return list.ToArray();
            }
        }

        public static String[] SelectJobStatuses(String user, DateTime fromDate, DateTime toDate) {
            using (var dbContext = new Model1Container()) {
                var actions = from c in dbContext.Logs
                              where c.OwnerName == user && c.Time > fromDate && c.Time < toDate
                              group c by c.Action into g
                              select new { g.Key, Count = g.Count() };
                List<String> list = new List<String>();
                foreach (var action in actions) {
                    list.Add(action.Key + ": " + action.Count);
                }

                return list.ToArray();
            }
        }
    }
}
