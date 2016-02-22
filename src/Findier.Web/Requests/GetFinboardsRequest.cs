using System.Collections.Generic;
using Findier.Web.Enums;
using Findier.Web.Extensions;
using Findier.Web.Models;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public class GetFinboardsRequest : FindierBaseRequest<FindierPageData<Finboard>>
    {
        public GetFinboardsRequest(Country country) : base("finboards")
        {
            this.Query(nameof(country), country);
        }

        public GetFinboardsRequest Limit(int count)
        {
            return this.Query("limit", count);
        }

        public GetFinboardsRequest Offset(int offset)
        {
            return this.Query("offset", offset);
        }

        public override FindierPageData<Finboard> ToDummyData()
        {
            return new FindierPageData<Finboard>
            {
                Results = new List<Finboard>
                {
                    new Finboard
                    {
                        Title = "Lol",
                        Description = "Fun stuff!"
                    }
                }
            };
        }
    }
}