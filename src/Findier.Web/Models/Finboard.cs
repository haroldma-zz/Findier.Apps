namespace Findier.Web.Models
{
    public class Finboard : DtoBase
    {
        public string Description { get; set; }

        public bool IsNsfw { get; set; }

        public string Title { get; set; }
    }
}