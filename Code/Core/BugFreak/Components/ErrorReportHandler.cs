using System.Collections.Generic;
using System.Linq;
using Bugfreak.Framework;
using Bugfreak.Results;

namespace Bugfreak.Components
{
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
