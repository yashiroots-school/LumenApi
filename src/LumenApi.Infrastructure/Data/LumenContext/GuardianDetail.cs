using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class GuardianDetail
{
    public int Id { get; set; }

    public string? GuardianName { get; set; }

    public string? Qualifications { get; set; }

    public string? Occupation { get; set; }

    public string? Organization { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? AnnualIncome { get; set; }

    public string? ResidentialAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public int StudentRefId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public int? StudentStudentId { get; set; }

    public virtual Student? StudentStudent { get; set; }
}
