using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTeacherAllocation
{
    public int AllocateId { get; set; }

    public string? TeacherName { get; set; }

    public string? ClassName { get; set; }

    public string? SubjectName { get; set; }

    public int StaffId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }
}
