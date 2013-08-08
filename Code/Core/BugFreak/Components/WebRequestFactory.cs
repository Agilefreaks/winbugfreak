using System;
using System.Net;

namespace BugFreak.Components
{
    public class WebRequestFactory : IWebRequestCreate
    {
        public WebRequest Create(Uri uri)
        {
            return WebRequest.Create(uri);
        }
    }
}