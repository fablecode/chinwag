using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using chinwag.Views.Archetypes;
using chinwag.Views.Banlist;
using chinwag.Views.Cards;
using chinwag.Views.Decks;
using chinwag.Views.Home;
using MahApps.Metro.IconPacks;
using chinwag.Services;
using Prism.Commands;

namespace chinwag.ViewModels;

public class ShellViewModel : BindableBase
{
    private readonly INavigationServiceEx _navigationServiceEx;
    public string AppTitle => "Chinwag";

    private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
    private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();
    private string _hamburgerTitle;
    private MenuItem _selectedMenuItem;

    public ObservableCollection<MenuItem> Menu => AppMenu;

    public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

    // Commands
    public DelegateCommand MenuItemInvokedCommand { get; private set; }

    public string HamburgerTitle
    {
        get => _hamburgerTitle;
        set => SetProperty(ref _hamburgerTitle, value);
    }

    public MenuItem SelectedMenuItem
    {
        get => _selectedMenuItem;
        set => SetProperty(ref _selectedMenuItem, value);
    }

    public ShellViewModel(INavigationServiceEx navigationServiceEx)
    {
        _navigationServiceEx = navigationServiceEx;

        MenuItemInvokedCommand = new DelegateCommand(MenuItemInvoked);

        // Build the menus
        Menu.Add(new MenuItem()
        {
            Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.HomeSolid },
            Label = "Home",
            NavigationType = typeof(Home),
            NavigationDestination = new Uri("Views/Home/Home.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.Stack},
            Label = "Decks",
            NavigationType = typeof(Decks),
            NavigationDestination = new Uri("Views/Decks.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.FileBlockAlt},
            Label = "Banlists",
            NavigationType = typeof(Banlists),
            NavigationDestination = new Uri("Views/Banlists.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Cards},
            Label = "Cards",
            NavigationType = typeof(Cards),
            NavigationDestination = new Uri("Views/Cards.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconCodicons() { Kind = PackIconCodiconsKind.Tag},
            Label = "Archetypes",
            NavigationType = typeof(Archetypes),
            NavigationDestination = new Uri("Views/Archetypes.xaml", UriKind.RelativeOrAbsolute)
        });

        SelectedMenuItem = Menu.First();
    }

    private void MenuItemInvoked()
    {
        _navigationServiceEx.Navigate(SelectedMenuItem.NavigationDestination);
    }
}