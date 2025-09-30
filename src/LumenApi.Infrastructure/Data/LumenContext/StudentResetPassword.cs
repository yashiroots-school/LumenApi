using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentResetPassword
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string ResetKey { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }
}
