using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Findier.Core.Extensions
{
    public static class UriExtensions
    {
        public static async Task<HttpResponseMessage> ExecuteAsync(
            this Uri uri,
            HttpMethod method,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            double timeout = 100.0)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);

                    return await client.SendAsync(new HttpRequestMessage(method, uri), completionOption);
                }
            }
            catch
            {
                return null;
            }
        }

        public static Task<HttpResponseMessage> GetAsync(this Uri uri)
        {
            return uri.ExecuteAsync(HttpMethod.Get);
        }

        public static async Task<Stream> GetStreamAsync(this Uri uri)
        {
            using (var response = await uri.GetAsync())
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }

            return null;
        }

        public static Task<HttpResponseMessage> HeadAsync(this Uri uri)
        {
            return uri.ExecuteAsync(HttpMethod.Head, HttpCompletionOption.ResponseHeadersRead, 10);
        }
    }
}