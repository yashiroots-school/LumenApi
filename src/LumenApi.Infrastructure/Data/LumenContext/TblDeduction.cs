using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblDeduction
{
    public int DeductionsId { get; set; }

    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public int NetPay { get; set; }

    public int DeductionAmt { get; set; }

    public string? AddedDate { get; set; }

    public string? AddedMonth { get; set; }

    public string? AddedYear { get; set; }

    public string? AddedDay { get; set; }

    public string? Remarks { get; set; }

    public DateTime? AddedDate1 { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }
}
