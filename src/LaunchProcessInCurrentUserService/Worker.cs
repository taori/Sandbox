using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImpersonationSystemService.WindowsApi;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LaunchProcessInCurrentUserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ProcessImpersonation.Launch("notepad");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                using (_logger.BeginScope("Worker cycle."))
                {
                    _logger.LogInformation(new EventId(1),"Cycle started {Time}", DateTimeOffset.Now);
                    await Task.Delay(5000, stoppingToken);
                    _logger.LogInformation(new EventId(1),"Cycle done.");
                }
            }
        }
    }
}