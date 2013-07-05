using System;
using System.Windows;
using System.Windows.Threading;

namespace BugFreak.Integration.WPF
{
    public class AgileReporter
    {
        public static void Hook()
        {
            var app = Application.Current;

            GlobalConfig.Settings.AppName = AppDomain.CurrentDomain.FriendlyName;
            BugFreak.AgileReporter.Init();
            app.Exit += OnExit;
            app.DispatcherUnhandledException += OnException;
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            BugFreak.AgileReporter.Instance.BeginReport(eventArgs.Exception);
            
            eventArgs.Handled = true;
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= OnException;
            app.Exit -= OnExit;
            BugFreak.AgileReporter.Dispose();
        }
    }
}
