using Chinwag.Application.Queries.ArchetypeCount;
using Chinwag.Application.Queries.BanlistCount;
using Chinwag.Application.Queries.CardCount;
using Chinwag.Application.Queries.DeckCount;
using MediatR;
using Prism.Ioc;

namespace Chinwag.Application
{
    public static class ApplicationInstaller
    {
        public static IContainerRegistry AddApplicationServices(this IContainerRegistry containerRegistry)
        {
            
            return containerRegistry
                .AddMediatr();
        }

        private static IContainerRegistry AddMediatr(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMediator, Mediator>();

            containerRegistry.Register<IRequestHandler<DeckCountQuery, int>, DeckCountQueryHandler>();
            containerRegistry.Register<IRequestHandler<BanlistCountQuery, int>, BanlistCountQueryHandler>();
            containerRegistry.Register<IRequestHandler<CardCountQuery, int>, CardCountQueryHandler>();
            containerRegistry.Register<IRequestHandler<ArchetypeCountQuery, int>, ArchetypeCountQueryHandler>();

            return containerRegistry;
        }
    }
}