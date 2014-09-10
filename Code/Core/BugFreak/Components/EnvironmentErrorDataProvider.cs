namespace BugFreak.Components
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public abstract class EnvironmentErrorDataProvider : IErrorDataProvider
    {
        protected EnvironmentErrorDataProvider()
        {
            AdditionalData = new List<KeyValuePair<string, string>>();
            EnvironmentData = new List<KeyValuePair<string, string>>
                              {
                                  new KeyValuePair<string, string>("ExitCode", Environment.ExitCode.ToString(CultureInfo.InvariantCulture)),
                                  new KeyValuePair<string, string>("Platform", Environment.OSVersion.Platform.ToString()),                              
                                  new KeyValuePair<string, string>("OS Version", Environment.OSVersion.Version.ToString()),
                                  new KeyValuePair<string, string>("ProcessorCount", Environment.ProcessorCount.ToString(CultureInfo.InvariantCulture)),
                                  new KeyValuePair<string, string>("Version", Environment.Version.ToString())
                              };
        }

        public List<KeyValuePair<string, string>> EnvironmentData { get; private set; }

        public List<KeyValuePair<string, string>> AdditionalData { get; private set; } 

        public virtual IEnumerable<KeyValuePair<string, string>> GetData()
        {
            return AdditionalData.Concat(EnvironmentData);
        }
    }
}