using System;
using System.Collections.Generic;
using Bugfreak.Framework;

namespace Bugfreak.Components
{
    public interface IErrorReportHandler : IDisposable
    {
        IEnumerable<IResult> Handle(ErrorReport report);
    }
}
