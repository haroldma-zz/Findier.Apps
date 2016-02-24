namespace Findier.Web.Models
{
    public class Post : PlainPost
    {
        public string Email { get; set; }

        public PlainFinboard Finboard { get; set; }

        public string PhoneNumber { get; set; }

        private bool CanMessage { get; set; }
    }
}