using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTestRecord
{
    public long RecordId { get; set; }

    public long StudentId { get; set; }

    public long? TestId { get; set; }

    public long? TermId { get; set; }

    public long? ClassId { get; set; }

    public long? SectionId { get; set; }

    public decimal? ObtainedMarks { get; set; }
  public int? BatchId { get; set; }

  public long? BoardId { get; set; }
  public int? RankInClass { get; set; }
}
