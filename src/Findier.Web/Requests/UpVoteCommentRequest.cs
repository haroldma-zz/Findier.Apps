using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class UpVoteCommentRequest : FindierBaseRequest
    {
        public UpVoteCommentRequest(string id, string commentId, bool add) : base("posts/{id}/comments/{commentId}/ups")
        {
            if (add)
            {
                this.Put();
            }
            else
            {
                this.Delete();
            }
            this.Parameter(nameof(id), id).Parameter(nameof(commentId), commentId);
        }
    }
}