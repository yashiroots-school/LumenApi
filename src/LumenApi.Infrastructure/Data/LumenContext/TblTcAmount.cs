using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTcAmount
{
    public long Id { get; set; }

    public long Type { get; set; }

    public decimal Amount { get; set; }

    public bool IsDeleted { get; set; }
}
