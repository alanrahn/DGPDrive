using System;
using System.ServiceProcess;

namespace DGPAutoWork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                DGPAutoWorkSvc ConsoleDGPService = new DGPAutoWorkSvc();
                ConsoleDGPService.ConsoleStartAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DGPAutoWorkSvc()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
