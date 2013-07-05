using System;
using System.ComponentModel.Design;
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
            if (String.IsNullOrEmpty(GlobalConfig.Settings.InstanceIdentifier))
            {
                throw new ArgumentException("Instance identifier not set");
            }
            if (String.IsNullOrEmpty(GlobalConfig.Settings.AppName))
            {
                throw new ArgumentException("AppName not set");
            }
        }

        private static void InitServices()
        {
            var serviceContainer = new ServiceContainer();

            GlobalConfig.ServiceProvider = serviceContainer;
            GlobalConfig.ErrorReportSerializer = new FormErrorReportSerializer();
            
            RegisterComponents(serviceContainer);
        }

        private static void RegisterComponents(ServiceContainer serviceContainer)
        {
            var errorReportQueue = new ErrorReportQueue();
            serviceContainer.AddService(typeof(IWebRequestCreate), new WebRequestFactory());
            serviceContainer.AddService(typeof(IErrorReportSerializer), (container, type) => GlobalConfig.ErrorReportSerializer);
            serviceContainer.AddService(typeof(IReportRequestBuilder), (container, type) => new ReportRequestBuilder());
            serviceContainer.AddService(typeof(IRemoteErrorReportStorage), (container, type) => new RemoteErrorReportStorage());
            serviceContainer.AddService(typeof(ILocalErrorReportStorage), (container, type) => new LocalErrorReportStorage());
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
