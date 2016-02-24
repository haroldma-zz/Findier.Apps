using Findier.Web.Attributes;
using Findier.Web.Enums;
using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    [IncludeGeoLocation]
    public class CreatePostRequest : FindierBaseRequest<string>
    {
        public CreatePostRequest(
            string finboardId,
            string title,
            string text,
            PostType type,
            decimal price = 0,
            bool isNsfw = false,
            bool canMessage = true,
            string email = null,
            string phoneNumber = null) : base("posts")
        {
            this.Post()
                .Parameter(nameof(finboardId), finboardId)
                .Parameter(nameof(title), title)
                .Parameter(nameof(text), text)
                .Parameter(nameof(type), type.ToString())
                .Parameter(nameof(price), price.ToString())
                .Parameter(nameof(isNsfw), isNsfw.ToString())
                .Parameter(nameof(canMessage), canMessage.ToString())
                .Parameter(nameof(email), email)
                .Parameter(nameof(phoneNumber), phoneNumber);
        }

        public override string ToDummyData()
        {
            return "xY";
        }
    }
}