using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSalaryStatement
{
    public int SalaryStatementId { get; set; }

    public string? EmployersDesignation { get; set; }

    public string? EmployeeName { get; set; }

    public int EmployeeCode { get; set; }

    public string? EmployeeAccountNo { get; set; }

    public string? TotalSalary { get; set; }

    public int AccountDetailsId { get; set; }

    public string? AccountDetails { get; set; }

    public string? SalarystatementMonth { get; set; }

    public string? SalarystatementYear { get; set; }

    public string? SalaryStatementDate { get; set; }

    public int StaffCategoryId { get; set; }
}
