using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class DeckType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
}
