using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblClassSubjects 
{
    public long Id { get; set; }

    public long ClassId { get; set; }

    public long SubjectId { get; set; }

    public long BoardId { get; set; }

    public bool? IsElective { get; set; }
}
