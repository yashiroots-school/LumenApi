using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblStaffSalary
{
    public int SalaryId { get; set; }

    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public int SalaryAmount { get; set; }

    public string? CreatedDate { get; set; }

    public int BasicAmount { get; set; }
}
