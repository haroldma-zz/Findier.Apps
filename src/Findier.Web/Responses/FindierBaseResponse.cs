namespace Findier.Web.Responses
{
    public class FindierBaseResponse
    {
        public string Error { get; set; }
    }

    public class FindierBaseResponse<T> : FindierBaseResponse
    {
        public T Data { get; set; }
    }
}