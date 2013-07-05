using System;

namespace BugFreak.Components
{
    public interface IErrorReportHandler : IDisposable
    {
        event EventHandler HandleComplete;

        void Handle(ErrorReport errorReport);
    }
}
