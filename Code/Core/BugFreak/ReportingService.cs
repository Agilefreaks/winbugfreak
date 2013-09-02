namespace BugFreak
{
    using System;
    using global::BugFreak.Components;

    public class ReportingService : IReportingService
    {
        private static IErrorHandler _errorHandler;

        public static IReportingService Instance { get; internal set; }

        public static void Init()
        {
            Initializer.Initialize();

            _errorHandler = GlobalConfig.ServiceLocator.GetService<IErrorHandler>();
        }

        public void BeginReport(Exception exc)
        {
            BeginReport(exc, null);
        }

        public void BeginReport(Exception exc, ReportCompletedCallback completeCallback)
        {
            _errorHandler.Handle(exc, completeCallback);
        }

        public static void Dispose()
        {
            _errorHandler.Dispose();
            _errorHandler = null;
            Instance = null;
        }
    }
}