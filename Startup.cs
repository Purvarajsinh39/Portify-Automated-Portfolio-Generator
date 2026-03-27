using System;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Portify.Startup))]

namespace Portify
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Hangfire to use SQL Server
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PortifyDB"].ConnectionString;
            
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new Hangfire.SqlServer.SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    UseRecommendedIsolationLevel = true
                });

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
