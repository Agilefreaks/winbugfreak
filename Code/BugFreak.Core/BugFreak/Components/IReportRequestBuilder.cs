using System.Net;

namespace BugFreak.Components
{
    public interface IReportRequestBuilder
    {
        WebRequest Build(ErrorReport report);
    }
}