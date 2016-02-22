using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class UpvoteCommentRequest : FindierBaseRequest
    {
        public UpvoteCommentRequest(string id, bool add) : base("comments/{id}/ups")
        {
            if (add)
            {
                this.Put();
            }
            else
            {
                this.Delete();
            }
            this.Segment(nameof(id), id);
        }
    }
}