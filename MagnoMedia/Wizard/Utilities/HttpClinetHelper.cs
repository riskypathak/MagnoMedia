using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Windows.Utilities
{
    class HttpClinetHelper
    {
        public static HttpResponseMessage Get(string url)
        {
            HttpResponseMessage response = null;
            try
            {
                string hostname = ConfigurationManager.AppSettings["apiBaseAddress"].ToString();
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(hostname);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync(url).Result;
                   
                }
            }
            catch (Exception ex) {
            // Log exception

            }

            return response;
        }

    }

     
}
