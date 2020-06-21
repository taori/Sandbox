using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

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
                    services.AddHostedService<Worker>()
                        .Configure<EventLogSettings>(els =>
                        {
                            els.LogName = "CurrentUserService";
                            els.SourceName = "CurrentUserService Source";
                        });
                });
    }
}