using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSummerInternship
{
    public long SummerInternshipId { get; set; }

    public string ScholarNumber { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? StartDate { get; set; }

    public string? MobileNo { get; set; }

    public string? EndDate { get; set; }

    public string? ProjectTitle { get; set; }

    public string? FacultyProjectGuide { get; set; }

    public string? FacultyGuideMobileNo { get; set; }

    public string? IndustryGuideName { get; set; }

    public string? IndustryGuideDesignation { get; set; }

    public string? IndustryGuideTelNo { get; set; }

    public string? IndustryGuideMobileNo { get; set; }

    public string? IndustryGuideEmail { get; set; }

    public string? StipendinThousands { get; set; }

    public string? ProjectDescription { get; set; }

    public string? ProjectSubmission { get; set; }

    public string? ReasonforNoSubmission { get; set; }

    public string? PrePlacementOfferReceived { get; set; }

    public string? Feedback { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }

    public virtual TblStudentDetail ScholarNumberNavigation { get; set; } = null!;
}
