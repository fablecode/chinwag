using Chinwag.Presentation.Core.Interfaces;
using Prism.Commands;

namespace Chinwag.Presentation.Core.Commands;

public class ApplicationCommands : IApplicationCommands
{
    public CompositeCommand NavigateCommand { get; } = new();
}