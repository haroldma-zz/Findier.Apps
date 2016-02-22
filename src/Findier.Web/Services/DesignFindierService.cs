using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Findier.Web.Http;
using Findier.Web.Requests;
using Findier.Web.Responses;

namespace Findier.Web.Services
{
    public class DesignFindierService : IFindierService
    {
        public string CurrentUser => "JohnDoe";

        public bool IsAuthenticated => true;

        public void Login(string username, string token)
        {
        }

        public Task<RestResponse<OAuthData>> LoginAsync(OAuthRequest request)
        {
            return
                Task.FromResult(new RestResponse<OAuthData>(new HttpResponseMessage(HttpStatusCode.OK), new OAuthData()));
        }

        public void Logout()
        {
        }

        public Task<RestResponse<OAuthData>> RegisterAsync(CreateUserRequest request)
        {
            return
                Task.FromResult(new RestResponse<OAuthData>(new HttpResponseMessage(HttpStatusCode.OK), new OAuthData()));
        }

        public Task<RestResponse<FindierBaseResponse<TT>>> SendAsync<T, TT>(T request) where T : FindierBaseRequest<TT>
        {
            return
                Task.FromResult(new RestResponse<FindierBaseResponse<TT>>(new HttpResponseMessage(HttpStatusCode.OK),
                    new FindierBaseResponse<TT>
                    {
                        Data = request.ToDummyData()
                    }));
        }
    }
}