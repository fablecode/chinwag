using Prism.Ioc;

namespace Chinwag.Application
{
    public static class ApplicationInstaller
    {
        public static IContainerRegistry AddApplicationServices(this IContainerRegistry containerRegistry)
        {
            return containerRegistry;
        }
    }
}