using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;//v2.3.3 
using Quartz.Impl;

namespace Birthday.BLL
{
    //For Scheduler, we have used v2.3.3 of quartz
    public class NotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //For Birthday, Get Employees with today's birth day
            //Get List of Recipients of employee's
            //
        }

        public void SendNotificationMails(bool isTest = true)
        {

        }
    }

    public class Triggers
    {
        public static ITrigger TimeTrigger()
        {
            return TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule
                 (s =>
                    s.WithIntervalInHours(24)
                   .OnEveryDay()
                   .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                 )
               .Build();
        }

        public static ITrigger CRONTrigger()
        {
            return TriggerBuilder.Create()

                .WithDailyTimeIntervalSchedule
                 (s =>
                    s.WithIntervalInHours(24)
                   .OnEveryDay()
                   .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 30))
                 )
               .Build();
            //.WithCronSchedule("At 9:30am every Monday through Friday", s => s.WithMisfireHandlingInstructionDoNothing())
        }
    }

    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<NotificationJob>().Build();
            // job.JobDataMap.Add("ftp.location", "ftp://SomeFileLocation");

            scheduler.ScheduleJob(job, Triggers.TimeTrigger());
        }
    }
}
