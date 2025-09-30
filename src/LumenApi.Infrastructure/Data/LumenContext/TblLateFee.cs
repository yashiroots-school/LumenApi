using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblLateFee
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int FeeHeadingId { get; set; }

    public float LateFee { get; set; }

    public bool Paid { get; set; }

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
