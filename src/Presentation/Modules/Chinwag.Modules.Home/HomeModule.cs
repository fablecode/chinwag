using Chinwag.Modules.Home.ViewModels;
using Chinwag.Modules.Home.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Chinwag.Modules.Home;

public class HomeModule : IModule
{
    private readonly IRegionManager _regionManager;

    public HomeModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomeControl>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }
}