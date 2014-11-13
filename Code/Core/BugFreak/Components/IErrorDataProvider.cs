namespace BugFreak.Components
{
    using System.Collections.Generic;

    public interface IErrorDataProvider
    {
        #region Public Methods and Operators

        List<KeyValuePair<string, string>> GetData();

        #endregion
    }
}