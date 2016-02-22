using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class UpvotePostRequest : FindierBaseRequest
    {
        public UpvotePostRequest(string id, bool add) : base("posts/{id}/ups")
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