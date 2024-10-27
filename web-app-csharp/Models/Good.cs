using System;
using System.Collections.Generic;

namespace web_app_csharp.Models;

public partial class Good
{
    public int GoodId { get; set; }

    public string Name { get; set; } = null!;

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Producer { get; set; }

    public decimal? DeptId { get; set; }

    public string? Description { get; set; }

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
