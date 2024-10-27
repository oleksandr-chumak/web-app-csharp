using System;
using System.Collections.Generic;

namespace web_app_csharp.Models;

public partial class Worker
{
    public int WorkersId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public decimal? DeptId { get; set; }

    public string? Information { get; set; }

    public virtual Department? Dept { get; set; }
}
