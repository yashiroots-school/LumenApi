using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class FeeHeading
{
    public int FeeId { get; set; }

    public string? FeeName { get; set; }

    public int FeeFrequencyId { get; set; }

    public string? FeeFrequencyName { get; set; }

    public byte Jan { get; set; }

    public byte Mar { get; set; }

    public byte Apr { get; set; }

    public byte May { get; set; }

    public byte Jun { get; set; }

    public byte Jul { get; set; }

    public byte Aug { get; set; }

    public byte Sep { get; set; }

    public byte Oct { get; set; }

    public byte Nov { get; set; }

    public byte Dec { get; set; }

    public int? FeeHeadingGroupsFeeHeadingGroupId { get; set; }

    public int? AccountsAccountId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public byte Feb { get; set; }

    public int CurrentYear { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public int FeeTypeId { get; set; }

    public string? Active { get; set; }

    public virtual Account? AccountsAccount { get; set; }

    public virtual Frequency FeeFrequency { get; set; } = null!;

    public virtual FeeHeadingGroup? FeeHeadingGroupsFeeHeadingGroup { get; set; }
}
