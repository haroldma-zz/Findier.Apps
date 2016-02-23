namespace Findier.Web.Models
{
    public class Finboard : PlainFinboard
    {
        public string Description { get; set; }

        public bool IsNsfw { get; set; }
    }
}