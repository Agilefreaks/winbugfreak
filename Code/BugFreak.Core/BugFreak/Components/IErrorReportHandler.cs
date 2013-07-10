using System;
using System.Collections.Generic;
using BugFreak.Framework;

namespace BugFreak.Components
{
    public interface IErrorReportHandler : IDisposable
    {
        IEnumerable<IResult> Handle(ErrorReport report);
    }
}
