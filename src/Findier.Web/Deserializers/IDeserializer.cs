namespace Findier.Web.Deserializers
{
    public interface IDeserializer
    {
        T Deserialize<T>(string text);
    }
}