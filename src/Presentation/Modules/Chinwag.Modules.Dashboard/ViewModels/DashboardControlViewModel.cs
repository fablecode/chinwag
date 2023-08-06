using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;
using System.Globalization;
using Prism.Mvvm;
using MahApps.Metro.Controls;

namespace Chinwag.Modules.Dashboard.ViewModels;

public class DashboardControlViewModel : BindableBase
{
    private ObservableCollection<GlanceItem> _glanceItems = new ObservableCollection<GlanceItem>();

    public ObservableCollection<GlanceItem> GlanceItems
    {
        get => _glanceItems;
        set => SetProperty(ref _glanceItems, value);
    }

    public DashboardControlViewModel()
    {
        _glanceItems.Add(new GlanceItem("Deck", "Decks")
        {
            Background = "Teal",
            Icon = new PackIconSimpleIcons() { Kind = PackIconSimpleIconsKind.BookStack },
            Count = 43
        });
        _glanceItems.Add(new GlanceItem("Banlist", "Banlists")
        {
            Background = "Chocolate",
            Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.FileBlockAlt },
            Count = 67
        });
        _glanceItems.Add(new GlanceItem("Card", "Cards")
        {
            Background = "Green",
            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.CardsPlayingOutline },
            Count = 11897
        });
        _glanceItems.Add(new GlanceItem("Archetype", "Archetypes")
        {
            Background = "#FF6B56A7",
            Icon = new PackIconCodicons() { Kind = PackIconCodiconsKind.Tag },
            Count = 117
        });
    }
}

public class GlanceItem
{
    public GlanceItem(string name, string pluralizedName)
    {
        Name = name;
        PluralizedName = pluralizedName;
    }

    public string Name { get; }
    public string PluralizedName { get; }
    public int Count { get; set; }
    public object Icon { get; set; }
    public string Background { get; set; }

    public string Label => $"{(Count == 1 ? Name : PluralizedName)}";
}