namespace BugFreak.Components
{
    using System.Collections.Generic;

    public interface IErrorDataProvider
    {
        #region Public Properties

        List<KeyValuePair<string, string>> AdditionalData { get; }

        List<KeyValuePair<string, string>> EnvironmentData { get; }

        #endregion

        #region Public Methods and Operators

        IEnumerable<KeyValuePair<string, string>> GetData();

        #endregion
    }
}