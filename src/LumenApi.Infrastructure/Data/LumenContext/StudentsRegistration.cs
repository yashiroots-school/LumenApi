using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentsRegistration
{
    public long StudentRegisterId { get; set; }

    public string ApplicationNumber { get; set; } = null!;

    public string Uin { get; set; } = null!;

    public string? Date { get; set; }

    public string Name { get; set; } = null!;

    public string? Class { get; set; }

    public string? Section { get; set; }

    public string? Gender { get; set; }

    public string? Rte { get; set; }

    public string? Medium { get; set; }

    public string? Caste { get; set; }

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

    public string? MarkForIdentity { get; set; }

    public string? AdharNo { get; set; }

    public string? AdharFile { get; set; }

    public string? OtherLanguages { get; set; }

    public bool IsApplyforTc { get; set; }

    public bool IsApplyforAdmission { get; set; }

    public int IsApprove { get; set; }

    public bool IsActive { get; set; }

    public bool? IsInsertFromAd { get; set; }

    public bool? IsAdmissionPaid { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? Email { get; set; }

    public string? LastStudiedSchoolName { get; set; }

    public string? ParentsEmail { get; set; }

    public int ClassId { get; set; }

    public int SectionId { get; set; }

    public string? LastName { get; set; }

    public int BatchId { get; set; }

    public string? BatchName1 { get; set; }

    public int BloodGroupId { get; set; }

    public int ReligionId { get; set; }

    public int CastId { get; set; }

    public int CategoryId { get; set; }

    public string? ClassName { get; set; }

    public string? SectionName { get; set; }

    public string? Transport { get; set; }

    public string? TransportOptions { get; set; }

    public string? Mobile { get; set; }

    public string? AdmissionFeePaid { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public string? AddedYear { get; set; }

    public string? RegistrationDate { get; set; }

    public bool IsEmailsent { get; set; }

    public string? PromotionDate { get; set; }

    public string? PromotionYear { get; set; }

    public string? EmailSendDate { get; set; }

    public int EmailSend { get; set; }

    public string? GradeDivision { get; set; }

    public string? House { get; set; }

    public string? Hostel { get; set; }

    public string? Status { get; set; }

    public string? SssmidNumber { get; set; }

    public string? Role { get; set; }

    public string? Designation { get; set; }

    public string? IsRtestudent { get; set; }

    public string? IsInDayCare { get; set; }

    public string? FamilySssmid { get; set; }

    public string? BankAccount { get; set; }

    public string? BankName { get; set; }

    public string? BankAcholder { get; set; }

    public string? BankIfsc { get; set; }

    public string? Subjects { get; set; }

    public string? OptionalSubjects { get; set; }

    public string? School { get; set; }

    public string? IsUserLoggedIn { get; set; }

    public string? LastLoginDate { get; set; }

    public long? RollNo { get; set; }

    public long? ScholarNo { get; set; }
}
