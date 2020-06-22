using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NLog.Extensions.Logging;

namespace LaunchProcessInCurrentUserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // see https://web.archive.org/web/20200423120541/https://csharp.christiannagel.com/2019/10/15/windowsservice/
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureLogging(builder =>
                {
                    builder.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure =>
                    {
                        configure.AddEventLog();
                        configure.AddDebug();
                        configure.AddNLog();
                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            configure.AddConsole();
                        }
                    });
                    
                    services.AddHostedService<Worker>()
                        .Configure<EventLogSettings>(els =>
                        {
                            els.LogName = hostContext.Configuration.GetValue<string>("Application:EventLogApplicationName") ?? "CurrentUserService";
                            els.SourceName = hostContext.Configuration.GetValue<string>("Application:EventLogSource") ?? "CurrentUserService Source";
                        });
                });
    }
}