using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Subject
{
    public int Id { get; set; }

    public string? Teacher { get; set; }

    public string? Class { get; set; }

    public string? Subject1 { get; set; }

    public int StaffId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int BatchId { get; set; }

    public bool ClassTeacher { get; set; }

    public int? SectionId { get; set; }

    public string? Section { get; set; }
}
