using System;

namespace BugFreak.Components
{
    public interface IErrorReportQueueListener : IDisposable
    {
        void Listen(IErrorReportQueue reportQueue);
    }
}