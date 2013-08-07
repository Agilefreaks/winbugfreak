namespace Bugfreak.Components
{
    public interface IErrorReportSerializer
    {
        string GetContentType();

        string Serialize(ErrorReport report);
    }
}
