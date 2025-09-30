using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblStudentPromote
{
    public int PromoteId { get; set; }

    public string? ScholarNumber { get; set; }

    public string? FromClass { get; set; }

    public string? ToClass { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public int FromClassId { get; set; }

    public int ToClassId { get; set; }

    public int StudentId { get; set; }

    public string? RegistrationDate { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public int BatchId { get; set; }

    public int? SectionId { get; set; }

    public string? ToSection { get; set; }
}
