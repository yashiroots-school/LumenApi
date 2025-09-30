using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblAcademicDetail
{
    public long AcademicDetailId { get; set; }

    public int NewProperty { get; set; }

    public string ScholarNumber { get; set; } = null!;

    public string? AcademicYear { get; set; }

    public string? Qualification { get; set; }

    public string? Institution { get; set; }

    public string? University { get; set; }

    public decimal Percentage { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }

    public string? Dateon { get; set; }

    public string? Stream { get; set; }
}
