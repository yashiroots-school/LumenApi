

//using System.Data.Entity;
//using System.Data.Entity;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Infrastructure.Classes;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Collections.Specialized.BitVector32;

namespace LumenApi.Web.Services;


public interface IStudentAttendance
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

  public string? CreatedDate { get; set; }

  public string? Day { get; set; }

  public string? CreatedBy { get; set; }

  public string? Others { get; set; }

}
public interface IAttendanceService
{
  Task<IApiResponse> GetStudentAttendance(StudentAttendanceParams studentAttendance);

  Task<IApiResponse> SaveStudentAttendance(List<AttendanceParams> AttendanceData);
  Task<IApiResponse> EditStudenAttendance(List<AttendanceParams> AttendanceData);
  Task<IApiResponse> DeleteStudenAttendance(List<AttendanceParams> AttendanceData);
  Task<IApiResponse> StudentAtendance(string? Role, string? stafId);
  Task<IApiResponse> SaveOrUpdateStaffAttendanceAsync(List<TblStaffAttendance> model);
  Task<IApiResponse> GetAttendanceByDateRangeAsync(string startDateStr, string endDateStr, int? staffId = null);
}
public class AttendanceService(Lumen090923Context lumen) : IAttendanceService
{
  private readonly Lumen090923Context _lumen = lumen;
  //public async Task<IApiResponse> GetStudentAttendance(StudentAttendanceParams studentAttendance)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {
  //    var stuAttend = await _lumen.TblStudentAttendances.ToListAsync();
  //    //  .Select(stuAtd => new AttendanceParams()
  //    //{
  //    //  AttendanceId = stuAtd.AttendanceId,
  //    //  ClassId = stuAtd.ClassId,
  //    //  ClassName = stuAtd.ClassName,
  //    //  SectionId = stuAtd.SectionId,
  //    //  SectionName = stuAtd.SectionName,
  //    //  MarkFullDayAbsent = stuAtd.MarkFullDayAbsent,
  //    //  MarkHalfDayAbsent = stuAtd.MarkHalfDayAbsent,
  //    //  StudentRegisterId = stuAtd.StudentRegisterId,
  //    //  StudentName = stuAtd.StudentName,
  //    //  CreatedDate = stuAtd.CreatedDate,
  //    //  CreatedBy = stuAtd.CreatedBy,
  //    //  Others = stuAtd.Others,
  //    //  Day = stuAtd.Day
  //    //}).ToListAsync();
  //    res.Data = stuAttend;
  //    res.Msg = $"{stuAttend.Count} records found.";
  //    res.ResponseCode = "201";

  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  public  Task<IApiResponse> GetStudentAttendance(StudentAttendanceParams studentAttendance)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      //      DateTime from_Date = DateTime.ParseExact(studentAttendance.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      //      DateTime to_Date = DateTime.ParseExact(studentAttendance.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      //      var validStudentList = new List<TblStudentAttendance>();

      //      var studentlist = new List<TblStudentAttendance>();
      //      if (studentAttendance.studentId != 0)
      //      {
      //        studentlist = _lumen.TblStudentAttendances
      //        .Where(x => x.ClassId == studentAttendance.classId 
      //        && x.SectionId == studentAttendance.sectionId && 
      //        x.StudentRegisterId == studentAttendance.studentId).OrderBy(x => x.StudentName)
      //        .ToList()
      //        .Where(x => DateTime.ParseExact(x.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= from_Date &&
      //        DateTime.ParseExact(x.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= to_Date)
      //        .ToList();
      //        foreach (var student in studentlist)
      //        {
      //          if (DateTime.TryParseExact(student.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
      //          {
      //            if (parsedDate >= from_Date && parsedDate <= to_Date)
      //            {
      //              validStudentList.Add(student);
      //            }
      //          }

      //        }
      //      }
      //      else
      //      {
      //        var a = DateTime.ParseExact("01/07/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);
      //        var b = DateTime.ParseExact("15/07/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);
      //        studentlist =await _lumen.TblStudentAttendances
      //        .Where(x => x.ClassId == studentAttendance.classId && x.SectionId == studentAttendance.sectionId)
      //        .ToListAsync();
      //        studentlist.ForEach(x => x.StudentName = _lumen.Students.Where(y => y.StudentId == x.StudentRegisterId)
      //        .Select(z => z.Name).FirstOrDefault());
      //        foreach (var student in studentlist)
      //        {
      //          if (DateTime.TryParseExact(student.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
      //          {
      //            if (parsedDate >= from_Date && parsedDate <= to_Date)
      //            {
      //              validStudentList.Add(student);
      //            }
      //          }
      //          else
      //          {
      //          }
      //        }
      //        var ab = studentlist
      //.Where(x => x.ClassId == studentAttendance.classId && x.SectionId == studentAttendance.sectionId && DateTime.ParseExact(x.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= from_Date && DateTime.ParseExact(x.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= to_Date)
      //.ToList();
      //      }

      //      var attendanceRecords = validStudentList
      //        .Where(x => x.ClassId == studentAttendance.classId && x.SectionId == studentAttendance.sectionId)
      //        .GroupBy(r => r.StudentRegisterId)
      //        .Select(g => new AttendanceRecord
      //            {
      //              StudentId = g.Key,
      //              StudentName = g.First().StudentName!,
      //              Attendance = g.ToList()!,
      //              AttendancePer = "Some value"
      //            }).OrderBy(x => x.StudentName)
      //            .ToList();
      //      TimeSpan duration = to_Date - from_Date;
      //      double totalDays = duration.Days + 1;
      //      var filteredAttendanceRecords = attendanceRecords
      //        .Where(record =>
      //        !_lumen.Students.Any(student =>
      //          student.StudentId == record.StudentId && student.IsApplyforTc == true)
      //        )
      //        .ToList();


      //      foreach (var record in filteredAttendanceRecords)
      //      {

      //        double attendedDays = 0;
      //        double attendedHalfDays = 0;
      //        foreach (var item in record.Attendance)
      //        {
      //          if (DateTime.ParseExact(item.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= from_Date && DateTime.ParseExact(item.CreatedDate!, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= to_Date)
      //          {
      //            if (item.MarkFullDayAbsent == "True")
      //            {
      //              attendedDays++;
      //            }
      //            if (item.MarkHalfDayAbsent == "True")
      //            {
      //              attendedHalfDays++;
      //            }
      //            if (item.Others == "True")
      //            {
      //              attendedDays++;
      //            }
      //          }
      //        }
      //        double totalAttendedDays = attendedDays + (attendedHalfDays / 2);
      //        double attendancePercentage = (totalAttendedDays / totalDays) * 100;

      //        record.TotalAttendedDays = totalAttendedDays.ToString();
      //        record.TotalDays = totalDays.ToString();

      //        record.AttendancePer = attendancePercentage.ToString("F2") + "%";
      //      }
      //      res.Data = filteredAttendanceRecords.OrderBy(x => x.StudentName);
      //      res.ResponseCode = StatusCodes.Status200OK.ToString();
      //      res.Msg = "Data fetched successfully.";
      //parse prams string date into date
      DateTime from_Date = DateTime.ParseExact(studentAttendance.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      DateTime to_Date = DateTime.ParseExact(studentAttendance.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

      //var listOfStudents = new List<StudentsRegistration>();
      //var listOfAttendance = new List<Tbl_StudentAttendance>();

      //List of attendances without attendance date filter AND Apply Attendance date filter
      var listOfAttendance = _lumen.TblStudentAttendances
.Where(x => x.ClassId == studentAttendance.classId && x.SectionId == studentAttendance.sectionId && x.BatchId == studentAttendance.batchId && (studentAttendance.studentId == 0 || x.StudentRegisterId == studentAttendance.studentId))
.ToList().Where(x => DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= from_Date && DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= to_Date).OrderBy(x => x.StudentName).ToList();
      var listOfStudents = _lumen.Students.Where(x => x.ClassId == studentAttendance.classId && x.SectionId == studentAttendance.sectionId && x.BatchId == studentAttendance.batchId && (studentAttendance.studentId == 0 || x.StudentId == studentAttendance.studentId)).ToList();
      var attendanceRecords = new List<AttendanceRecord>();

      foreach (var student in listOfStudents)
      {
        var attendances = listOfAttendance.Where(x => x.StudentRegisterId == student.StudentId).ToList();
         if (!attendances.Any())
            {
                attendances.Add(new TblStudentAttendance
                {
                    StudentRegisterId = student.StudentId,
                    StudentName = student.Name,
                    CreatedDate = studentAttendance.fromDate,//from_Date.ToString("dd/MM/yyyy"),
                    MarkFullDayAbsent = "False",
                    MarkHalfDayAbsent="false",
                    ClassId=studentAttendance.classId,
                    SectionId=studentAttendance.classId,
                    BatchId=studentAttendance.batchId,
                    ClassName=student.Class,
                    SectionName=student.Section,
                    Others = "False",
                    Day="",
                    CreatedBy="",
                });
            }
        attendanceRecords.Add(new AttendanceRecord
        {
          StudentId = student.StudentId,
          StudentName = student.Name,
          Attendance = attendances,
          AttendancePer = "Some value"
        });
      }


      //        var attendanceRecords = validStudentList.Where(x => x.Class_Id == classId && x.Section_Id == sectionId && x.BatchId == BatchId)studentAttendance
      //.GroupBy(r => r.StudentRegisterID)
      //.Select(g => new AttendanceRecord
      //{
      //    StudentId = g.Key,
      //    StudentName = g.First().Student_Name,
      //    Attendance = g.ToList(),
      //    AttendancePer = "Some value"
      //}).OrderBy(x => x.StudentName)
      //.ToList();
      // Calculate attendance percentage

      TimeSpan duration = to_Date - from_Date;
      double totalDays = duration.Days + 1;
      var filteredAttendanceRecords = attendanceRecords
.Where(record =>
!_lumen.Students.Any(student =>
  student.StudentId == record.StudentId && student.IsApplyforTc == true
)
)
.ToList();

      //var attendanceRecords1 = studentlist.Where(x => x.Class_Id == classId && x.Section_Id == sectionId).ToList();
      foreach (var record in filteredAttendanceRecords)
      {

        double attendedDays = 0;
        double attendedHalfDays = 0;
        foreach (var item in record.Attendance)
        {
          if (DateTime.ParseExact(item.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= from_Date && DateTime.ParseExact(item.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= to_Date)
          {
            if (item.MarkFullDayAbsent == "True")
            {
              attendedDays++;
            }
            if (item.MarkFullDayAbsent == "True")
            {
              attendedHalfDays++;
            }
            if (item.Others == "True")
            {
              attendedDays++;
            }
          }
        }
        double totalAttendedDays = attendedDays + (attendedHalfDays / 2);
        double attendancePercentage = (totalAttendedDays / totalDays) * 100;
        record.AttendancePer = attendancePercentage.ToString("F2") + "%";
      }
      // return Json(filteredAttendanceRecords.OrderBy(x => x.StudentName), JsonRequestBehavior.AllowGet);
      res.Data = filteredAttendanceRecords.OrderBy(x => x.StudentName);
           res.ResponseCode = StatusCodes.Status200OK.ToString();
           res.Msg = "Data fetched successfully.";
    }


    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return Task.FromResult(res);
  }

  //public async Task<IApiResponse> SaveStudenAttendance(IStudentAttendance AttendanceData)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {


  //    EntityEntry<TblStudentAttendance> addStuData = await _lumen.TblStudentAttendances
  //  .AddAsync(new TblStudentAttendance()
  //  {
  //    ClassId = AttendanceData.ClassId,
  //    ClassName = AttendanceData.ClassName,
  //    SectionId = AttendanceData.SectionId,
  //    SectionName = AttendanceData.SectionName,
  //    MarkFullDayAbsent = AttendanceData.MarkFullDayAbsent,
  //    MarkHalfDayAbsent = AttendanceData.MarkHalfDayAbsent,
  //    StudentRegisterId = AttendanceData.StudentRegisterId,
  //    StudentName = AttendanceData.StudentName,
  //    CreatedDate = AttendanceData.CreatedDate?? string.Empty,
  //    CreatedBy = AttendanceData.CreatedBy,
  //    Others = AttendanceData.Others,
  //    Day = AttendanceData.Day
  //  });



  //    var isRecoredSaved = _lumen.SaveChanges();
  //    if (isRecoredSaved == 1)
  //    {

  //      res.Data = isRecoredSaved;
  //      res.Msg = "Record save successfully.";
  //      res.ResponseCode = "201";
  //    }
  //    else
  //    {
  //      res.Data = isRecoredSaved;
  //      res.Msg = "Something went worng .Please try again later.";
  //      res.ResponseCode = "400";
  //    }

  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  public async Task<IApiResponse> SaveStudentAttendance(List<AttendanceParams> attendanceDataList)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      if (attendanceDataList == null || !attendanceDataList.Any())
      {
        res.Msg = "No attendance data provided.";
        res.ResponseCode = "400";
        return res;
      }
      var CurrentYear=_lumen.TblBatches.Where(x=>x.IsActiveForPayments==true && x.IsActiveForAdmission==true).Select(x=>x.BatchId).FirstOrDefault();
      // Map to TblStudentAttendance entities
      var attendanceEntities = attendanceDataList.Select(attendance => new TblStudentAttendance
      {
        ClassId = attendance.ClassId,
        ClassName = attendance.ClassName,
        SectionId = attendance.SectionId,
        SectionName = attendance.SectionName,
        MarkFullDayAbsent = attendance.MarkFullDayAbsent,
        MarkHalfDayAbsent = attendance.MarkHalfDayAbsent,
        StudentRegisterId = attendance.StudentRegisterId,
        StudentName = attendance.StudentName,
        CreatedDate = attendance.CreatedDate ?? string.Empty,
        CreatedBy = attendance.CreatedBy,
        Others = attendance.Others,
        Day = attendance.Day,
        BatchId=CurrentYear,
      }).ToList();

      await _lumen.TblStudentAttendances.AddRangeAsync(attendanceEntities);
      var isRecordSaved = await _lumen.SaveChangesAsync();

      res.Data = isRecordSaved;
      res.Msg = isRecordSaved > 0 ? "Records saved successfully." : "Something went wrong. Please try again later.";
      res.ResponseCode = isRecordSaved > 0 ? "201" : "400";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  //public async Task<IApiResponse> EditStudenAttendance(List<AttendanceParams> attendanceDataList)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {
  //    if (attendanceDataList == null || !attendanceDataList.Any())
  //    {
  //      res.Msg = "No attendance data provided.";
  //      res.ResponseCode = "400";
  //      return res;
  //    }
  //    var CurrentYear = _lumen.TblBatches.Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission == true).Select(x => x.BatchId).FirstOrDefault();
  //    // Map to TblStudentAttendance entities
  //    var attendanceEntities = attendanceDataList.Select(attendance => new TblStudentAttendance
  //    {
  //      ClassId = attendance.ClassId,
  //      ClassName = attendance.ClassName,
  //      SectionId = attendance.SectionId,
  //      SectionName = attendance.SectionName,
  //      MarkFullDayAbsent = attendance.MarkFullDayAbsent,
  //      MarkHalfDayAbsent = attendance.MarkHalfDayAbsent,
  //      StudentRegisterId = attendance.StudentRegisterId,
  //      StudentName = attendance.StudentName,
  //      CreatedDate = attendance.CreatedDate ?? string.Empty,
  //      CreatedBy = attendance.CreatedBy,
  //      Others = attendance.Others,
  //      Day = attendance.Day,
  //      BatchId = CurrentYear,
  //    }).ToList();

  //    //await _lumen.TblStudentAttendances.AddRangeAsync(attendanceEntities);
  //    //var isRecordSaved = await _lumen.SaveChangesAsync();
  //   _lumen.Entry(attendanceEntities).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
  //   var isRecordSaved = await _lumen.SaveChangesAsync();

  //    res.Data = isRecordSaved;
  //    res.Msg = isRecordSaved > 0 ? "Records saved successfully." : "Something went wrong. Please try again later.";
  //    res.ResponseCode = isRecordSaved > 0 ? "201" : "400";
  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  public async Task<IApiResponse> EditStudenAttendance(List<AttendanceParams> attendanceDataList)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      if (attendanceDataList == null || !attendanceDataList.Any())
      {
        res.Msg = "No attendance data provided.";
        res.ResponseCode = "400";
        return res;
      }

      var CurrentYear = _lumen.TblBatches
          .Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission == true)
          .Select(x => x.BatchId)
          .FirstOrDefault();

      foreach (var attendance in attendanceDataList)
      {
        var existingAttendance = await _lumen.TblStudentAttendances
            .FirstOrDefaultAsync(x=>x.AttendanceId== attendance.AttendanceId);

        if (existingAttendance != null)
        {
          // Update existing record
          existingAttendance.ClassId = attendance.ClassId;
          existingAttendance.ClassName = attendance.ClassName;
          existingAttendance.SectionId = attendance.SectionId;
          existingAttendance.SectionName = attendance.SectionName;
          existingAttendance.MarkFullDayAbsent = attendance.MarkFullDayAbsent;
          existingAttendance.MarkHalfDayAbsent = attendance.MarkHalfDayAbsent;
          existingAttendance.StudentName = attendance.StudentName;
          existingAttendance.CreatedDate = attendance.CreatedDate??""; // Ensure correct format
          existingAttendance.CreatedBy = attendance.CreatedBy;
          existingAttendance.Others = attendance.Others;
          existingAttendance.BatchId = CurrentYear;
          _lumen.Entry(existingAttendance).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
        else
        {
          // Add new record if not found
          var newAttendance = new TblStudentAttendance
          {
            ClassId = attendance.ClassId,
            ClassName = attendance.ClassName,
            SectionId = attendance.SectionId,
            SectionName = attendance.SectionName,
            MarkFullDayAbsent = attendance.MarkFullDayAbsent,
            MarkHalfDayAbsent = attendance.MarkHalfDayAbsent,
            StudentRegisterId = attendance.StudentRegisterId,
            StudentName = attendance.StudentName,
            CreatedDate = attendance.CreatedDate??"",
            CreatedBy = attendance.CreatedBy,
            Others = attendance.Others,
            Day = attendance.Day,
            BatchId = CurrentYear,
          };

          await _lumen.TblStudentAttendances.AddAsync(newAttendance);
        }
      }

      var isRecordSaved = await _lumen.SaveChangesAsync();

      res.Data = isRecordSaved;
      res.Msg = isRecordSaved > 0 ? "Records saved successfully." : "Something went wrong. Please try again later.";
      res.ResponseCode = isRecordSaved > 0 ? "201" : "400";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> DeleteStudenAttendance(List<AttendanceParams> attendanceDataList)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      //TblStudentAttendance? editStuData = await _lumen.TblStudentAttendances.FindAsync(AttendanceData.AttendanceId);
      if (attendanceDataList == null || !attendanceDataList.Any())
      {
        res.Msg = "Student attendance data not found.Please provide valid student data.";
        res.ResponseCode = "201";
      }
      else
      {
        var attendanceIds = attendanceDataList.Select(a => a.AttendanceId).ToList();

        // Fetch attendance records that match the provided IDs
        var attendanceRecords = await _lumen.TblStudentAttendances
            .Where(a => attendanceIds.Contains(a.AttendanceId))
            .ToListAsync();

        if (!attendanceRecords.Any())
        {
          res.Msg = "No matching attendance records found.";
          res.ResponseCode = "404";
          return res;
        }

        // Remove the matched records
        _lumen.RemoveRange(attendanceRecords);
        int affectedRows = await _lumen.SaveChangesAsync();

        if (affectedRows > 0)
        {
          res.Data = affectedRows;
          res.Msg = "Records deleted successfully.";
          res.ResponseCode = "201";
        }
        else
        {
          res.Data = 0;
          res.Msg = "No records were deleted. Please try again.";
          res.ResponseCode = "400";
        }

      }

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  //public async Task<IApiResponse> EditStudenAttendance(IStudentAttendance AttendanceData)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {
  //    TblStudentAttendance? editStuData = await _lumen.TblStudentAttendances.FindAsync(AttendanceData.AttendanceId);
  //    if (editStuData == null)
  //    {
  //      res.Msg = "Student attendance data not found.Please provide valid student data.";
  //      res.ResponseCode = "201";
  //    }
  //    else
  //    {
  //      editStuData.AttendanceId = AttendanceData.AttendanceId;
  //      editStuData.SectionId = AttendanceData.SectionId;
  //      editStuData.SectionName = AttendanceData.SectionName;
  //      editStuData.ClassId = AttendanceData.ClassId;
  //      editStuData.ClassName = AttendanceData.ClassName;
  //      editStuData.StudentRegisterId = AttendanceData.StudentRegisterId;
  //      editStuData.StudentName = AttendanceData.StudentName;
  //      editStuData.MarkHalfDayAbsent = AttendanceData.MarkHalfDayAbsent;
  //      editStuData.MarkFullDayAbsent = AttendanceData.MarkFullDayAbsent;
  //      editStuData.Day = AttendanceData.Day;
  //      editStuData.Others = AttendanceData.Others;
  //      editStuData.CreatedDate = AttendanceData.CreatedDate??string.Empty;
  //      editStuData.CreatedBy = AttendanceData.CreatedBy;
  //      _lumen.Entry(editStuData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
  //      var isRecoredSaved = _lumen.SaveChanges();
  //      if (isRecoredSaved == 1)
  //      {

  //        res.Data = isRecoredSaved;
  //        res.Msg = "Record save successfully.";
  //        res.ResponseCode = "201";
  //      }
  //      else
  //      {
  //        res.Data = isRecoredSaved;
  //        res.Msg = "Something went worng .Please try again later.";
  //        res.ResponseCode = "400";
  //      }
  //    }

  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  //public async Task<IApiResponse> DeleteStudenAttendance(IStudentAttendance AttendanceData)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {
  //    TblStudentAttendance? editStuData = await _lumen.TblStudentAttendances.FindAsync(AttendanceData.AttendanceId);
  //    if (editStuData == null)
  //    {
  //      res.Msg = "Student attendance data not found.Please provide valid student data.";
  //      res.ResponseCode = "201";
  //    }
  //    else
  //    {

  //       _lumen.Remove(editStuData);
  //      int affectedRows=_lumen.SaveChanges();

  //      if (affectedRows > 0 )
  //      {

  //        res.Data = affectedRows;
  //        res.Msg = "Record Deleted successfully.";
  //        res.ResponseCode = "201";
  //      }
  //      else
  //      {
  //        res.Data = affectedRows;
  //        res.Msg = "Something went worng .Please try again later.";
  //        res.ResponseCode = "400";
  //      }
  //    }

  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  public async Task<IApiResponse> StudentAtendance(string? Role, string? stafId)
  {
    IApiResponse response = new ApiResponse();
    StaffClassStudentsSection staffClassStudentsSection = new StaffClassStudentsSection();

    // Check if the Role is not null or empty and is "Staff" and stafId is not null or empty
    if (!string.IsNullOrEmpty(Role) && Role == "Staff" && !string.IsNullOrEmpty(stafId))
    {
      // Use async method for database calls
      staffClassStudentsSection.staff = await _lumen.StafsDetails.ToListAsync();  // Ensure StafsDetails is an EF DbSet

      // Get the classes assigned to the staff
      var classlist = await _lumen.Subjects
          .Where(x => x.StaffId == Convert.ToInt32(stafId) && x.ClassTeacher == true)
          .ToListAsync();  // Ensure Subjects is an EF DbSet

      // Extract classIds
      var classIds = classlist.Select(x => x.ClassId).Distinct().FirstOrDefault();

      // Fetch class details based on classIds
      //var classDetails = await _lumen.TblCommonDataListItems
      //    .Where(item => classIds.Contains(item.DataListItemId ?? 0))
      //    .Select(item => new TblDataListItem
      //    {
      //      DataListItemId = item.DataListItemId ?? 0,
      //      DataListItemName = item.DataListItemName
      //    })
      //    .ToListAsync();

      var classDetails = await _lumen.TblCommonDataListItems
          .Where(item => item.DataListItemId == classIds)
          .Select(item => new TblDataListItem
          {
            DataListItemId = item.DataListItemId ?? 0,
            DataListItemName = item.DataListItemName
          })
          .ToListAsync();


      // Assign the class details to the section
      staffClassStudentsSection.classsname = classDetails;

      // Get sections related to the classes
      var query = (from s in classlist
                   join c in _lumen.TblCommonDataListItems on s.ClassId equals c.DataListItemId
                   where s.StaffId == Convert.ToInt32(stafId) && s.ClassTeacher == true
                   select c).Distinct();

      staffClassStudentsSection.Section = query
          .Select(x => new TblDataListItem
          {
            DataListItemId = x.DataListItemId ?? 0,
            DataListItemName = x.DataListItemName
          })
          .ToList();

      // Get all batches
      staffClassStudentsSection.tblBatch = await _lumen.TblBatches.ToListAsync();  // Ensure TblBatches is an EF DbSet

      // Set the response data
      response.Data = staffClassStudentsSection;
      response.Msg = "Data fetched successfully";
      response.ResponseCode = "200";
    }
    // Admin or Administrator role handling
    else if (!string.IsNullOrEmpty(Role) && (Role == "Admin" || Role == "Administrator"))
    {
      // Fetch all staff, class details, sections, and batches for Admin/Administrator
      staffClassStudentsSection.staff = await _lumen.StafsDetails.ToListAsync();
      staffClassStudentsSection.classsname = new List<TblDataListItem>();
      staffClassStudentsSection.Section = new List<TblDataListItem>();
      staffClassStudentsSection.tblBatch = await _lumen.TblBatches.ToListAsync();

      // Set the response data for Admin
      response.Data = staffClassStudentsSection;
      response.Msg = "Data fetched successfully";
      response.ResponseCode = "200";
    }
    else
    {
      // Handle case when Role or stafId is invalid
      response.Msg = "Invalid role or staff ID";
      response.ResponseCode = "400";
    }

    return response;
  }
  public async Task<IApiResponse> SaveOrUpdateStaffAttendanceAsync(List<TblStaffAttendance> models)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      foreach (var model in models)
      {
        // 1️⃣ Validate Attendence_Date format
        if (!DateTime.TryParseExact(model.Attendence_Date, "dd/MM/yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
          res.Msg = $"Invalid attendance date format for StaffId {model.Staff_Id}. Use dd/MM/yyyy.";
          res.ResponseCode = "400";
          return res;
        }

        // 2️⃣ Check only one flag is true
        int trueCount = new[]
        {
                model.Mark_FullDayAbsent,
                model.Mark_HalfDayAbsent,
                model.Mark_Other,
                model.Mark_CL,
                model.Mark_ML,
                model.Mark_L,
                model.Mark_FullDayPresent
            }.Count(x => !string.IsNullOrEmpty(x) && x.Equals("true", StringComparison.OrdinalIgnoreCase));

        if (trueCount > 1)
        {
          res.Msg = $"Only one attendance mark can be true for StaffId {model.Staff_Id}.";
          res.ResponseCode = "400";
          return res;
        }

        // 3️⃣ Check duplicate
        var existingRecord = await _lumen.TblStaffAttendances
            .FirstOrDefaultAsync(x => x.Staff_Id == model.Staff_Id
                                   && x.Attendence_Date == model.Attendence_Date
                                   && x.CurrentYear == model.CurrentYear
                                   && x.IsDeleted == false);

        if (existingRecord != null)
        {
          // Update existing
          existingRecord.Mark_FullDayAbsent = model.Mark_FullDayAbsent;
          existingRecord.Mark_HalfDayAbsent = model.Mark_HalfDayAbsent;
          existingRecord.Mark_Other = model.Mark_Other;
          existingRecord.Mark_CL = model.Mark_CL;
          existingRecord.Mark_ML = model.Mark_ML;
          existingRecord.Mark_L = model.Mark_L;
          existingRecord.Mark_FullDayPresent = model.Mark_FullDayPresent;

          existingRecord.ModifiedDate = DateTime.Now;
          existingRecord.UserId = model.UserId;
          existingRecord.IP = model.IP;

          _lumen.Entry(existingRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        else
        {
          // Insert new
          model.AddedDate = DateTime.Now;
          model.ModifiedDate = DateTime.Now;
          model.IsDeleted = false;

          await _lumen.TblStaffAttendances.AddAsync(model);
        }
      }

      // Save all changes at once
      await _lumen.SaveChangesAsync();

      res.Msg = "Attendance records processed successfully.";
      res.ResponseCode = "200";
      res.Data = models;
    }
    catch (Exception ex)
    {
      res.Msg = "Error: " + ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }
  public async Task<IApiResponse> GetAttendanceByDateRangeAsync(string startDateStr, string endDateStr, int? staffId = null)
  {
    IApiResponse res = new ApiResponse();
    IFormatProvider culture = CultureInfo.InvariantCulture;

    try
    {
      // 1️⃣ Parse input dates
      if (!DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime startDate))
      {
        res.Msg = "Invalid start date format. Use dd/MM/yyyy";
        res.ResponseCode = "400";
        return res;
      }

      if (!DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime endDate))
      {
        res.Msg = "Invalid end date format. Use dd/MM/yyyy";
        res.ResponseCode = "400";
        return res;
      }

      // 2️⃣ Query attendance (non-deleted)
      var query = _lumen.TblStaffAttendances.AsQueryable();
      query = query.Where(x => x.IsDeleted == false);

      if (staffId.HasValue)
        query = query.Where(x => x.Staff_Id == staffId.Value);

      // 3️⃣ Fetch data from DB first
      var records = await query.ToListAsync();

      // 4️⃣ In-memory filter for valid Attendence_Date
      var result = records
          .Where(x => !string.IsNullOrEmpty(x.Attendence_Date) &&
                      DateTime.TryParseExact(x.Attendence_Date, "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime dt) &&
                      dt >= startDate && dt <= endDate)
          .OrderBy(x => x.Staff_Id)
          .ThenBy(x => x.Attendence_Date)
          .ToList();

      // 5️⃣ Return response
      res.Data = result;
      res.Msg = "Attendance fetched successfully";
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = "Error: " + ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }

  public class StaffClassStudentsSection
  {
    public List<StafsDetail>? staff { get; set; }
    public List<TblDataListItem>? classsname {get;set;}
    public List<TblDataListItem>? Section { get; set; }
    public List<TblBatch>? tblBatch { get; set; }
  
  }

}




