using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class MigrationHistory22032021
{
    public string MigrationId { get; set; } = null!;

    public string ContextKey { get; set; } = null!;

    public byte[] Model { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
