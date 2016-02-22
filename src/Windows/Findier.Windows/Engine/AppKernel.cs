﻿using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using Findier.Client.Windows.Engine.Bootstrppers;
using Findier.Client.Windows.Services;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Client.Windows.Engine
{
    public class AppKernel
    {
        private readonly IEnumerable<IBootStrapper> _bootStrappers;

        public AppKernel(IEnumerable<Module> modules, IEnumerable<IBootStrapper> bootStrappers)
        {
            _bootStrappers = bootStrappers;
            var builder = new ContainerBuilder();
            Load(builder, modules);
            Container = builder.Build();
        }

        public IContainer Container { get; set; }

        public ILifetimeScope BeginScope()
        {
            return Container.BeginLifetimeScope();
        }

        public void Load(ContainerBuilder builder, IEnumerable<Module> modules)
        {
            builder.Register(context => BootStrapper.Current?.RootFrame ?? new Frame()).As<Frame>();

            if (!DesignMode.DesignModeEnabled)
                builder.Register(context => Window.Current.Dispatcher)
                    .As<CoreDispatcher>()
                    .SingleInstance()
                    .AutoActivate();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }
        }

        public void OnLaunched()
        {
            var insightsService = Resolve<IInsightsService>();
            using (insightsService.TrackTimeEvent("BootstrappersLaunched"))
                foreach (var bootstrapper in _bootStrappers)
                {
                    var name = bootstrapper.GetType().Name;
                    using (var scope = BeginScope())
                    using (
                        insightsService.TrackTimeEvent("BootstrapperLaunched",
                            new Dictionary<string, string> { { "Name", name } }))
                        try
                        {
                            bootstrapper.OnLaunched(scope);
                        }
                        catch
                        {
                            // ignored
                        }
                }
        }

        public void OnRelaunched()
        {
            var settings = Resolve<ISettingsUtility>();
            var insightsService = Resolve<IInsightsService>();
            using (insightsService.TrackTimeEvent("BootstrappersRelaunched"))
                foreach (var bootstrapper in _bootStrappers)
                {
                    var name = bootstrapper.GetType().Name;
                    var key = $"bootstrapper-state-{name}";
                    using (var scope = BeginScope())
                    using (
                        insightsService.TrackTimeEvent("BootstrapperRelaunched",
                            new Dictionary<string, string> { { "Name", name } })
                        )
                        try
                        {
                            bootstrapper.OnRelaunched(scope,
                                settings.Read(key, new Dictionary<string, object>()));
                        }
                        catch
                        {
                            // ignored
                        }

                    // Erase the module state
                    settings.Remove(key);
                }
        }

        public void OnResuming()
        {
            var insightsService = Resolve<IInsightsService>();
            using (insightsService.TrackTimeEvent("BootstrappersResumed"))
                foreach (var bootstrapper in _bootStrappers)
                {
                    var name = bootstrapper.GetType().Name;
                    using (var scope = BeginScope())
                    using (
                        insightsService.TrackTimeEvent("BootstrapperResumed",
                            new Dictionary<string, string> { { "Name", name } }))
                        try
                        {
                            bootstrapper.OnResuming(scope);
                        }
                        catch
                        {
                            // ignored
                        }
                }
        }

        public void OnSuspending()
        {
            var settings = Resolve<ISettingsUtility>();
            var insightsService = Resolve<IInsightsService>();
            using (insightsService.TrackTimeEvent("BootstrappersSuspended"))
                foreach (var bootstrapper in _bootStrappers)
                {
                    var name = bootstrapper.GetType().Name;
                    var key = $"bootstrapper-state-{name}";
                    var dict = new Dictionary<string, object>();

                    using (var scope = BeginScope())
                    using (
                        insightsService.TrackTimeEvent("BootstrapperSuspended",
                            new Dictionary<string, string> { { "Name", name } })
                        )
                        try
                        {
                            bootstrapper.OnSuspending(scope, dict);
                        }
                        catch
                        {
                            // ignored
                        }

                    // Save the module state
                    settings.Write(key, dict);
                }
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}