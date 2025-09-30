using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class Classroom
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public string? RoomNo { get; set; }

    public string? RoomType { get; set; }

    public string? Seatingcapacity { get; set; }

    public string? Location { get; set; }

    public string? Remarks { get; set; }
}
