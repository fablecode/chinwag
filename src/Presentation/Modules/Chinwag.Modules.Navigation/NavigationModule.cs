using Chinwag.Modules.Navigation.Views;
using Chinwag.Presentation.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Chinwag.Modules.Navigation;

public class NavigationModule : IModule
{
    private readonly IRegionManager _regionManager;

    public NavigationModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationControl));
    }
}