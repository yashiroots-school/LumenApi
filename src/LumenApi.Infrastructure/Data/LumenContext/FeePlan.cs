using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class FeePlan
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

    public int FeeTypeId { get; set; }

    public int TransportOptId { get; set; }

    public int? KmDistanceId { get; set; }

    public string? Amount { get; set; }

    public byte Jan { get; set; }

    public byte Feb { get; set; }

    public byte Mar { get; set; }

    public byte Apr { get; set; }

    public byte May { get; set; }

    public byte Jun { get; set; }

    public byte Jul { get; set; }

    public byte Aug { get; set; }

    public byte Sep { get; set; }

    public byte Oct { get; set; }

    public byte Nov { get; set; }

    public byte Dec { get; set; }

    public string? FeeConfigurationid { get; set; }

    public string? FeeConfigurationname { get; set; }

    public int BatchId { get; set; }

    public string? BatchName1 { get; set; }

    public string? Medium { get; set; }
}
