using Newtonsoft.Json;

namespace Findier.Web.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
        public T Deserialize<T>(string text)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(text);
            }
            catch
            {
                return default(T);
            }
        }
    }
}