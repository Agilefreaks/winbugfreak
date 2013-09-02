namespace BugFreak
{
    using System;
    using global::BugFreak.WinRT.Components;
    using Windows.UI.Xaml;
    
    public static class BugFreak
    {
        private static Application _app;

        public static void Hook(string apiKey, string token, Application app)
        {
            GlobalConfig.ApiKey = apiKey;
            GlobalConfig.Token = token;

            GlobalConfig.ErrorDataProviders.Add(new PackageInfoProvier());
            ReportingService.Init();

            _app = app;
            _app.UnhandledException += OnException;
        }

        private static void OnException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var exception = eventArgs.Exception;
            exception.Data.Add("Message", eventArgs.Message);

            // set as handled to allow reporting error and on complete shut down app
            ReportingService.Instance.BeginReport(exception, OnReportCompleted);
            eventArgs.Handled = true;
        }

        private static void OnReportCompleted(Exception exc, bool reported)
        {
            _app.Exit();
        }
    }
}
