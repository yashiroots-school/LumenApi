using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class RolePagePermission
{
    public int Id { get; set; }

    public string? RoleId { get; set; }

    public string? PageName { get; set; }

    public bool HasPermission { get; set; }

    public string? RoleName { get; set; }

    public string? PageViewName { get; set; }

    public int ParentId { get; set; }
}
