using Chinwag.Application.Queries.DeckCount;
using MahApps.Metro.IconPacks;
using MediatR;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Chinwag.Application.Queries.ArchetypeCount;
using Chinwag.Application.Queries.BanlistCount;
using Chinwag.Application.Queries.CardCount;

namespace Chinwag.Modules.Dashboard.ViewModels;

public class DashboardControlViewModel : BindableBase
{
    private readonly IMediator _mediator;
    private ObservableCollection<GlanceItem> _glanceItems = new();

    public ObservableCollection<GlanceItem> GlanceItems
    {
        get => _glanceItems;
        set => SetProperty(ref _glanceItems, value);
    }

    private void RetrieveGlanceItems()
    {
        _glanceItems.Add(new GlanceItem("Deck", "Decks")
        {
            Background = "Teal",
            Icon = new PackIconSimpleIcons() { Kind = PackIconSimpleIconsKind.BookStack },
            Count = new LazyProperty<int>( DeckCountSend, default)
        });

        _glanceItems.Add(new GlanceItem("Banlist", "Banlists")
        {
            Background = "Chocolate",
            Icon = new PackIconUnicons() { Kind = PackIconUniconsKind.FileBlockAlt },
            Count = new LazyProperty<int>( BanlistCountSend, default)
        });

        _glanceItems.Add(new GlanceItem("Card", "Cards")
        {
            Background = "Green",
            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.CardsPlayingOutline },
            Count = new LazyProperty<int>( CardCountSend, default)
        });

        _glanceItems.Add(new GlanceItem("Archetype", "Archetypes")
        {
            Background = "#FF6B56A7",
            Icon = new PackIconCodicons() { Kind = PackIconCodiconsKind.Tag },
            Count = new LazyProperty<int>( ArchetypeCountSend, default)
        });
    }

    private Task<int> BanlistCountSend(CancellationToken cancellationToken)
    {
        return CountSend<BanlistCountQuery>(cancellationToken);
    }

    private Task<int> DeckCountSend(CancellationToken cancellationToken)
    {
        return CountSend<DeckCountQuery>(cancellationToken);
    }
    private Task<int> CardCountSend(CancellationToken cancellationToken)
    {
        return CountSend<CardCountQuery>(cancellationToken);
    }
    private Task<int> ArchetypeCountSend(CancellationToken cancellationToken)
    {
        return CountSend<ArchetypeCountQuery>(cancellationToken);
    }

    private Task<int> CountSend<T>(CancellationToken cancellationToken) where T : IRequest<int>, new()
    {
        return _mediator.Send(new T(), cancellationToken);
    }


    public DashboardControlViewModel(IMediator mediator)
    {
        _mediator = mediator;

        RetrieveGlanceItems();
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
    public LazyProperty<int> Count { get; set; }
    public object Icon { get; set; }
    public string Background { get; set; }

    public string Label => $"{(Count == 1 ? Name : PluralizedName)}";
}

public class LazyProperty<T> : BindableBase
{
    private readonly CancellationTokenSource _cancelTokenSource = new();
    private readonly Func<CancellationToken, Task<T>> _retrievalFunc;
    private readonly T _defaultValue;
    private T _value;


    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private bool _isLoaded;
    public bool IsLoaded
    {
        get => _isLoaded;
        set => SetProperty(ref _isLoaded, value);
    }

    private bool _errorOnLoading;
    public bool ErrorOnLoading
    {
        get => _errorOnLoading;
        set => SetProperty(ref _errorOnLoading, value);
    }

    public T Value
    {
        get
        {
            if (IsLoaded)
                return _value;

            if (!_isLoading)
            {
                IsLoading = true;
                LoadValueAsync()
                    .ContinueWith((t) =>
                    {
                        if (!t.IsCanceled)
                        {
                            if (t.IsFaulted)
                            {
                                _value = _defaultValue;
                                ErrorOnLoading = true;
                                IsLoaded = true;
                                IsLoading = false;
                                SetProperty(ref _value, _defaultValue);
                            }
                            else
                            {
                                Value = t.Result;
                            }
                        }
                    });
            }

            return _defaultValue;
        }
        // if you want a ReadOnly-property just set this setter to private
        set
        {
            // since we set the value now, there is no need
            // to retrieve the "old" value asynchronously
            if (_isLoading)
            {
                CancelLoading();
            }

            IsLoaded = true;
            IsLoading = false;
            ErrorOnLoading = false;

            SetProperty(ref _value, value);
        }
    }

    private async Task<T> LoadValueAsync()
    {
        return await _retrievalFunc(_cancelTokenSource.Token);
    }

    public void CancelLoading()
    {
        _cancelTokenSource.Cancel();
    }

    public LazyProperty(Func<CancellationToken, Task<T>> retrievalFunc, T defaultValue)
    {
        _retrievalFunc = retrievalFunc ?? throw new ArgumentNullException(nameof(retrievalFunc));
        _defaultValue = defaultValue;

        _value = default(T) ?? throw new ArgumentNullException(nameof(defaultValue));
    }

    /// <summary>
    /// This allows you to assign the value of this lazy property directly
    /// to a variable of type T
    /// </summary>        
    public static implicit operator T(LazyProperty<T> p)
    {
        return p.Value;
    }
}