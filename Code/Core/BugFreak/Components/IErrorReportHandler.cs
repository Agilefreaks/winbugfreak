using System;

namespace BugFreak.Components
{
    public interface IErrorReportHandler : IDisposable
    {
        void Handle(ErrorReport report);
    }
}
