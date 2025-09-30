using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblRevision
{
    public int RevisionId { get; set; }

    public string? ClassName { get; set; }

    public int ClassId { get; set; }

    public string? SectionName { get; set; }

    public int SectionId { get; set; }

    public string? SubjectName { get; set; }

    public int SubjectId { get; set; }

    public string? RevisionDate { get; set; }

    public string? Description { get; set; }

    public string? CreatedDate { get; set; }
}
