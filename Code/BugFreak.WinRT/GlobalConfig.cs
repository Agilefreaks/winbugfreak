namespace BugFreak.WinRT
{
    public static class GlobalConfig
    {
        public static string ApiKey
        {
            get { return global::BugFreak.GlobalConfig.ApiKey; }
            set { global::BugFreak.GlobalConfig.ApiKey = value; }
        }

        public static string Token
        {
            get { return global::BugFreak.GlobalConfig.Token; }
            set { global::BugFreak.GlobalConfig.Token = value; }
        }

        public static string ServiceEndpoint
        {
            get { return global::BugFreak.GlobalConfig.ServiceEndPoint; }
            set { global::BugFreak.GlobalConfig.ServiceEndPoint = ServiceEndpoint; }
        }
    }
}