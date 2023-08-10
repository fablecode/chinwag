using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class SubCategory
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual Category Category { get; set; } = null!;
}
