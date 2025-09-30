using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblPaymentTransactionDetail
{
    public long PaymentTransactionId { get; set; }

    public string? TransactionStatus { get; set; }

    public string? TransactionError { get; set; }

    public string? TxnDate { get; set; }

    public string? Amount { get; set; }

    public string? TransactionId { get; set; }

    public string? TrackId { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Pmntmode { get; set; }

    public string? Type { get; set; }

    public string? Card { get; set; }

    public string? CardType { get; set; }

    public string? Member { get; set; }

    public string? PaymentId { get; set; }

    public int? StudentId { get; set; }

    public string? ApplicationNumber { get; set; }
}
