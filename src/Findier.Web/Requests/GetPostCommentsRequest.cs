using System;
using System.Collections.Generic;
using Findier.Web.Extensions;
using Findier.Web.Models;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public class GetPostCommentsRequest : FindierBaseRequest<FindierPageData<Comment>>
    {
        public GetPostCommentsRequest(string id) : base("posts/{id}/comments")
        {
            this.Segment("id", id);
        }

        public GetPostCommentsRequest Limit(int limit)
        {
            return this.Query("limit", limit);
        }

        public GetPostCommentsRequest Offset(int offset)
        {
            return this.Query("offset", offset);
        }

        public override FindierPageData<Comment> ToDummyData()
        {
            return new FindierPageData<Comment>
            {
                Results = new List<Comment>
                {
                    new Comment
                    {
                        User = "Doe",
                        Text = "John dies at the end.",
                        IsOp = true,
                        Ups = 1,
                        CreatedAt = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5))
                    }
                }
            };
        }
    }
}