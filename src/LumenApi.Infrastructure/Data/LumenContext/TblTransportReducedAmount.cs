using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTransportReducedAmount
{
    public int ReducedAmountId { get; set; }

    public string? Amount { get; set; }

    public string? Range { get; set; }
}
