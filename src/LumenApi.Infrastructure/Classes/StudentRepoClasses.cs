using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.Web;

namespace LumenApi.Infrastructure.Classes;
  internal class StudentRepoClasses
  {
  }


  public class AttendanceRecord
  {
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public List<TblStudentAttendance> Attendance { get; set; } =new List<TblStudentAttendance>();
    public string AttendancePer { get; set; } = string.Empty;
  public string TotalDays { get; set; } = string.Empty;
  public string TotalAttendedDays { get; set; } = string.Empty;
}


