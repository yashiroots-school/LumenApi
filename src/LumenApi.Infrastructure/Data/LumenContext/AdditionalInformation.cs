using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class AdditionalInformation
{
    public int Id { get; set; }

    public string? AssignClass { get; set; }

    public string? AssignSection { get; set; }

    public string? Remarks { get; set; }

    public string? Grade { get; set; }

    public string? FeeStructureApplicable { get; set; }

    public float DistancefromSchool { get; set; }

    public string? TransportFacility { get; set; }

    public string? BirthCertificateAvatar { get; set; }

    public string? ThreePassportSizePhotographs { get; set; }

    public string? ProgressReport { get; set; }

    public string? MigrationCertificate { get; set; }

    public int StudentRefId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? Group { get; set; }

    public string? BatchName { get; set; }

    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int SectionId { get; set; }

    public string? SectionName { get; set; }

    public int? StudentStudentId { get; set; }

    public string? TransportOptions { get; set; }

    public bool Physicallychalanged { get; set; }

    public string? IncomeCertificate { get; set; }

    public string? CastCertificate { get; set; }

    public string? FatherAdhar { get; set; }

    public string? MotherAdhar { get; set; }

    public string? BankBook { get; set; }

    public string? Ssmid { get; set; }

    public string? TransportVehicleNo { get; set; }

    public virtual Student? StudentStudent { get; set; }
}
