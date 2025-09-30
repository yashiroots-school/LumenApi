using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblStudentFeeSaved
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public float TotalFee { get; set; }

    public float FeePaid { get; set; }

    public float OldFee { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }
}
