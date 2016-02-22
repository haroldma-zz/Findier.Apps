using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DownvoteCommentRequest : FindierBaseRequest
    {
        public DownvoteCommentRequest(string id, bool add) : base("comments/{id}/downs")
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