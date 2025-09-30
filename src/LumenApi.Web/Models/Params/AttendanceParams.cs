using LumenApi.Web.Services;

namespace LumenApi.Web.Models.Params;

public class AttendanceParams : IStudentAttendance
{
  public int AttendanceId { get;set;} 
  public int ClassId { get; set; }

  public int SectionId { get; set; }

  public string? ClassName { get; set; }

  public string? SectionName { get; set; }

  public string? MarkFullDayAbsent { get; set; }

  public string? MarkHalfDayAbsent { get; set; }

  public int StudentRegisterId { get; set; }

  public string? StudentName { get; set; }

  public string? CreatedDate { get; set; }

  public string? Day { get; set; }

  public string? CreatedBy { get; set; }

  public string? Others { get; set; }
}


public class StudentAttendanceParams
{
  public int classId {get;set;}
   public  int sectionId {get;set;}
  public string fromDate { get; set; } = null!;
   public  string toDate  {get;set;} = null!;
  public  int studentId {get;set;}
  public int batchId { get; set; }
}
