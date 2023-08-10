using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class DeckCard
{
    public long DeckTypeId { get; set; }

    public long DeckId { get; set; }

    public long CardId { get; set; }

    public int Quantity { get; set; }

    public int SortOrder { get; set; }

    public virtual Card Card { get; set; } = null!;

    public virtual Deck Deck { get; set; } = null!;

    public virtual DeckType DeckType { get; set; } = null!;
}
