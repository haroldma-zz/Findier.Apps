namespace Findier.Web.Models
{
    public class Post : PlainPost
    {
        public string Email { get; set; }

        public PlainCategory Category { get; set; }

        public string PhoneNumber { get; set; }
    }
}