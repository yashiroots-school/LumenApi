using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblDataListItem
{
    public int DataListItemId { get; set; }

    public string? DataListItemName { get; set; } = string.Empty;

    public string? DataListId { get; set; } = string.Empty;

    public string? DataListName { get; set; } = string.Empty;

    public string? Status { get; set; } = string.Empty;
}
