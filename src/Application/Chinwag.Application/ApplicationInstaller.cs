using Chinwag.Application.Queries.DeckCount;
using MediatR;
using Prism.Ioc;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Xml.Linq;

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

            return containerRegistry;
        }
    }
}