using System;
using System.Threading;

namespace BugFreak
{
    using System.Windows;
    using System.Windows.Threading;

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
            GlobalConfig.ErrorDataProviders.Add(new WpfErrorDataProvider());

            app.Exit += OnExit;
            app.DispatcherUnhandledException += AppOnDispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainUnhandledException;
        }

        private static void AppOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            OnException(sender, dispatcherUnhandledExceptionEventArgs.Exception);
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            OnException(sender, (Exception)unhandledExceptionEventArgs.ExceptionObject);
            Thread.Sleep(3000);
        }

        private static void OnException(object sender, Exception exception)
        {
            ReportingService.Instance.BeginReport(exception);
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= AppOnDispatcherUnhandledException;
            app.Exit -= OnExit;
            ReportingService.Dispose();
        }
    }
}
