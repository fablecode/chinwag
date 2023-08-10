using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Limit
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<BanlistCard> BanlistCards { get; set; } = new List<BanlistCard>();
}
