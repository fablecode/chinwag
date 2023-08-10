using Chinwag.Application;
using Chinwag.DatabaseMigration;
using Chinwag.Infrastructure;
using Chinwag.Modules.Cards;
using Chinwag.Modules.Dashboard;
using Chinwag.Modules.Decks;
using Chinwag.Modules.Home;
using Chinwag.Modules.Navigation;
using Chinwag.Presentation.Core.Commands;
using Chinwag.Presentation.Core.Interfaces;
using Chinwag.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Configuration;
using Serilog.Formatting.Elasticsearch;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Chinwag
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public IConfiguration Configuration { get; private set; }

        private IOptions<AppSettings> _appSettings;

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            IServiceProvider provider = services.BuildServiceProvider();

            _appSettings = provider.GetRequiredService<IOptions<AppSettings>>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
        }



        //private void ConfigureServices()
        //{
        //    IServiceCollection services = new ServiceCollection();

        //    services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

        //    IServiceProvider provider = services.BuildServiceProvider();

        //    _appSettings = provider.GetRequiredService<IOptions<AppSettings>>();
        //}
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            containerRegistry.RegisterInstance(_appSettings);

            // Application Services
            var connectionString = Configuration
                .GetConnectionString("Chinwag")
                ?.Replace("[Database]", Environment.CurrentDirectory);

            const string connectString = @"Server=(LocalDB)\MSSQLLocalDB;" + "Integrated Security=true";
            var builder = new SqlConnectionStringBuilder(connectString)
            {
                AttachDBFilename = Path.Combine(Environment.CurrentDirectory, Configuration.GetConnectionString("DbRelativeFilePath")!),
                TrustServerCertificate = true,
                InitialCatalog = "Chinwag"
            };

            containerRegistry
                .AddDatabaseMigrationServices(builder.ConnectionString)
                .AddApplicationServices()
                .AddInfrastructureServices(builder.ConnectionString);

            containerRegistry.RegisterSingleton<ILoggerFactory>(ConfigureLogger);
            containerRegistry.Register(typeof(ILogger<>), typeof(LoggingAdapter<>));
        }

        private ILoggerFactory ConfigureLogger()
        {
            var loggerFactory = new LoggerFactory();

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithProperty("Machine", Environment.MachineName)
                .Enrich.WithProperty("CurrentDirectory", Environment.CurrentDirectory)
                .WriteTo.File(new ElasticsearchJsonFormatter(inlineFields: true, renderMessageTemplate: false),
                    _appSettings.Value.GetLogFilePath(), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7);

            loggerConfiguration.Enrich.WithProperty(nameof(_appSettings.Value.ApplicationName), _appSettings.Value.ApplicationName);

            var logger = loggerConfiguration.CreateLogger();
            Log.Logger = logger;

            loggerFactory.AddSerilog(logger);

            return loggerFactory;
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

    public class LoggingAdapter<T> : Logger<T>
    {
        public LoggingAdapter(ILoggerFactory factory) : base(factory)
        {
        }
    }

    public record AppSettings
    {
        /// <summary>
        /// Application name
        /// </summary>
        [Required]
        [Range(3, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Wikia settings
        /// </summary>
        public List<WikiaSetting> WikiaSettings { get; set; }

        /// <summary>
        /// Log file name
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetLogFileName() => $"{ApplicationName.ToLower()}-log-.json";

        /// <summary>
        /// Get the log directory
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetLogDirectoryPath() => Path.Combine(GetRootPath(), ApplicationName);

        /// <summary>
        /// Get the log file path
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetLogFilePath() => Path.Combine(GetLogDirectoryPath(), GetLogFileName());

        /// <summary>
        /// Get the root path
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PlatformNotSupportedException"></exception>
        private static string GetRootPath()
        {
            return Path.Combine
            (
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Fablecode"
            );
        }
    }
    public record WikiaSetting
    {
        public string WikiaDomainUrl { get; init; }
        public string Category { get; init; }
        public int PageSize { get; init; }
    }
}
