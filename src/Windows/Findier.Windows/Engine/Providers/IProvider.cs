using Autofac;

namespace Findier.Windows.Engine.Providers
{
    public interface IProvider<out T>
    {
        T CreateInstance(IComponentContext context);
    }
}