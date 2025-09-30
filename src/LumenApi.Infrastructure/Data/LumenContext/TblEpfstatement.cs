using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblEpfstatement
{
    public int EpfstatementId { get; set; }

    public string? EmployeeCode { get; set; }

    public string? Uin { get; set; }

    public string? EmployeeName { get; set; }

    public int GrossWages { get; set; }

    public int EpfWages { get; set; }

    public int EpsWages { get; set; }

    public int Edliwages { get; set; }

    public int EmployeContribution { get; set; }

    public int EmployerContribution { get; set; }

    public int EpsPension { get; set; }

    public int NcpDays { get; set; }

    public int RefundAdvances { get; set; }

    public string? AddedDate { get; set; }

    public string? AddedDay { get; set; }

    public string? AddedMonth { get; set; }

    public string? AddedYear { get; set; }

    public int StaffCategoryId { get; set; }
}
