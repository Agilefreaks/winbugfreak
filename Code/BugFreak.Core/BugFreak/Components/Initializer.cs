using System;
using System.Net;

namespace BugFreak.Components
{
    internal class Initializer
    {
        public static void Initialize()
        {
            VerifySettings();
            InitServices();
            InitReporter();
        }

        private static void VerifySettings()
        {
            if (String.IsNullOrEmpty(GlobalConfig.Settings.AppName))
            {
                throw new ArgumentException("AppName not set");
            }
            if (String.IsNullOrEmpty(GlobalConfig.Settings.Token))
            {
                throw new ArgumentException("Token not set");
            }
            if (!Uri.IsWellFormedUriString(GlobalConfig.Settings.ServiceEndPoint, UriKind.Absolute))
            {
                throw new ArgumentException("ServiceEndpoint not valid");
            }
            if (string.IsNullOrEmpty(GlobalConfig.Settings.ApiKey))
            {
                throw new ArgumentException("ApiKey not set");
            }
        }

        private static void InitServices()
        {
            var serviceContainer = new SimpleServiceContainer();

            GlobalConfig.ServiceLocator = serviceContainer;
            GlobalConfig.ErrorReportSerializer = new FormErrorReportSerializer();
           
            var errorReportQueue = new ErrorReportQueue();
            serviceContainer.AddService(typeof(IWebRequestCreate), new WebRequestFactory());
            serviceContainer.AddService(typeof(IErrorReportSerializer), (container, type) => GlobalConfig.ErrorReportSerializer);
            serviceContainer.AddService(typeof(IReportRequestBuilder), (container, type) => new ReportRequestBuilder());
            serviceContainer.AddService(typeof(IErrorReportStorage), (container, type) => new RemoteErrorReportStorage());
            serviceContainer.AddService(typeof(IErrorReportStorage), (container, type) => new LocalErrorReportStorage());
            serviceContainer.AddService(typeof(IErrorReportQueue), errorReportQueue);
            
            var errorHandler = new ErrorReportHandler();
            serviceContainer.AddService(typeof(IErrorReportHandler), errorHandler);

            var errorQueueListener = new ErrorReportQueueListener();
            serviceContainer.AddService(typeof(IErrorReportQueueListener), errorQueueListener);

            errorQueueListener.Listen(errorReportQueue);
        }

        private static void InitReporter()
        {
            AgileReporter.Instance = new AgileReporter();
        }
    }
}
