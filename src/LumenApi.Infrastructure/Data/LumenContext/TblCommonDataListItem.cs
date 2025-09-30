using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCommonDataListItem
{
    public int DatalistId { get; set; }

    public string? DataListName { get; set; }

    public string? DataListItemName { get; set; }

    public string? Status { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }
  public int? DataListItemId { get; set; }
}
