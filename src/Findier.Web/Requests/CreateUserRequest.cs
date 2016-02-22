using Findier.Web.Extensions;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public class CreateUserRequest : FindierCustomBaseRequest<OAuthData>
    {
        /// <summary>
        ///     Use this constructor for password registration.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="password">The password.</param>
        public CreateUserRequest(
            string username,
            string email,
            string displayName,
            string password) : base("users")
        {
            this.Post()
                .Parameter(nameof(username), username)
                .Parameter(nameof(email), email)
                .Parameter(nameof(displayName), displayName)
                .Parameter(nameof(password), password);
        }
    }
}