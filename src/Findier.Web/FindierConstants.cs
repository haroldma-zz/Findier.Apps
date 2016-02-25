namespace Findier.Web
{
    public static class FindierConstants
    {
#if DEBUG
        public const string BaseUrl = "http://findierapi-dev.azurewebsites.net/api/";

#else  
        public const string BaseUrl = "http://findierapi.azurewebsites.net/api";
#endif
        public const string GeoHeader = "X-Geo-Location";

        public const int RequestMaxCount = 100;
    }
}