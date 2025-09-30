using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblPaymentTransactionFeeDetail
{
    public int PaymentFeedetailsId { get; set; }

    public int? PaymentTransactionId { get; set; }

    public int? FeeId { get; set; }

    public string? FeeAmount { get; set; }

    public DateTime? CreatedOn { get; set; }
}
