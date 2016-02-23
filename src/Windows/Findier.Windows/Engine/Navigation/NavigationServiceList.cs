using System.Collections.Generic;
using System.Linq;

namespace Findier.Windows.Engine.Navigation
{
    public class NavigationServiceList : List<INavigationService>
    {
        public INavigationService GetByFrameId(string frameId) => this.FirstOrDefault(x => x.FrameFacade.FrameId == frameId);
    }
}
