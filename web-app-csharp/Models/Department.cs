using System;
using System.Collections.Generic;

namespace web_app_csharp.Models;

public partial class Department
{
    public decimal DeptId { get; set; }

    public string Name { get; set; } = null!;

    public string? Info { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
