using System;
using AutoHub.BusinessLogic.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace AutoHub.API.Extensions;

public static class AddQuartzExtension
{
    public static void AddQuartz(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey(nameof(LotWinnerDeterminantJob));

            options.AddJob<LotWinnerDeterminantJob>(opts => opts.WithIdentity(jobKey));
            options.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{jobKey}Trigger")
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(1))
                    .RepeatForever()));
        });
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}
