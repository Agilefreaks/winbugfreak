namespace BugFreak.Components
{
    using System.Collections.Generic;

    public interface IErrorDataProvider
    {
        List<KeyValuePair<string, string>> GetData();
    }
}