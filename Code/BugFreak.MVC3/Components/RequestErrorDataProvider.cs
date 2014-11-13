
namespace BugFreak.Components
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;

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
            var result = new List<KeyValuePair<string, string>>();

            if (HttpContext != null)
            {
                result.Add(new KeyValuePair<string, string>("Timestamp", HttpContext.Timestamp.ToString(CultureInfo.InvariantCulture)));
                result.Add(new KeyValuePair<string, string>("URL", HttpContext.Request.Url.AbsoluteUri));
                result.Add(new KeyValuePair<string, string>("Query", HttpContext.Request.QueryString.ToString()));
                result.Add(new KeyValuePair<string, string>("HttpMethod", HttpContext.Request.HttpMethod));
                result.Add(new KeyValuePair<string, string>("User Agent", HttpContext.Request.UserAgent));
                result.Add(new KeyValuePair<string, string>("Accept", string.Join(", ", HttpContext.Request.AcceptTypes ?? new string[0])));
                result.Add(new KeyValuePair<string, string>("Content-Length", HttpContext.Request.ContentLength.ToString(CultureInfo.InvariantCulture)));
                result.Add(new KeyValuePair<string, string>("Content-Type", HttpContext.Request.ContentType));
            }

            return result;
        }
    }
}
