using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TransportFeeConfiguration
{
    public int TransportFeeConfigurationId { get; set; }

    public string? Class { get; set; }

    public string? BatchName { get; set; }

    public int? FromKm { get; set; }

    public int? ToKm { get; set; }

    public int? Amount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int ClassId { get; set; }

    public int BatchId { get; set; }
}
