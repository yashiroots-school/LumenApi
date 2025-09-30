using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblAccountSummary
{
    public int SummaryId { get; set; }

    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public int NetPay { get; set; }

    public int Pf { get; set; }

    public int BasicSalary { get; set; }

    public int DeductionAmt { get; set; }

    public int ArrearAmt { get; set; }

    public string? Arrear { get; set; }

    public int Da { get; set; }

    public int ProfessionalTax { get; set; }

    public string? AddedDate { get; set; }

    public string? AddedMonth { get; set; }

    public string? AddedYear { get; set; }

    public string? AddedDay { get; set; }

    public int EmployeeContribution { get; set; }

    public int EmployerContribution { get; set; }

    public int NetPay1 { get; set; }

    public int AttendencePercentage { get; set; }

    public int Esi { get; set; }

    public int Gross { get; set; }

    public int TotalSalary { get; set; }

    public double Lop { get; set; }

    public int Cca { get; set; }

    public int Hra { get; set; }

    public int OtherAllowance { get; set; }

    public string? NoOfdayspresent { get; set; }

    public int TotalPercentage { get; set; }
}
