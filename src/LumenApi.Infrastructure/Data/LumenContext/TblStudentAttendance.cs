using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenApi.Web;

public partial class TblStudentAttendance
{
    public int AttendanceId { get; set; }

    public int ClassId { get; set; }

    public int SectionId { get; set; }

    public string? ClassName { get; set; }

    public string? SectionName { get; set; }

    public string? MarkFullDayAbsent { get; set; }

    public string? MarkHalfDayAbsent { get; set; }

    public int StudentRegisterId { get; set; }

    public string? StudentName { get; set; }

    public string CreatedDate { get; set; }=string.Empty;

    public string? Day { get; set; }

    public string? CreatedBy { get; set; }

    public string? Others { get; set; }
  public int? BatchId { get; set; }
}

[Table("MobileAppVersions")]
public  class MobileAppVersions
{
  public int Id { get; set; }

  public string VersionName { get; set; }=string.Empty ;  

  public DateTime? Date { get; set; }

  

  [Required]
  [DataType(DataType.DateTime)]
  public DateTime CurrentDate { get; set; } // save current date+time

  // REMOVE public int DaysAgo { get; set; }
}
public class tbl_CalendarEvents
{
  [Key]
  public int ID { get; set; }   // Primary key

  [Required]
  // [StringLength(255)]
  public string EventName { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Date)]
  public DateTime EventDate { get; set; }

  [Required]
  [DataType(DataType.Time)]
  public TimeSpan EventTime { get; set; }
}

public class tbl_Notice
{
  [Key]
  public int ID { get; set; }

  [Required]
  [StringLength(255)]
  public string NoticeName { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Date)]
  public DateTime NoticeDate { get; set; }


  [Required]
  [DataType(DataType.DateTime)]
  public DateTime CurrentDate { get; set; } // save current date+time

  // REMOVE public int DaysAgo { get; set; }
}
public class NoticeDto
{
  public string NoticeName { get; set; } = string.Empty;
  public DateTime NoticeDate { get; set; }
}
