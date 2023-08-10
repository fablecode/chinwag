using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Deck
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? VideoUrl { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
}
