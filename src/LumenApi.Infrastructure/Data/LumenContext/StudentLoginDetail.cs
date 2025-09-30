using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentLoginDetail
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
