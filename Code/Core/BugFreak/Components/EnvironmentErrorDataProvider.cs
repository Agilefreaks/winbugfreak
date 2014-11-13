namespace BugFreak.Components
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class EnvironmentErrorDataProvider : IErrorDataProvider
    {
        public virtual List<KeyValuePair<string, string>> GetData()
        {
            return new List<KeyValuePair<string, string>>
                              {
                                  new KeyValuePair<string, string>("ExitCode", Environment.ExitCode.ToString(CultureInfo.InvariantCulture)),
                                  new KeyValuePair<string, string>("Platform", Environment.OSVersion.Platform.ToString()),                              
                                  new KeyValuePair<string, string>("OS Version", Environment.OSVersion.Version.ToString()),
                                  new KeyValuePair<string, string>("ProcessorCount", Environment.ProcessorCount.ToString(CultureInfo.InvariantCulture)),
                                  new KeyValuePair<string, string>("Version", Environment.Version.ToString())
                              }; ;
        }
    }
}