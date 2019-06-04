using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AnlandProject.Backend.Batch
{
    public class JobScheduler
    {
        public static async Task Start()
        {
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PWDExpiredWarning>()
                .WithIdentity("myJob", "group1")
                .Build();

            ////Example: Trigger the job to run now, and then every 40 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(40)
            //        //.WithIntervalInMinutes(5)
            //        .RepeatForever())
            //.Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("myTrigger", "group1")
            .WithCronSchedule("0 0 5 * * ?")
            .ForJob("myJob", "group1")
            .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}