using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TcFeeDetail
{
    public long Id { get; set; }

    public long? StudentTcDetailsId { get; set; }

    public int StudentId { get; set; }

    public decimal ReceiptAmount { get; set; }

    public string? PaymentMode { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsTcfee { get; set; }

    public DateTime? PaidDate { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual StudentTcDetail? StudentTcDetails { get; set; }
}
