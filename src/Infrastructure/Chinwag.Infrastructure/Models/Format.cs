using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Format
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Acronym { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<Banlist> Banlists { get; set; } = new List<Banlist>();
}
