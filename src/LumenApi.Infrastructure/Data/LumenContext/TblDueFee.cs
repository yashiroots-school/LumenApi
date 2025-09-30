using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblDueFee
{
    public int DueFeeId { get; set; }

    public int FeeHeadingId { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Jan { get; set; }

    public string? Feb { get; set; }

    public string? Mar { get; set; }

    public string? Apr { get; set; }

    public string? May { get; set; }

    public string? Jun { get; set; }

    public string? Jul { get; set; }

    public string? Aug { get; set; }

    public string? Sep { get; set; }

    public string? Oct { get; set; }

    public string? Nov { get; set; }

    public string? Dec { get; set; }

    public string? PaidMonths { get; set; }

    public int ClassId { get; set; }

    public int CategoryId { get; set; }

    public string? ClassName { get; set; }

    public string? CategoryName { get; set; }

    public string? PayHeadings { get; set; }

    public float TotalFee { get; set; }

    public string? FeePaids { get; set; }

    public string? Course { get; set; }

    public string? CourseSpecialization { get; set; }

    public string? FeeReceiptsOneTimeCreator { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? BatchName { get; set; }

    public string? FeeHeading { get; set; }

    public float PaidAmount { get; set; }

    public float DueAmount { get; set; }
}
