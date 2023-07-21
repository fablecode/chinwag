using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using Chinwag.Modules.Decks;
using Chinwag.Modules.Home;
using Chinwag.Modules.Navigation;
using Chinwag.Presentation.Core.Commands;
using Chinwag.Views;
using Chinwag.Presentation.Core.Interfaces;
using Prism.Modularity;

namespace Chinwag
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NavigationModule>();
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<DecksModule>();
        }
    }
}
