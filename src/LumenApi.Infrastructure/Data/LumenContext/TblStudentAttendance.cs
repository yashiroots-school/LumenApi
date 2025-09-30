using System;
using System.Collections.Generic;
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

  
}
