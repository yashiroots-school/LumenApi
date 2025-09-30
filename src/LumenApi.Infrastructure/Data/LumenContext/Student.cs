using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Student
{
    public int StudentId { get; set; }

    public string ApplicationNumber { get; set; } = null!;

    public string Uin { get; set; } = null!;

    public string? Date { get; set; }

    public string Name { get; set; } = null!;

    public string? Class { get; set; }

    public string? Section { get; set; }

    public string? Gender { get; set; }

    public int AgeInWords { get; set; }

    public string? Dob { get; set; }

    public string? Pob { get; set; }

    public string? Nationality { get; set; }

    public string? Religion { get; set; }

    public string? MotherTongue { get; set; }

    public string? Category { get; set; }

    public string? BloodGroup { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Hobbies { get; set; }

    public string? Sports { get; set; }

    public string? OtherDetails { get; set; }

    public string? ProfileAvatar { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? MarkForIdentity { get; set; }

    public string? OtherLanguages { get; set; }

    public string? Medium { get; set; }

    public string? Caste { get; set; }

    public string? Rte { get; set; }

    public string? AdharNo { get; set; }

    public string? AdharFile { get; set; }

    public string? BatchName { get; set; }

    public bool IsApplyforTc { get; set; }

    public bool IsApplyforAdmission { get; set; }

    public int? IsApprove { get; set; }

    public bool IsActive { get; set; }

    public bool? IsInsertFromAd { get; set; }

    public bool? IsAdmissionPaid { get; set; }

    public string? RegNumber { get; set; }

    public int ClassId { get; set; }

    public int CategoryId { get; set; }

    public int BatchId { get; set; }

    public string? ParentEmail { get; set; }

    public string? AdmissionFeePaid { get; set; }

    public string? LastName { get; set; }

    public string? Transport { get; set; }

    public string? TransportOptions { get; set; }

    public string? Mobile { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public int BloodGroupId { get; set; }

    public bool IsPromoted { get; set; }

    public int? SectionId { get; set; }

     

    public long? RollNo { get; set; }

    public long? ScholarNo { get; set; }

    public virtual ICollection<AdditionalInformation> AdditionalInformations { get; set; } = new List<AdditionalInformation>();

    public virtual ICollection<GuardianDetail> GuardianDetails { get; set; } = new List<GuardianDetail>();

    public virtual ICollection<PastSchoolingReport> PastSchoolingReports { get; set; } = new List<PastSchoolingReport>();

    public virtual ICollection<StudentRemoteAccess> StudentRemoteAccesses { get; set; } = new List<StudentRemoteAccess>();

    public virtual ICollection<StudentTcDetail> StudentTcDetails { get; set; } = new List<StudentTcDetail>();

    public virtual ICollection<TcFeeDetail> TcFeeDetails { get; set; } = new List<TcFeeDetail>();
}
