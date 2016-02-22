using System.Reflection;
using Autofac;
using Findier.Client.Windows.Engine.Mvvm;
using Findier.Core.Extensions;
using Module = Autofac.Module;

namespace Findier.Client.Windows.Engine.Modules
{
    internal class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Every view model is a child of this base
            var viewModelBase = typeof (ViewModelBase);

            // they should also be located in that assembly (Audiotica.Windows)
            var assembly = viewModelBase.GetTypeInfo().Assembly;

            var types = assembly.DefinedTypes.GetImplementations(viewModelBase);
            foreach (var type in types)
            {
                builder.RegisterType(type);
            }
        }
    }
}