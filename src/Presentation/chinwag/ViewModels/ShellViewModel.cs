using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
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
    private bool _isBackButtonVisible;

    public ObservableCollection<MenuItem> Menu => AppMenu;

    public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

    public bool CanGoBack => _navigationServiceEx.CanGoBack;

    public bool IsBackButtonVisible
    {
        get => _isBackButtonVisible;
        set => SetProperty(ref _isBackButtonVisible, value);
    }


    // Commands
    public DelegateCommand MenuItemInvokedCommand { get; private set; }
    public DelegateCommand GoBackCommand { get; private set; }

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

        _navigationServiceEx.Navigated += NavigationServiceEx_OnNavigated;

        MenuItemInvokedCommand = new DelegateCommand(MenuItemInvoked);
        GoBackCommand = new DelegateCommand(GoBackInvoked);

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
            NavigationDestination = new Uri("Views/Decks/Decks.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.FileBlockAlt},
            Label = "Banlists",
            NavigationType = typeof(Banlists),
            NavigationDestination = new Uri("Views/Banlists/Banlists.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Cards},
            Label = "Cards",
            NavigationType = typeof(Cards),
            NavigationDestination = new Uri("Views/Cards/Cards.xaml", UriKind.RelativeOrAbsolute)
        });

        Menu.Add(new MenuItem()
        {
            Icon = new PackIconCodicons() { Kind = PackIconCodiconsKind.Tag},
            Label = "Archetypes",
            NavigationType = typeof(Archetypes),
            NavigationDestination = new Uri("Views/Archetypes/Archetypes.xaml", UriKind.RelativeOrAbsolute)
        });

        SelectedMenuItem = Menu.First();
    }

    private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
    {
        //SelectedMenuItem = Menu.First(x => x.NavigationDestination == e.Uri);

        IsBackButtonVisible = _navigationServiceEx.CanGoBack;
    }

    private void GoBackInvoked()
    {
        _navigationServiceEx.GoBack();
    }

    private void MenuItemInvoked()
    {
        _navigationServiceEx.Navigate(SelectedMenuItem.NavigationDestination);
    }
}