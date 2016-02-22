using Findier.Core.Helpers;

namespace Findier.Web.Models
{
    public class Comment : DtoBaseWithCreatedAt
    {
        public int Downs { get; set; }

        public bool IsOp { get; set; }

        public string Text { get; set; }

        public int Ups { get; set; }

        public string User { get; set; }

        public string HeaderText => $"@{User} - {TimeHelper.ToTimeSince(CreatedAt)}";
    }
}