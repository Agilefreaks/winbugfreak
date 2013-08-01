using System;
using BugFreak.Components;

namespace BugFreak
{
    public class AgileReporter : IReportingService
    {
        public static IReportingService Instance { get; internal set; }

        public static void Init()
        {
            Initializer.Initialize();
        }

        public void BeginReport(Exception exc)
        {
            var errorReport = CreateReport(exc);
            Queue(errorReport);
        }

        private ErrorReport CreateReport(Exception exc)
        {
            return ErrorReport.FromException(exc);
        }

        private void Queue(ErrorReport errorReport)
        {
            var errorReportQueue = GlobalConfig.ServiceLocator.GetService<IErrorReportQueue>();
            
            errorReportQueue.Enqueue(errorReport);
        }

        public static void Dispose()
        {
            GlobalConfig.ServiceLocator.GetService<IErrorReportQueueListener>().Dispose();
            GlobalConfig.ServiceLocator.GetService<IErrorReportHandler>().Dispose();

            Instance = null;
        }
    }
}