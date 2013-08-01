namespace BugFreak.Silverlight
{
    using System;
    using System.Windows;

    public class AgileReporter
    {
        public static void Hook()
        {
            var app = Application.Current;

            BugFreak.AgileReporter.Init();
            app.Exit += OnExit;
            app.UnhandledException += OnException;
        }

        private static void OnException(object sender, ApplicationUnhandledExceptionEventArgs eventArgs)
        {
            BugFreak.AgileReporter.Instance.BeginReport(eventArgs.ExceptionObject);

            eventArgs.Handled = true;
        }

        private static void OnExit(object sender, EventArgs eventArgs)
        {
            var app = Application.Current;

            app.UnhandledException -= OnException;
            app.Exit -= OnExit;
            BugFreak.AgileReporter.Dispose();
        }
    }
}
