using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class MasterLabel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? LableId { get; set; }

    public int SubMenuId { get; set; }
}
