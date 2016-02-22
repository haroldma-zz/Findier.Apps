using System.Collections.Generic;

namespace Findier.Web.Responses
{
    public class FindierPageData<T>
    {
        public bool HasNext { get; set; }

        public List<T> Results { get; set; }
    }
}