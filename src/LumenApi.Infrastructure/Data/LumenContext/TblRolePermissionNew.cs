using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblRolePermissionNew
{
    public int RolepermissionId { get; set; }

    public string? RoleId { get; set; }

    public int MenuId { get; set; }

    public int SubmenuId { get; set; }

    public string? SubmenuUrl { get; set; }

    public bool CreatePermission { get; set; }

    public bool EditPermission { get; set; }

    public bool UpdatePermission { get; set; }

    public bool DeletePermission { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? SubmenuName { get; set; }

    public bool SubmenuPermission { get; set; }

    public int StaffId { get; set; }

    public string? StaffName { get; set; }
}
