using System;
using System.Collections.Generic;
using System.Text;
using MyDownloader.Core;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyDownloader.Protocols
{
    public class BaseProtocolProvider
    {
        static BaseProtocolProvider()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        }

        protected WebRequest GetRequest(ResourceLocation location)
        {
            WebRequest request = WebRequest.Create(location.URL);
            request.Timeout = 30000;
            SetProxy(request);
            return request;
        }

        protected void SetProxy(WebRequest request)
        {

        }
    }
}
