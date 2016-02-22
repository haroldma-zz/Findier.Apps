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
            double price = 0) : base("posts")
        {
            this.Post()
                .Parameter(nameof(finboardId), finboardId)
                .Parameter(nameof(title), title)
                .Parameter(nameof(text), text)
                .Parameter(nameof(type), type.ToString())
                .Parameter(nameof(price), price.ToString());
        }

        public override string ToDummyData()
        {
            return "xY";
        }
    }
}