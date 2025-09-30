using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSchoolSetup
{
    public int SchoolsetupId { get; set; }

    public int SchoolId { get; set; }

    public int BankId { get; set; }

    public int BranchId { get; set; }

    public int MerchantNameId { get; set; }

    public string? Status { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? FeeConfigurationId { get; set; }

    public string? FeeConfiguratinname { get; set; }
}
