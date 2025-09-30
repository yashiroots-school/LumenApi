using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblRemark
{
    public long RemarkId { get; set; }

    public string? Remark { get; set; }

    public long? TermId { get; set; }

    public long? BoardId { get; set; }

    public long? StudentId { get; set; }
  public int? BatchId { get; set; }
}
