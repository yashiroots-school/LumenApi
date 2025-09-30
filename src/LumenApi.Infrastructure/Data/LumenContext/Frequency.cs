using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Frequency
{
    public int FeeFrequencyId { get; set; }

    public string? FeeFrequencyName { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public virtual ICollection<FeeHeading> FeeHeadings { get; set; } = new List<FeeHeading>();
}
