using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class MasterReport
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
