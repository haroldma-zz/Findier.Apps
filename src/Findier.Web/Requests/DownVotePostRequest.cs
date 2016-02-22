using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DownVotePostRequest : FindierBaseRequest
    {
        public DownVotePostRequest(string id, bool add) : base("posts/{id}/downs")
        {
            if (add)
            {
                this.Put();
            }
            else
            {
                this.Delete();
            }
            this.Parameter(nameof(id), id);
        }
    }
}