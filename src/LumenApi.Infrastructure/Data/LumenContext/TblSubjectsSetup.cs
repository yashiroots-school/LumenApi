using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSubjectsSetup
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public bool? IsElective { get; set; }
}
