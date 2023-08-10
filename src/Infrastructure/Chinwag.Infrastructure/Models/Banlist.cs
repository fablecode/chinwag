using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Banlist
{
    public long Id { get; set; }

    public long FormatId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<BanlistCard> BanlistCards { get; set; } = new List<BanlistCard>();

    public virtual Format Format { get; set; } = null!;
}
