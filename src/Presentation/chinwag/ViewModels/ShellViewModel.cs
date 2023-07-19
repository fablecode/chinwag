using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using chinwag.Views.Archetypes;
using chinwag.Views.Banlist;
using chinwag.Views.Cards;
using chinwag.Views.Decks;
using chinwag.Views.Home;
using MahApps.Metro.IconPacks;

namespace chinwag.ViewModels;

public class ShellViewModel : BindableBase
{
    public string AppTitle => "Chinwag";

    private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
    private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();

    public ObservableCollection<MenuItem> Menu => AppMenu;

    public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

    public ShellViewModel()
    {
        // Build the menus
        Menu.Add(new MenuItem()
        {
            Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.HomeSolid },
            Label = "Home",
            NavigationType = typeof(Home),
            NavigationDestination = new Uri("Views/Home.xaml", UriKind.RelativeOrAbsolute)
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
    }
}