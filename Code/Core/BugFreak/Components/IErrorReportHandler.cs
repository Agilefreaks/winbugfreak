using System;
using System.Collections.Generic;
using BugFreak.Framework;

namespace BugFreak.Components
{
    using global::BugFreak.Framework;

    public interface IErrorReportHandler : IDisposable
    {
        IEnumerable<IResult> Handle(ErrorReport report);
    }
}
