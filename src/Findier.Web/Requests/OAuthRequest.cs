using Findier.Web.Extensions;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    /// <summary>
    ///     OAuth login request.
    /// </summary>
    public class OAuthRequest : FindierCustomBaseRequest<OAuthData>
    {
        /// <summary>
        ///     Use this constructor for password logins.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public OAuthRequest(
            string username,
            string password) : base("oauth/token")
        {
            this.Post()
                .Parameter(nameof(username), username)
                .Parameter(nameof(password), password)
                .Parameter("grant_type", "password");
        }

        /// <summary>
        ///     Use this constructor for refresh token logins.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        public OAuthRequest(
            string refreshToken) : base("oauth/token")
        {
            this.Post()
                .Parameter("refresh_token", refreshToken)
                .Parameter("grant_type", "refresh_token");
        }
    }
}