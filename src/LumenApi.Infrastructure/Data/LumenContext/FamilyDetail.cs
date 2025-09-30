using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class FamilyDetail
{
    public int Id { get; set; }

    public string? FatherName { get; set; }

    public string? Fqualifications { get; set; }

    public string? Foccupation { get; set; }

    public string? Forganization { get; set; }

    public string? Fphone { get; set; }

    public string? Fmobile { get; set; }

    public string? Femail { get; set; }

    public string? FannualIncome { get; set; }

    public string? FresidentialAddress { get; set; }

    public string? MotherName { get; set; }

    public string? Mqualifications { get; set; }

    public string? Moccupation { get; set; }

    public string? Morganization { get; set; }

    public string? Mphone { get; set; }

    public string? Mmobile { get; set; }

    public string? Memail { get; set; }

    public string? MannualIncome { get; set; }

    public string? MpermanentAddress { get; set; }

    public int StudentRefId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? NoOfBrothers { get; set; }

    public string? NoOfSisters { get; set; }

    public string? BatchName { get; set; }

    public string? ApplicationNumber { get; set; }

    public string? StudentStudentId { get; set; }

    public string? Siblings { get; set; }
}
