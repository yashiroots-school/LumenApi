using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblFeeReceipt
{
    public int FeeReceiptId { get; set; }

    public int? StudentId { get; set; }

    public bool Jan { get; set; }

    public bool Feb { get; set; }

    public bool Mar { get; set; }

    public bool Apr { get; set; }

    public bool May { get; set; }

    public bool Jun { get; set; }

    public bool Jul { get; set; }

    public bool Aug { get; set; }

    public bool Sep { get; set; }

    public bool Oct { get; set; }

    public bool Nov { get; set; }

    public bool Dec { get; set; }

    public string? Type { get; set; }

    public string? PaidMonths { get; set; }

    public int ClassId { get; set; }

    public int CategoryId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public float Concession { get; set; }

    public float ConcessionAmt { get; set; }

    public string? StudentName { get; set; }

    public string? PayHeadings { get; set; }

    public float OldBalance { get; set; }

    public float ReceiptAmt { get; set; }

    public string? ClassName { get; set; }

    public string? CategoryName { get; set; }

    public float TotalFee { get; set; }

    public float LateFee { get; set; }

    public float BalanceAmt { get; set; }

    public string? PaymentMode { get; set; }

    public string? BankName { get; set; }

    public string? CheckId { get; set; }

    public string? Remark { get; set; }

    public string? FeePaids { get; set; }

    public string? FeeReceiptsOneTimeCreator { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? DueAmount { get; set; }

    public string? PaidAmount { get; set; }

    public string? ApplicationNumber { get; set; }

    public string? FeeHeadingIds { get; set; }

    public string? FeeconfigurationId { get; set; }

    public string? Feeconfigurationname { get; set; }
}
