using System;
using BugFreak.Utils;

namespace BugFreak.Components
{
    public class RemoteErrorReportStorage : IRemoteErrorReportStorage
    {
        private readonly IReportRequestBuilder _reportRequestBuilder;

        public RemoteErrorReportStorage()
        {
            _reportRequestBuilder = GlobalConfig.ServiceProvider.GetService<IReportRequestBuilder>();
        }

        public bool TryStore(ErrorReport report)
        {
            bool success;

            try
            {
                var request = _reportRequestBuilder.Build(report);
                request.GetResponse();

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
    }
}