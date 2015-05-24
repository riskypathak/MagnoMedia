using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MagnoMedia.Windows.Utilities
{
   public class HttpClientHelper
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


        public static HttpResponseMessage Post<T>(string url, T data)
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
                    response = client.PostAsJsonAsync<T>(url, data).Result;
                }
            }
            catch (Exception ex)
            {
                // Log exception

            }

            return response;

        }

    }

     
}
