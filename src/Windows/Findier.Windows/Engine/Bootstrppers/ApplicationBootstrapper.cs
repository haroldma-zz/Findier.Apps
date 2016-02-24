using Autofac;
using Findier.Core.Utilities.Interfaces;
using Findier.Web.Services;
using Findier.Windows.Services;

namespace Findier.Windows.Engine.Bootstrppers
{
    internal class ApplicationBootstrapper : AppBootStrapper
    {
        public override void OnLaunched(IComponentContext context)
        {
            var applicationUtility = context.Resolve<IApplicationUtility>();
            var insightService = context.Resolve<IInsightsService>();
            var findierService = context.Resolve<IFindierService>();

            applicationUtility.OnStart();

            insightService.Client.Context.User.AccountId = findierService.CurrentUser;
            findierService.PropertyChanged +=
                (sender, args) =>
                    {
                        if (args.PropertyName == nameof(findierService.CurrentUser))
                        {
                            insightService.Client.Context.User.AccountId = findierService.CurrentUser;
                        }
                    };
        }
    }
}