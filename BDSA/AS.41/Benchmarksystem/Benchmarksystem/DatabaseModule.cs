using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Benchmarksystem {
    public class DatabaseModule {
        public static void LogAction(Job job, String action) {
            using (var dbContext = new Model1Container()) {
                Log log = new Log();
                log.Action = action;
                log.JobId = job.id;
                log.Owner = job.owner;
                log.Timestamp = DateTime.Now;

                dbContext.Logs.AddObject(log);
                dbContext.SaveChanges();
            }
        }

        public static String[] SelectAllUsers() {
            using (var dbContext = new Model1Container()) {
                var users = (from log in dbContext.Logs
                             select log.Owner).Distinct();
                return users.ToArray();
            }
        }

        public static int[] SelectAllJobs(String user) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.Owner == user
                            select log.JobId).Distinct();
                return jobs.ToArray();
            }
        }

        public static int[] SelectAllJobs(String user, DateTime fromDate) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.Owner == user && log.Action == "submitted" && log.Timestamp > fromDate
                            select log.JobId).Distinct();
                return jobs.ToArray();
            }
        }

        public static int[] SelectAllJobs(String user, DateTime fromDate, DateTime toDate) {
            using (var dbContext = new Model1Container()) {
                var jobs = (from log in dbContext.Logs
                            where log.Owner == user && log.Action == "submitted" && log.Timestamp > fromDate && log.Timestamp < toDate
                            select log.JobId).Distinct();
                return jobs.ToArray();
            }
        }

        public static String[] SelectJobStatuses(DateTime fromDate, DateTime toDate) {
            using (var dbContext = new Model1Container()) {
                var actions = from c in dbContext.Logs
                              where c.Timestamp > fromDate && c.Timestamp < toDate
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
                              where c.Owner == user && c.Timestamp > fromDate && c.Timestamp < toDate
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
