using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class StudentRegNumberMaster
{
    public int StudnetRegNumberMasterId { get; set; }

    public string? Class { get; set; }

    public string? BatchName { get; set; }

    public string? RegPrefix { get; set; }

    public int? RegLength { get; set; }

    public int? RegNumberStartWith { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? RegStatus { get; set; }

    public int? RegLastNumber { get; set; }

    public int ClassId { get; set; }

    public int BatchId { get; set; }
}
