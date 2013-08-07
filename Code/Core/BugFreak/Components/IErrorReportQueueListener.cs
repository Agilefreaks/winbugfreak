using System;

namespace Bugfreak.Components
{
    public interface IErrorReportQueueListener : IDisposable
    {
        void Listen(IErrorReportQueue reportQueue);
    }
}