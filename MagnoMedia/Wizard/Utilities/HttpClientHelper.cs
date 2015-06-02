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
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync(string.Format("{0}/{1}", StaticData.ApiHost, url)).Result;
                }
            }
            catch (Exception ex)
            {
                // Log exception

            }

            return response;
        }


        public static HttpResponseMessage Post<T>(string url, T data)
        {
            HttpResponseMessage response = null;

            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.PostAsJsonAsync<T>(string.Format("{0}/{1}", StaticData.ApiHost, url), data).Result;
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
