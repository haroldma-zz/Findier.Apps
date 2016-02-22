using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DownvotePostRequest : FindierBaseRequest
    {
        public DownvotePostRequest(string id, bool add) : base("posts/{id}/downs")
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