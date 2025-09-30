using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTerm
{
    public long TermId { get; set; }

    public string? TermName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? BoardId { get; set; }

    public long? BatchId { get; set; }

    public long? ClassId { get; set; }
}
