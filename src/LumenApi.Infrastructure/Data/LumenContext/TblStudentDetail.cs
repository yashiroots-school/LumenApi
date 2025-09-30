using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblStudentDetail
{
    public string ScholarNumber { get; set; } = null!;

    public long StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Course { get; set; }

    public string? Years { get; set; }

    public string? Batch { get; set; }

    public string? Specialization { get; set; }

    public string? Sibiling1 { get; set; }

    public string? Sibiling2 { get; set; }

    public string? Sibiling3 { get; set; }

    public string? Sibiling4 { get; set; }

    public string? Sibiling5 { get; set; }

    public string? Category { get; set; }

    public string? FacultyMentor { get; set; }

    public string? DateofBirth { get; set; }

    public string? Age { get; set; }

    public string? Gender { get; set; }

    public string? CorrespondenceAddress { get; set; }

    public string? ResidenceLocation { get; set; }

    public string? CountryCode { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public string? OutStationStudent { get; set; }

    public string? NativePlace { get; set; }

    public string? Hostalite { get; set; }

    public string? FatherName { get; set; }

    public string? FatherProfession { get; set; }

    public string? FatherCountryCode { get; set; }

    public string? FatherMobileNo { get; set; }

    public string? FatherEmailId { get; set; }

    public string? FatherCompanyName { get; set; }

    public string? MotherName { get; set; }

    public string? MotherProfession { get; set; }

    public string? MotherCountryCode { get; set; }

    public string? MotherMobileNo { get; set; }

    public string? MotherEmailId { get; set; }

    public string? MotherCompanyName { get; set; }

    public string? Status { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }

    public string? Cmcremarks { get; set; }

    public string? DateOn { get; set; }

    public string? Class { get; set; }

    public string? Religious { get; set; }

    public string? ReligiousOther { get; set; }

    public int ClassId { get; set; }

    public int BatchId { get; set; }

    public virtual ICollection<TblDeclaration> TblDeclarations { get; set; } = new List<TblDeclaration>();

    public virtual ICollection<TblSummerInternship> TblSummerInternships { get; set; } = new List<TblSummerInternship>();
}
