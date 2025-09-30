using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentRemoteAccess
{
    public int Id { get; set; }

    public string? EnterDesiredlogin { get; set; }

    public string? Password { get; set; }

    public int StudentRefId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public virtual Student StudentRef { get; set; } = null!;
}
