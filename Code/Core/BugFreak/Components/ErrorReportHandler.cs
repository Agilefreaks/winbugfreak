using System.Collections.Generic;
using System.Linq;

namespace BugFreak.Components
{
    using global::BugFreak.Framework;
    using global::BugFreak.Results;

    public class ErrorReportHandler : IErrorReportHandler
    {
        private IList<IErrorReportStorage> _storageLocations;

        public ErrorReportHandler()
        {
            _storageLocations = GlobalConfig.ServiceLocator.GetServices<IErrorReportStorage>();
        }

        public void Handle(ErrorReport report)
        {
            new SequentialResult(_storageLocations.Select(storage => new ErrorReportSaveResult(storage, report)).Cast<IResult>()).Execute(new ExecutionContext());
        }

        public void Dispose()
        {
            _storageLocations = null;
        }
    }
}
