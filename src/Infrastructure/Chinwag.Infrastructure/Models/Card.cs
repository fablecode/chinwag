using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Card
{
    public long Id { get; set; }

    public long? CardNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? CardLevel { get; set; }

    public int? CardRank { get; set; }

    public int? Atk { get; set; }

    public int? Def { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<BanlistCard> BanlistCards { get; set; } = new List<BanlistCard>();

    public virtual ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();

    public virtual ICollection<Archetype> Archetypes { get; set; } = new List<Archetype>();
}
