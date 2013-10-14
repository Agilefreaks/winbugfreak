namespace BugFreak.Components
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;

    public class SessionErrorDataProvider : IErrorDataProvider
    {
        private HttpContext _httpContext;

        public HttpContext HttpContext
        {
            get { return _httpContext ?? HttpContext.Current; }
            set { _httpContext = value; }
        }

        public List<KeyValuePair<string, string>> GetData()
        {
            var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("SessionID", HttpContext.Session.SessionID),
                    new KeyValuePair<string, string>("SessionTimeout", HttpContext.Session.Timeout.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("SessionIsNew", HttpContext.Session.IsNewSession.ToString())
                };

            return result;
        }
    }
}