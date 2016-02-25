using Findier.Core.Helpers;
using Findier.Web.Enums;

namespace Findier.Web.Models
{
    public class Post : DtoBaseWithCreatedAt
    {
        public PlainCategory Category { get; set; }

        public int Downs { get; set; }

        public string Email { get; set; }

        public string HeaderText => $"@{User} - {TimeHelper.ToTimeSince(CreatedAt)}";

        public bool IsArchived { get; set; }

        public bool IsNsfw { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Price { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public PostType Type { get; set; }

        public int Ups { get; set; }

        public string User { get; set; }
    }
}