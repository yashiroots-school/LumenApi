using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblBatch
{
    public int BatchId { get; set; }

    public string? BatchName { get; set; }

    public bool IsActiveForAdmission { get; set; }

    public bool IsActiveForPayments { get; set; }

    public bool IsActiveForRegistrationFee { get; set; }
}
