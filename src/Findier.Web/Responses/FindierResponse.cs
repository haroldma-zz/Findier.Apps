namespace Findier.Web.Responses
{
    public class FindierResponse
    {
        public string Error { get; set; }
    }

    public class FindierResponse<T> : FindierResponse
    {
        public T Data { get; set; }
    }
}