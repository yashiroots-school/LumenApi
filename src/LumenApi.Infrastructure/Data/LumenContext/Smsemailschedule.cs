using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Smsemailschedule
{
    public long Smsemailscheduleid { get; set; }

    public string? Scheduletype { get; set; }

    public DateTime Createddate { get; set; }
}
