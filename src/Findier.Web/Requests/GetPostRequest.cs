using System;
using Findier.Web.Enums;
using Findier.Web.Extensions;
using Findier.Web.Models;

namespace Findier.Web.Requests
{
    public class GetPostRequest : FindierBaseRequest<Post>
    {
        public GetPostRequest(string id) : base("posts/{id}")
        {
            this.Segment("id", id);
        }

        public override Post ToDummyData()
        {
            return new Post
            {
                Title = "5 dollar app icons",
                Text =
                    "Studying gfx design and trying to do some freelance work on the side.\n\nCan do flat and minimilistic look. Text or icon based.",
                User = "Danny",
                Ups = 5,
                Downs = 2,
                Type = PostType.Fixed,
                Price = 5,
                CreatedAt = DateTime.UtcNow.Subtract(TimeSpan.FromDays(2))
            };
        }
    }
}