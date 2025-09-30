using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCreateBranch
{
    public int BranchId { get; set; }

    public int BankId { get; set; }

    public string? BankName { get; set; }

    public string? BranchName { get; set; }

    public string? ContactNo { get; set; }

    public string? ContactName { get; set; }

    public string? LandlineNo { get; set; }
}
