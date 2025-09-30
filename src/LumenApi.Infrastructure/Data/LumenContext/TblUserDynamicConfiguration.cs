using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblUserDynamicConfiguration
{
    public int Mainid { get; set; }

    public string? ListType { get; set; }

    public string? ListData { get; set; }

    public string? CurrentUser { get; set; }
}
