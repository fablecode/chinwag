using Prism.Commands;
using Prism.Mvvm;
using System;
using Chinwag.Presentation.Core.Constants;
using Chinwag.Presentation.Core.Interfaces;
using Prism.Regions;

namespace Chinwag.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;

    public MainWindowViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands)
    {
        _regionManager = regionManager;
        _title = "Chinwag";

        NavigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand);

        applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
    }

    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    // Commands
    public DelegateCommand<string> NavigateCommand { get; }


    private void ExecuteNavigateCommand(string navigationPath)
    {
        if (string.IsNullOrEmpty(navigationPath))
            throw new ArgumentNullException(navigationPath);

        _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
    }

}