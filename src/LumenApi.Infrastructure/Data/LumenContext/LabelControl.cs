using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class LabelControl
{
    public int Id { get; set; }

    public int LableId { get; set; }

    public string? LabelName { get; set; }

    public bool IsActive { get; set; }

    public int SchoolId { get; set; }
}
