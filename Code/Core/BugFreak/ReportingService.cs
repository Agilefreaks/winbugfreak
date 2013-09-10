namespace BugFreak
{
    using System;
    using global::BugFreak.Components;

    public class ReportingService : IReportingService
    {
        private static IErrorHandler errorHandler;

        public static IErrorHandler ErrorHandler
        {
            get
            {
                return errorHandler ?? (errorHandler = GlobalConfig.ServiceLocator.GetService<IErrorHandler>());
            }

            internal set
            {
                errorHandler = value;
            }
        }

        public static IReportingService Instance { get; internal set; }

        public static void Init()
        {
            Initializer.Initialize();
        }

        public void BeginReport(Exception exc)
        {
            BeginReport(exc, null);
        }

        public void BeginReport(Exception exc, ReportCompletedCallback completeCallback)
        {
            ErrorHandler.Handle(exc, completeCallback);
        }

        public static void Dispose()
        {
            ErrorHandler.Dispose();
            ErrorHandler = null;
            Instance = null;
        }
    }
}