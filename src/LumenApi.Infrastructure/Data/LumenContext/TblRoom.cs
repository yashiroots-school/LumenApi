using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblRoom
{
    public int RoomId { get; set; }

    public string? RoomName { get; set; }

    public string? RoomNo { get; set; }

    public string? RoomType { get; set; }

    public string? SeatingCapacity { get; set; }

    public string? Location { get; set; }

    public string? Remarks { get; set; }

    public int RoomTypeId { get; set; }
}
