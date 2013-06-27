using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.SPOT;

namespace Endjin.XivleyClient
{
    public static class XivleyClient
    {
        const string baseUri = "http://api.xively.com/v2/feeds/";

        public static void Send(string apiKey, string feedId, string sample)
        {
            Debug.Print("time: " + DateTime.Now);
            Debug.Print("memory available: " + Debug.GC(true));

            try
            {
                using (var request = CreateRequest(apiKey, feedId, sample))
                {
                    request.Timeout = 5000;     // 5 seconds
                    // send request and receive response
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        HandleResponse(response);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }

        static HttpWebRequest CreateRequest(string apiKey, string feedId, string sample)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(sample);

            var request = (HttpWebRequest)WebRequest.Create(baseUri + feedId);

            // request line
            request.Method = "PUT";

            // request headers
            request.ContentLength = buffer.Length;
            request.ContentType = "application/json";
            request.Headers.Add("X-ApiKey", apiKey);

            // request body
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            return request;
        }

        public static void HandleResponse(HttpWebResponse response)
        {
            Debug.Print("Status code: " + response.StatusCode);
        }
    }
}