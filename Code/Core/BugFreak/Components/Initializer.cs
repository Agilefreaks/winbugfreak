namespace BugFreak.Components
{
    using System;
    using System.Net;

    internal class Initializer
    {
        public static void Initialize()
        {
            SetDefaults();
            VerifySettings();
            InitServices();
            InitReporter();
        }

        private static void SetDefaults()
        {
            if (!Uri.IsWellFormedUriString(GlobalConfig.ServiceEndPoint, UriKind.Absolute))
            {
                GlobalConfig.ServiceEndPoint = "https://www.bugfreak.co/v1/api/errors";
            }
        }

        private static void VerifySettings()
        {
            if (String.IsNullOrEmpty(GlobalConfig.Token))
            {
                throw new ArgumentException("Token not set");
            }
            if (!Uri.IsWellFormedUriString(GlobalConfig.ServiceEndPoint, UriKind.Absolute))
            {
                throw new ArgumentException("ServiceEndpoint not valid");
            }
            if (string.IsNullOrEmpty(GlobalConfig.ApiKey))
            {
                throw new ArgumentException("ApiKey not set");
            }
        }

        private static void InitServices()
        {
            var serviceContainer = new SimpleServiceContainer();

            GlobalConfig.ServiceLocator = serviceContainer;
            GlobalConfig.ErrorReportSerializer = new FormErrorReportSerializer();
           
            var errorReportQueue = new ErrorQueue();
            serviceContainer.AddService(typeof(IWebRequestCreate), new WebRequestFactory());
            serviceContainer.AddService(typeof(IErrorReportSerializer), (container, type) => GlobalConfig.ErrorReportSerializer);
            serviceContainer.AddService(typeof(IReportRequestBuilder), (container, type) => new ReportRequestBuilder());
            serviceContainer.AddService(typeof(IErrorReportStorage), (container, type) => new RemoteErrorReportStorage());
            serviceContainer.AddService(typeof(IErrorReportStorage), (container, type) => new LocalErrorReportStorage());
            serviceContainer.AddService(typeof(IErrorQueue), errorReportQueue);
            
            var errorHandler = new ErrorHandler();
            serviceContainer.AddService(typeof(IErrorHandler), errorHandler);
        }

        private static void InitReporter()
        {
            ReportingService.Instance = new ReportingService();
        }
    }
}
