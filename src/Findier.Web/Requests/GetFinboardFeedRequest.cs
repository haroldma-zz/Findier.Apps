using System.Collections.Generic;
using Findier.Web.Enums;
using Findier.Web.Extensions;
using Findier.Web.Models;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public class GetFinboardFeedRequest : FindierBaseRequest<FindierPageData<PlainPost>>
    {
        public GetFinboardFeedRequest(string id) : base("finboards/{id}/posts")
        {
            this.Segment("id", id);
        }

        public GetFinboardFeedRequest Limit(int limit)
        {
            return this.Query("limit", limit);
        }

        public GetFinboardFeedRequest Offset(int offset)
        {
            return this.Query("offset", offset);
        }

        public override FindierPageData<PlainPost> ToDummyData()
        {
            return new FindierPageData<PlainPost>
            {
                Results = new List<PlainPost>
                {
                    new PlainPost
                    {
                        Title = "5 dollar app icons",
                        Text =
                            "Studying gfx design and trying to do some freelance work on the side.\n\nCan do flat and minimilistic look. Text or icon based.",
                        User = "Danny",
                        Ups = 5,
                        Downs = 2,
                        Type = PostType.Fixed,
                        Price = 5
                    },
                    new PlainPost
                    {
                        Title = "Full body massage xoxo",
                        Text =
                            "Hello and good evening gentlemen. My name is Nayalia and i am here to accompanion you through tonight...I love what i do and to meet new people and am open to new experinces!",
                        User = "Nayalia",
                        Ups = 12,
                        Downs = 5,
                        Type = PostType.Money
                    }
                }
            };
        }
    }
}