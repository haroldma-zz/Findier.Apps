using Findier.Web.Extensions;
using Findier.Web.Http;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public abstract class FindierBaseRequest : RestRequestObject<FindierBaseResponse>
    {
        protected FindierBaseRequest(string path)
        {
            this.Url(FindierConstants.BaseUrl + path);
        }
    }

    public abstract class FindierBaseRequest<T> : RestRequestObject<FindierBaseResponse<T>>
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