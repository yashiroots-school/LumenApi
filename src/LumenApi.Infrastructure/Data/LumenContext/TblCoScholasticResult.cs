using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCoScholasticResult
{
    public long Id { get; set; }

    public long? BoardId { get; set; }

    public string? ObtainedGrade { get; set; }

    public long? StudentId { get; set; }

    public long? CoScholasticId { get; set; }

    public long? ClassId { get; set; }

    public long? TermId { get; set; }

    public long? SectionId { get; set; }
}
