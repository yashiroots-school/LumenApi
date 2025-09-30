using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTransportKm
{
    public int KmId { get; set; }

    public float FromKm { get; set; }

    public float ToKm { get; set; }

    public int BatchId { get; set; }

    public int SchoolId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public float Amount { get; set; }
}
