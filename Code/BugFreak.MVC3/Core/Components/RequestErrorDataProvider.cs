using System.Collections.Generic;
using System.Globalization;
using System.Web;
using BugFreak.Components;

namespace BugFreak.Core.Components
{
    public class RequestErrorDataProvider : IErrorDataProvider
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
                    new KeyValuePair<string, string>("Timestamp", HttpContext.Timestamp.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("URL", HttpContext.Request.Url.AbsoluteUri),
                    new KeyValuePair<string, string>("Query", HttpContext.Request.QueryString.ToString()),
                    new KeyValuePair<string, string>("HttpMethod", HttpContext.Request.HttpMethod),
                    new KeyValuePair<string, string>("User Agent", HttpContext.Request.UserAgent),
                    new KeyValuePair<string, string>("Accept", string.Join(", ", HttpContext.Request.AcceptTypes ?? new string[0])),
                    new KeyValuePair<string, string>("Content-Length", HttpContext.Request.ContentLength.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("Content-Type", HttpContext.Request.ContentType)
                };
            
            return result;
        }
    }
}
