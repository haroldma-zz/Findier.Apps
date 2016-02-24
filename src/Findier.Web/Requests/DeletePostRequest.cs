using Findier.Web.Extensions;

namespace Findier.Web.Requests
{
    public class DeletePostRequest : FindierBaseRequest
    {
        public DeletePostRequest(string id) : base("posts/{id}")
        {
            this.Delete()
                .Segment("id", id);
        }
    }
}