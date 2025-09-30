using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblWorkExperience
{
    public long WorkExperienceId { get; set; }

    public string ScholarNumber { get; set; } = null!;

    public string? TotalExperience { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyProfile { get; set; }

    public string? Designation { get; set; }

    public int FromDate { get; set; }

    public string? ToDate { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }
}
