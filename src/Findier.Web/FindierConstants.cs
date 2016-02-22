namespace Findier.Web
{
    public static class FindierConstants
    {
#if DEBUG
        public const string BaseUrl = "http://localhost:36321/api/";

#else  
        public const string BaseUrl = "http://app.getfindier.com/api";
#endif
        public const string GeoHeader = "X-Geo-Location";

        public const int RequestMaxCount = 100;
    }
}