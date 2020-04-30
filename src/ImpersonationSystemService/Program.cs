using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Topshelf;

namespace ImpersonationSystemService
{
    class Program
    {
        private static readonly ILogger Log = LogManager.GetLogger(nameof(Program));

        static int Main(string[] args)
        {
            try
            {
                Log.Info("Program starting with args {@args}", args);
                var rc = HostFactory.Run(configurator =>
                {
                    configurator.UseNLog();
                    configurator.Service<MainService>(service =>
                    {
                        service.ConstructUsing(name => new MainService());
                        service.WhenStarted(s => s.Start());
                        service.WhenStopped(s => s.Stop());
                    });

                    // Log.Debug("Running as local service.");
                    // configurator.RunAsLocalService();
                    Log.Debug("Running as local system.");
                    configurator.RunAsLocalSystem();

                    configurator.SetDescription("A Notepad launcher");
                    configurator.SetDisplayName("A Notepad launcher");
                    configurator.SetServiceName("ANotepadLauncher");
                });

                var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
                Environment.ExitCode = exitCode;
                Log.Info("Exiting with {code} {codeName}", exitCode, rc.ToString());
                return Environment.ExitCode;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return -1;
            }
        }
    }
}
