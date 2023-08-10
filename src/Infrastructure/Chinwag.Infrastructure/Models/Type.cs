using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Type
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }
}
