using Autofac;

namespace Findier.Client.Windows.Engine.Providers
{
    public interface IProvider<out T>
    {
        T CreateInstance(IComponentContext context);
    }
}