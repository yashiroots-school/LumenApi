using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class PastSchoolingReport
{
    public int Id { get; set; }

    public string? NameOfSchoolLastAttended { get; set; }

    public string? ClassPassed { get; set; }

    public string? ReasonForLeaving { get; set; }

    public string? Tcavatar { get; set; }

    public string? MarksCardAvatar { get; set; }

    public string? CharacterConductCertificateAvatar { get; set; }

    public int StudentRefId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? Promotion { get; set; }

    public string? BatchName { get; set; }

    public int? StudentStudentId { get; set; }

    public virtual Student? StudentStudent { get; set; }
}
