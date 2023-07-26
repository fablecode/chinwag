using Chinwag.Modules.Cards.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Chinwag.Modules.Cards;

public class CardsModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<CardsControl>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}