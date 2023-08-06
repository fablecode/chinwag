using Chinwag.Modules.Dashboard.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Chinwag.Modules.Dashboard;

public class DashboardModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<DashboardControl>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}