using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TransportFeePlan
{
    public int FeePlanId { get; set; }

    public string? FeePlanName { get; set; }

    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int FeeId { get; set; }

    public string? FeeName { get; set; }

    public float FeeValue { get; set; }

    public string? Opt1 { get; set; }

    public string? Opt2 { get; set; }

    public string? Opt3 { get; set; }

    public string? Opt4 { get; set; }

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
