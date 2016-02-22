using Newtonsoft.Json;

namespace Findier.Web.Responses
{
    public class OAuthData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public string Error { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string Username { get; set; }
    }
}