using Newtonsoft.Json;

namespace Findier.Web.Deserializers
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}