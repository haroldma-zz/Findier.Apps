using System.Threading.Tasks;
using Findier.Web.Http;
using Findier.Web.Requests;
using Findier.Web.Responses;

namespace Findier.Web.Services
{
    public interface IFindierService
    {
        string CurrentUser { get; }

        bool IsAuthenticated { get; }

        void Login(string username, string token);

        Task<RestResponse<OAuthData>> LoginAsync(OAuthRequest request);

        void Logout();

        Task<RestResponse<OAuthData>> RegisterAsync(CreateUserRequest request);

        Task<RestResponse<FindierResponse<TT>>> SendAsync<T, TT>(T request) where T : FindierBaseRequest<TT>;
    }
}