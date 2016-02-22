using Findier.Web.Extensions;
using Findier.Web.Models;

namespace Findier.Web.Requests
{
    public class CreateCommentRequest : FindierBaseRequest<Comment>
    {
        public CreateCommentRequest(
            string id,
            string text) : base("posts/{id}/comments")
        {
            this.Post()
                .Segment(nameof(id), id)
                .Parameter(nameof(text), text);
        }

        public override Comment ToDummyData()
        {
            return new Comment
            {
                User = "Doe",
                Text = "John dies at the end.",
                IsOp = true,
                Ups = 1
            };
        }
    }
}