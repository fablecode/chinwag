using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class BanlistCard
{
    public long BanlistId { get; set; }

    public long CardId { get; set; }

    public long LimitId { get; set; }

    public virtual Banlist Banlist { get; set; } = null!;

    public virtual Card Card { get; set; } = null!;

    public virtual Limit Limit { get; set; } = null!;
}
