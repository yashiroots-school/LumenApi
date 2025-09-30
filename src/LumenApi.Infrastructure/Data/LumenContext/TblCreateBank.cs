using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCreateBank
{
    public int BankId { get; set; }

    public string? BankName { get; set; }

    public string? BankCode { get; set; }

    public string? ContactNo { get; set; }

    public string? LandlineNo { get; set; }

    public string? ContactpersonName { get; set; }
}
