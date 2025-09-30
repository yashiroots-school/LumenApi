using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentTcDetail
{
    public long Id { get; set; }

    public int StudentId { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Ispaid { get; set; }

    public long TcId { get; set; }

    public int BatchId { get; set; }

    public int ExamStatusId { get; set; }

    public int? PromoteClassId { get; set; }

    public string? Remark { get; set; }

    public int? RemarksId { get; set; }

    public string? Reason { get; set; }

    public int? ReasonId { get; set; }

    public DateTime? SchoolLeftDate { get; set; }

    public int ClassId { get; set; }

    public int? PromoteSectionId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<TcFeeDetail> TcFeeDetails { get; set; } = new List<TcFeeDetail>();
}
