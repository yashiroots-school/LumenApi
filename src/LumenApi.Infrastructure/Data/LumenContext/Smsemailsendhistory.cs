using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Smsemailsendhistory
{
    public long Historyid { get; set; }

    public int Senderid { get; set; }

    public string? Sendertype { get; set; }

    public string? Sms { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Attachedfile { get; set; }

    public string? Attachedfiletype { get; set; }

    public string? Attachedfilename { get; set; }

    public DateTime Createddate { get; set; }
}
