namespace BugFreak.Infrastructure
{
    using System.Web.Mvc;

    public class ReportErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ReportingService.Instance.BeginReport(filterContext.Exception);
        }
    }
}