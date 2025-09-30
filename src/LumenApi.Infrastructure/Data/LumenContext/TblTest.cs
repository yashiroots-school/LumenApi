using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LumenApi.Web;

public partial class TblTests
{
  [Key]
  public long TestId { get; set; }

    public long ClassId { get; set; }

    public long SubjectId { get; set; }

    public string? TestName { get; set; }

    public string? TestType { get; set; }

    public decimal MaximumMarks { get; set; }
    public decimal? MinimumMarks { get; set; }
    public long TermId { get; set; }
    public long BoardId { get; set; }

    public bool IsOptional { get; set; }
}
