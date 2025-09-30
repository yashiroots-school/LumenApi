using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCreateSchool
{
    public int SchoolId { get; set; }

    public string? SchoolName { get; set; }

    public string? Address { get; set; }

    public string? Website { get; set; }

    public string? Copyright { get; set; }

    public string? Date { get; set; }

    public string? Email { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? UploadImage { get; set; }

    public string? Status { get; set; }

    public string? Password { get; set; }

    public long? BoardId { get; set; }
}
