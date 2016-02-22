using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class UpVotePostRequest : FindierBaseRequest
    {
        public UpVotePostRequest(string id, bool add) : base("posts/{id}/ups")
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