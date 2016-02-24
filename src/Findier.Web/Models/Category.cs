namespace Findier.Web.Models
{
    public class Category : PlainCategory
    {
        public string Description { get; set; }

        public bool IsNsfw { get; set; }
    }
}