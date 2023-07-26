using Chinwag.Modules.Navigation.Models;
using Chinwag.Presentation.Core.Constants;
using Chinwag.Presentation.Core.Interfaces;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Chinwag.Modules.Navigation.ViewModels;

public class NavigationControlViewModel : BindableBase
{
    private readonly IApplicationCommands _applicationCommands;

    // Collections
    private static readonly ObservableCollection<NavigationItem> AppMenu = new ObservableCollection<NavigationItem>();
    private static readonly ObservableCollection<NavigationItem> AppOptionsMenu = new ObservableCollection<NavigationItem>();
    public ObservableCollection<NavigationItem> Menu => AppMenu;
    public ObservableCollection<NavigationItem> OptionsMenu => AppOptionsMenu;

    // Commands
    public DelegateCommand<HamburgerMenuItemInvokedEventArgs> MenuItemInvokedCommand { get; }

    private string? _hamburgerTitle;
    public string? HamburgerTitle
    {
        get => _hamburgerTitle;
        set => SetProperty(ref _hamburgerTitle, value);
    }

    private NavigationItem? _selectedMenuItem;

    public NavigationItem? SelectedMenuItem
    {
        get => _selectedMenuItem;
        set => SetProperty(ref _selectedMenuItem, value);
    }

    private bool _isMenuOpen;
    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set => SetProperty(ref _isMenuOpen, value);
    }

    public NavigationControlViewModel(IApplicationCommands applicationCommands)
    {
        _applicationCommands = applicationCommands;
        MenuItemInvokedCommand = new DelegateCommand<HamburgerMenuItemInvokedEventArgs>(MenuItemInvoked);

        // Build the menus
        Menu.Add(new NavigationItem()
        {
            Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.HomeSolid },
            Label = "Home",
            NavigationPath = NavigationPaths.HomePath
        });

        Menu.Add(new NavigationItem()
        {
            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Cards },
            Label = "Decks",
            NavigationPath = NavigationPaths.DecksPath
        });

        Menu.Add(new NavigationItem()
        {
            Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.FileBlockAlt },
            Label = "Banlists",
            NavigationPath = NavigationPaths.BanlistsPath,
        });

        Menu.Add(new NavigationItem()
        {
            Icon = new PackIconCodicons() { Kind = PackIconCodiconsKind.Tag },
            Label = "Archetypes",
            NavigationPath = NavigationPaths.ArchetypesPath
        });

        Menu.Add(new NavigationItem()
        {
            Icon = new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.Stack },
            Label = "Cards",
            NavigationPath = NavigationPaths.CardsPath
        });

        SelectedMenuItem = Menu.First();
    }

    private void MenuItemInvoked(HamburgerMenuItemInvokedEventArgs e)
    {
        if (e.InvokedItem is NavigationItem navigationItem)
        {
            _applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
        }
    }
}