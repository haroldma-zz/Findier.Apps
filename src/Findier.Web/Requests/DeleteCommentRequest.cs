using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DeleteCommentRequest : FindierBaseRequest
    {
        public DeleteCommentRequest(string id) : base("comments/{id}")
        {
            this.Delete()
                .Segment("id", id);
        }
    }
}