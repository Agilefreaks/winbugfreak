namespace BugFreak
{
    using System;
    using System.Windows;

    using global::BugFreak.Components;

    public class BugFreak
    {
        /// <summary>
        /// User Hook don't instantiate this class
        /// </summary>
        private BugFreak()
        {
        }

        public static void Hook(string apiKey, string token, Application app)
        {
            GlobalConfig.ApiKey = apiKey;
            GlobalConfig.Token = token;

            ReportingService.Init();
            GlobalConfig.ErrorDataProviders.Add(new SilverlightErrorDataProvider());

            app.Exit += OnExit;
            app.UnhandledException += OnException;
        }

        private static void OnException(object sender, ApplicationUnhandledExceptionEventArgs eventArgs)
        {
            ReportingService.Instance.BeginReport(eventArgs.ExceptionObject);
        }

        private static void OnExit(object sender, EventArgs eventArgs)
        {
            var app = Application.Current;

            app.UnhandledException -= OnException;
            app.Exit -= OnExit;
            ReportingService.Dispose();
        }
    }
}
