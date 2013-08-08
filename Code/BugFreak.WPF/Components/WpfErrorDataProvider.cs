namespace BugFreak.Components
{
    using System;
    using System.Collections.Generic;

    public class WpfErrorDataProvider : EnvironmentErrorDataProvider
    {
        public override List<KeyValuePair<string, string>> GetData()
        {
            var result = base.GetData();

            result.Add(new KeyValuePair<string, string>("ServicePack", Environment.OSVersion.ServicePack));

            return result;
        }
    }
}