using System;
using System.Collections.Generic;

namespace web_app_csharp.Models;

public partial class Sale
{
    public int SalesId { get; set; }

    public int CheckNo { get; set; }

    public int GoodId { get; set; }

    public DateTime DateSale { get; set; }

    public int? Quantity { get; set; }

    public virtual Good Good { get; set; } = null!;
}
