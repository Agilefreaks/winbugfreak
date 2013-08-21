namespace BugFreak
{
    using global::BugFreak.WinRT.Components;
    using Windows.UI.Xaml;
    
    public static class BugFreak
    {
        public static void Hook(string apiKey, string token, Application app)
        {
            GlobalConfig.ApiKey = apiKey;
            GlobalConfig.Token = token;

            GlobalConfig.ErrorDataProviders.Add(new PackageInfoProvier());
            ReportingService.Init();
            
            app.UnhandledException += OnException;
        }

        private static void OnException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var exception = eventArgs.Exception;
            exception.Data.Add("Message", eventArgs.Message);

            ReportingService.Instance.BeginReport(exception);
        }
    }
}
