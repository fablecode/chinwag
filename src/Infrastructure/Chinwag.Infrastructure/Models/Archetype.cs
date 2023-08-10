using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Archetype
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
