using System.Diagnostics;
using System.ServiceProcess;
using ImpersonationSystemService.WindowsApi;
using NLog;

namespace ImpersonationSystemService
{
    public class MainService : ServiceBase
    {
        public MainService()
        {
            this.ServiceName = "MainService";
        }

        private static readonly ILogger Log = LogManager.GetLogger(nameof(MainService));

        public void Start()
        {
            var process = "notepad";
            Log.Info("Starting main service and executing {process}", process);
            // Process.Start(process);
            ProcessAsUser.Launch(process);
        }
    }
}