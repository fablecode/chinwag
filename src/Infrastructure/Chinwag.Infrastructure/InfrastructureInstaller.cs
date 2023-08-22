using Chinwag.Domain.Repository;
using Chinwag.Infrastructure.Database;
using Chinwag.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Prism.Ioc;

namespace Chinwag.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static IContainerRegistry AddInfrastructureServices(this IContainerRegistry containerRegistry, string connectionString)
        {
            return containerRegistry
                .AddDbContext(connectionString)
                .AddRepositories();
        }

        private static IContainerRegistry AddDbContext(this IContainerRegistry containerRegistry, string connectionString)
        {
            var builder = new DbContextOptionsBuilder<ChinwagDbContext>();
            builder.UseSqlServer(connectionString);

            containerRegistry.RegisterInstance(builder.Options);
            containerRegistry.Register<ChinwagDbContext>();

            return containerRegistry;
        }

        public static IContainerRegistry AddRepositories(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDeckRepository, DeckRepository>();
            containerRegistry.Register<IBanlistRepository, BanlistRepository>();
            containerRegistry.Register<ICardRepository, CardRepository>();
            containerRegistry.Register<IArchetypeRepository, ArchetypeRepository>();

            return containerRegistry;
        }
    }
}