using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class ReportHeading
{
    public long Id { get; set; }

    public long ReportId { get; set; }

    public int HeadingId { get; set; }

    public int OrderNo { get; set; }
}
