using System;

namespace BugFreak.Components
{
    public interface IErrorHandler : IDisposable
    {
        void Handle(Exception exc, ReportCompletedCallback callback);
    }
}
