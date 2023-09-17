using Hangfire;
using TaskManager.BLL.Interface.WorkerServices;

namespace TaskManager.Api.Extensions
{
    public static class HangFireConfig
    {

// ...


    public static void ConfigureJobs(this IApplicationBuilder app, IWebHostEnvironment env)
    {
       
        app.UseHangfireDashboard("/dashboard");

        // Add Hangfire server
        app.UseHangfireServer();

            // Configure background job tasks here
            string jobId = BackgroundJob.Enqueue<INotificationWorkerService>(x => x.SendReminders());
            string contJobId = BackgroundJob.ContinueJobWith<INotificationWorkerService>(jobId, x => x.SendReminders());
            RecurringJob.AddOrUpdate<INotificationWorkerService>(contJobId, x => x.SendReminders(), "0 */4 * * *");

        }

    }
}
