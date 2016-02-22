using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class CreateCommentRequest : FindierBaseRequest<string>
    {
        public CreateCommentRequest(
            string id,
            string text) : base("posts/{id}/comments")
        {
            this.Post()
                .Segment(nameof(id), id)
                .Parameter(nameof(text), text);
        }

        public override string ToDummyData()
        {
            return "xS";
        }
    }
}