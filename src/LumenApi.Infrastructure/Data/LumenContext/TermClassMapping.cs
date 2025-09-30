using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TermClassMapping
{
    public int Id { get; set; }

    public long? TermId { get; set; }

    public int? ClassId { get; set; }
}
