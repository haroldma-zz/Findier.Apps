using System;

namespace Findier.Web.Models
{
    public class DtoBase
    {
        public string Id { get; set; }
    }

    public class DtoBaseWithCreatedAt : DtoBase
    {
        public DateTime CreatedAt { get; set; }
    }
}