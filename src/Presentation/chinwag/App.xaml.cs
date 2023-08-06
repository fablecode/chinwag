using System.Collections.Generic;
using System.IO;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using Chinwag.Modules.Cards;
using Chinwag.Modules.Decks;
using Chinwag.Modules.Home;
using Chinwag.Modules.Navigation;
using Chinwag.Presentation.Core.Commands;
using Chinwag.Views;
using Chinwag.Presentation.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Prism.Modularity;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Chinwag.Modules.Dashboard;

namespace Chinwag
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public IConfiguration _configuration { get; private set; }

        private IOptions<AppSettings>? _appSettings;

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            IServiceProvider provider = services.BuildServiceProvider();

            _appSettings = provider.GetRequiredService<IOptions<AppSettings>>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            containerRegistry.RegisterInstance(_appSettings);
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
            moduleCatalog.AddModule<CardsModule>();
            moduleCatalog.AddModule<DashboardModule>();
        }
    }



    public record AppSettings
    {
        public List<WikiaSetting> WikiaSettings { get; set; }
    }
    public record WikiaSetting
    {
        public string WikiaDomainUrl { get; init; }
        public string Category { get; init; }
        public int PageSize { get; init; }
    }
}
