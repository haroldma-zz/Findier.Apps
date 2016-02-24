using Findier.Web.Enums;
using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class EditPostRequest : FindierBaseRequest
    {
        public EditPostRequest(
            string id,
            string text = null,
            PostType? type = null,
            decimal? price = null,
            bool? isNsfw = null,
            string email = null,
            string phoneNumber = null) : base("posts/{id}")
        {
            this.Put().Segment(nameof(id), id);

            if (text != null)
            {
                this.Parameter(nameof(text), text);
            }

            if (type != null)
            {
                this.Parameter(nameof(type), type.ToString());
            }

            if (price != null)
            {
                this.Parameter(nameof(price), price.ToString());
            }

            if (isNsfw != null)
            {
                this.Parameter(nameof(isNsfw), isNsfw.ToString());
            }

            if (email != null)
            {
                this.Parameter(nameof(email), email);
            }
            if (phoneNumber != null)
            {
                this.Parameter(nameof(phoneNumber), phoneNumber);
            }
        }
    }
}