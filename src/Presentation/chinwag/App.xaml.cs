using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Windows;
using chinwag.Views;
using chinwag.Services;

namespace chinwag
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterSingleton<INavigationServiceEx, NavigationServiceEx>();


        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}
