namespace BugFreak.Components
{
    using System.Collections.Generic;
    using System.Web;

    public class UserErrorDataProvider : IErrorDataProvider
    {
        private HttpContextBase _httpContext;

        public HttpContextBase HttpContext
        {
            get
            {
                return _httpContext ?? (System.Web.HttpContext.Current != null
                                            ? new HttpContextWrapper(System.Web.HttpContext.Current)
                                            : null);
            }
            set { _httpContext = value; }
        }

        public List<KeyValuePair<string, string>> GetData()
        {
            var result = new List<KeyValuePair<string, string>>();

            if (HttpContext != null && HttpContext.Request.IsAuthenticated)
            {
                result.Add(new KeyValuePair<string, string>("Username", HttpContext.User.Identity.Name));
                result.Add(new KeyValuePair<string, string>("AuthenticationType", HttpContext.User.Identity.AuthenticationType));
            }
            
            return result;
        }
    }
}