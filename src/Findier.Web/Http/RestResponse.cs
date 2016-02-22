using System.Net;
using System.Net.Http;

namespace Findier.Web.Http
{
    public class RestResponse
    {
        internal RestResponse(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            IsSuccessStatusCode = response.IsSuccessStatusCode;
        }

        public bool IsSuccessStatusCode { get; }

        public string ReasonPhrase { get; }

        public HttpStatusCode StatusCode { get; }
    }

    public sealed class RestResponse<T> : RestResponse
    {
        internal RestResponse(HttpResponseMessage response, T deserializedResponse) : base(response)
        {
            DeserializedResponse = deserializedResponse;
        }

        public T DeserializedResponse { get; }
    }
}