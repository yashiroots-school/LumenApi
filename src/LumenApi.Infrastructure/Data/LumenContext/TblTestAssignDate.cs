using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTestAssignDates
{
    public long TestAssignId { get; set; }

    public long? TestId { get; set; }

    public long? BoardId { get; set; }

    public long? BatchId { get; set; }

    public long? ClassId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
