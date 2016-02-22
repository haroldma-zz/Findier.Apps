using Findier.Web.Extensions;
using Findier.Web.Http;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public abstract class FindierBaseRequest : RestRequestObject<FindierResponse>
    {
        protected FindierBaseRequest(string path)
        {
            this.Url(FindierConstants.BaseUrl + path);
        }
    }

    public abstract class FindierBaseRequest<T> : RestRequestObject<FindierResponse<T>>
    {
        protected FindierBaseRequest(string path)
        {
            this.Url(FindierConstants.BaseUrl + path);
        }

        public abstract T ToDummyData();
    }

    public abstract class FindierCustomBaseRequest<T> : RestRequestObject<T>
    {
        protected FindierCustomBaseRequest(string path)
        {
            this.Url(FindierConstants.BaseUrl + path);
        }
    }
}