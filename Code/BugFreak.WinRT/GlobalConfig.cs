namespace BugFreak.WinRT
{
    public static class GlobalConfig
    {
        public static string ApiKey
        {
            get { return global::BugFreak.GlobalConfig.Settings.ApiKey; }
            set { global::BugFreak.GlobalConfig.Settings.ApiKey = value; }
        }

        public static string Token
        {
            get { return global::BugFreak.GlobalConfig.Settings.Token; }
            set { global::BugFreak.GlobalConfig.Settings.Token = value; }
        }

        public static string ServiceEndpoint
        {
            get { return global::BugFreak.GlobalConfig.Settings.ServiceEndPoint; }
            set { global::BugFreak.GlobalConfig.Settings.ServiceEndPoint = ServiceEndpoint; }
        }
    }
}