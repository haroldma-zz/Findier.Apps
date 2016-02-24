using System.Collections.Generic;
using Findier.Web.Enums;
using Findier.Web.Extensions;
using Findier.Web.Models;
using Findier.Web.Responses;

namespace Findier.Web.Requests
{
    public class GetCategoriesRequest : FindierBaseRequest<FindierPageData<Category>>
    {
        public GetCategoriesRequest(Country country) : base("categories")
        {
            this.Query(nameof(country), country);
        }

        public GetCategoriesRequest Limit(int count)
        {
            return this.Query("limit", count);
        }

        public GetCategoriesRequest Offset(int offset)
        {
            return this.Query("offset", offset);
        }

        public override FindierPageData<Category> ToDummyData()
        {
            return new FindierPageData<Category>
            {
                Results = new List<Category>
                {
                    new Category
                    {
                        Title = "Lol",
                        Description = "Fun stuff!"
                    }
                }
            };
        }
    }
}