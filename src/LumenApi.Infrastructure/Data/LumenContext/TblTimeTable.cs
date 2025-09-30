using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblTimeTable
{
    public int TimeTableId { get; set; }

    public string? ClassName { get; set; }

    public int ClassId { get; set; }

    public string? SectionName { get; set; }

    public int SectionId { get; set; }

    public string? StaffName { get; set; }

    public int StafId { get; set; }

    public int RoomId { get; set; }

    public string? RoomName { get; set; }

    public int DayTimeId { get; set; }

    public string? CreatedDate { get; set; }

    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public string? Date { get; set; }

    public int DayId { get; set; }

    public int TimeId { get; set; }
}
