using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSemester
{
    public long SemesterId { get; set; }

    public string ScholarNumber { get; set; } = null!;

    public string? Year { get; set; }

    public string? Sem { get; set; }

    public string? Percentage { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }

    public decimal Perse2 { get; set; }

    public decimal Persentagegrade { get; set; }
}
