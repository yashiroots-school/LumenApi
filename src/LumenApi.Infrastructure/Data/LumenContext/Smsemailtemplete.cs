using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Smsemailtemplete
{
    public long Smsemailid { get; set; }

    public string? Notificationtype { get; set; }

    public string? Sms { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Attachedfile { get; set; }

    public string? Attachedfiletype { get; set; }

    public string? Attachedfilename { get; set; }

    public string? Createddate { get; set; }

    public string? Smssubject { get; set; }
}
