namespace BugFreak.Components
{
    public interface IErrorReportStorage
    {
        bool TryStore(ErrorReport report);
    }
}