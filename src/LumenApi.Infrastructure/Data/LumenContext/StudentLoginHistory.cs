using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentLoginHistory
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string UserPassword { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }
}
