using System;
using System.Collections.Generic;

namespace Chinwag.Infrastructure.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
