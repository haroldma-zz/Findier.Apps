using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Findier.Web.Deserializers
{
    public class DotNetXmlDeserializer : IDeserializer
    {
        public T Deserialize<T>(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return default(T);
            }

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                var serializer = new XmlSerializer(typeof (T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}