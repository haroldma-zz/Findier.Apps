using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DownVoteCommentRequest : FindierBaseRequest
    {
        public DownVoteCommentRequest(string id, string commentId, bool add) : base("posts/{id}/comments/{commentId}/downs")
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