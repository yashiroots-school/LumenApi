using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class ClassAndSection
{
    public int Id { get; set; }

    public string? Class { get; set; }

    public string? Section { get; set; }

    public string? OtherSection { get; set; }
}
