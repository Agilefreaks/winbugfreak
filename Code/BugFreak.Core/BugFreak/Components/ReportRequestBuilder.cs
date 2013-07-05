using System;
using System.IO;
using System.Net;
using BugFreak.Utils;

namespace BugFreak.Components
{
    public class ReportRequestBuilder : IReportRequestBuilder
    {
        public const string InstanceIdentifierKey = "apiKey";
        public const string HttpMethod = "POST";

        private readonly IWebRequestCreate _webRequestFactory;

        public ReportRequestBuilder()
        {
            _webRequestFactory = GlobalConfig.ServiceProvider.GetService<IWebRequestCreate>();
        }

        public WebRequest Build(ErrorReport report)
        {
            var request = CreateRequest();
            SetMethod(request);
            Sign(request);
            SetAgent(request);
            Write(report, request);

            return request;
        }

        private WebRequest CreateRequest()
        {
            return _webRequestFactory.Create(new Uri(GlobalConfig.Settings.ServiceEndPoint));
        }

        private void SetMethod(WebRequest request)
        {
            request.Method = HttpMethod;
        }

        private void Sign(WebRequest request)
        {
            request.Headers.Add(InstanceIdentifierKey, GlobalConfig.Settings.InstanceIdentifier);
        }

        private void SetAgent(WebRequest request)
        {
            var httpRequest = request as HttpWebRequest;

            if (httpRequest != null)
            {
                httpRequest.UserAgent = GlobalConfig.Settings.AppName;
            }
        }

        private void Write(ErrorReport report, WebRequest request)
        {
            var serializer = GlobalConfig.ServiceProvider.GetService<IErrorReportSerializer>();

            var serializedObj = serializer.Serialize(report);

            var outputStream = request.GetRequestStream();
            var writer = new StreamWriter(outputStream);

            writer.Write(serializedObj);
            writer.Flush();

            if (outputStream.CanSeek)
            {
                outputStream.Seek(0, SeekOrigin.Begin);
            }

            request.ContentType = serializer.GetContentType();
        }
    }
}