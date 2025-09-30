using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblArchieveChangeStaffAccounttype
{
    public int ChangeAccounTypeId { get; set; }

    public int StafId { get; set; }

    public string? EmpId { get; set; }

    public string? StafName { get; set; }

    public string? EmployeeDesignation { get; set; }

    public int EmployeeAccountId { get; set; }

    public string? EmployeeAccountName { get; set; }

    public int CategoryId { get; set; }

    public string? StaffCategoryName { get; set; }

    public string? EmployeeCode { get; set; }
}
