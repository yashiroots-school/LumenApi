using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LumenApi.Web;

public partial class TblStaffAttendance
{
  [Key]
  public int StaffAtte_Id { get; set; }
  public int Staff_Id { get; set; }
  public string? Staff_Name { get; set; }

  public string? Mark_FullDayAbsent { get; set; }  // nvarchar
  public string? Mark_HalfDayAbsent { get; set; }
  public string? Mark_Other { get; set; }
  public string? Mark_CL { get; set; }
  public string? Mark_ML { get; set; }
  public string? Mark_L { get; set; }
  public string? Mark_FullDayPresent { get; set; }
               
  public string? Total { get; set; }

  public DateTime AddedDate { get; set; }
  public DateTime ModifiedDate { get; set; }
  public int CurrentYear { get; set; }
  public string? IP { get; set; }
  public string? UserId { get; set; }

  public bool IsDeleted { get; set; }
  public int CreateBy { get; set; }
  public string? InsertBy { get; set; }
  public string? BatchName { get; set; }

  public string? Attendence_Date { get; set; } // nvarchar "dd/MM/yyyy"
  public int Attendence_Day { get; set; }
  public string? Attendence_Month { get; set; }  // nvarchar
  public string? Attendence_Year { get; set; }   // nvarchar
  public string? Employee_Code { get; set; }
}


