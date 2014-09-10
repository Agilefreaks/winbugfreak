namespace BugFreak.Components
{
    using System;
    using System.Collections.Generic;

    public class WpfErrorDataProvider : EnvironmentErrorDataProvider
    {
        public WpfErrorDataProvider()
        {
            EnvironmentData.Add(new KeyValuePair<string, string>("ServicePack", Environment.OSVersion.ServicePack));
        }
    }
}