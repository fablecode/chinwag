using Prism.Commands;

namespace Chinwag.Presentation.Core.Interfaces;

public interface IApplicationCommands
{
    CompositeCommand NavigateCommand { get; }
}