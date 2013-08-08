using System.Collections.Generic;
using System.Linq;
using BugFreak.Framework;
using BugFreak.Results;

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

        public IEnumerable<IResult> Handle(ErrorReport report)
        {
            return _storageLocations.Select(storage => new ErrorReportSaveResult(storage, report)).Cast<IResult>();
        }

        public void Dispose()
        {
            _storageLocations = null;
        }
    }
}
