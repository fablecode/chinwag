using Chinwag.Modules.Decks.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Chinwag.Modules.Decks;

public class DecksModule : IModule
{
    private readonly IRegionManager _regionManager;

    public DecksModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<DecksControl>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}