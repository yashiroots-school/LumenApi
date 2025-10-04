//using System.Data.Entity;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Web.Models.Params;
using LumenApi.Web.Models.StudentView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumenApi.Web.Services;
public interface IDashBoardService
{
  public Task<IApiResponse> DashBoard(string RoleName);
  public IApiResponse GetStudentAttendanceReport(DateTime startDate, DateTime endDate, int classId, int sectionId, int fromYear, int toYear, int studentId = 0);
  public IApiResponse GetMobileApp();
  public IApiResponse SaveMobileMobileApp(MobileAppVersions mobileAppVersions);
  public Task<IApiResponse> GetTimeTable(int classId, int SectionId, int StaffId);
  public Task<IApiResponse> AddCalendarEvent(tbl_CalendarEvents model);
  public Task<IApiResponse> GetCalenderEvents();
  public Task<IApiResponse> DeleteEvent(int id);
  public Task<IApiResponse> GetEventsByDate(string date);
  public Task<IApiResponse> GetNotice();
  public Task<IApiResponse> AddNoticeAsync(tbl_Notice notice);
  Task<IApiResponse> DeleteNotice(int id);

}
public class DashboardServices(Lumen090923Context lumen) : IDashBoardService
{
  private readonly Lumen090923Context _lumen = lumen;
  public async Task<IApiResponse> DashBoard(string RoleName)
  {
    IApiResponse res = new ApiResponse();
    DashBoardCount dashBoardCount = new DashBoardCount();

    if (RoleName == "Administrator")
    {
      dashBoardCount.TotalStudents = await _lumen.Students.CountAsync(x => x.IsApplyforTc == false && x.IsApprove == 217);

      var CurrentBatch = await _lumen.TblBatches
          .Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission == true)
          .FirstOrDefaultAsync();
      if (CurrentBatch != null) {
        dashBoardCount.TC = await _lumen.StudentTcDetails.CountAsync(x => x.BatchId == CurrentBatch.BatchId);

        dashBoardCount.NewAdmission = await _lumen.Students
            .CountAsync(x => x.IsActive == true && x.BatchId == x.CurrentYear && x.CurrentYear == CurrentBatch.BatchId);

        dashBoardCount.TotalStaff = await _lumen.StafsDetails.CountAsync(x => x.IsDeleted == false);

        // Total Fee Collected (using SumAsync for efficiency)
        var Totalfee = await _lumen.TblFeeReceipts
            .Where(x => x.BatchName == CurrentBatch.BatchId.ToString() || x.BatchName == CurrentBatch.BatchName)
            .SumAsync(x => Convert.ToDecimal(x.PaidAmount));
        dashBoardCount.TotalFeeCollect = Totalfee;

        var today = DateTime.Now.Date;

        // Today's Fee Collection (using SumAsync for efficiency)
        var TodayCollection = await _lumen.TblFeeReceipts
            .Where(x => x.AddedDate == today && (x.BatchName == CurrentBatch.BatchId.ToString() || x.BatchName == CurrentBatch.BatchName))
            .SumAsync(x => Convert.ToDecimal(x.PaidAmount));
        dashBoardCount.TodayFeeCollection = TodayCollection;

        var todayString = DateTime.Now.ToString("dd/MM/yyyy");
        dashBoardCount.AbsentStudent = await _lumen.TblStudentAttendances
            .CountAsync(x => x.CreatedDate == todayString && x.MarkFullDayAbsent == "False");

        DateTime todayDate = DateTime.Now;
        int day = todayDate.Day;
        int month = todayDate.Month;
        int year = todayDate.Year;
        dashBoardCount.AbsentTeacher = await _lumen.TblStaffAttendances
            .CountAsync(x => x.Attendence_Date == day.ToString() &&
                             x.Attendence_Month == month.ToString() &&
                             x.Attendence_Year == year.ToString() &&
                             (x.Mark_FullDayPresent != "P" || x.Mark_FullDayAbsent == "True"));



     }
    }

    res.Data = dashBoardCount;
    res.ResponseCode = "200";
    return res;
  }
  //public async Task<IApiResponse> GetStudentAttendanceReport(DateTime startDate, DateTime endDate, int classId, int sectionId, int fromYear, int toYear, int studentId = 0)
  //{
  //  IApiResponse res = new ApiResponse();
  //  var response = new
  //  {
  //    DateRangeAttendance = new List<object>(), // Placeholder for date range attendance
  //    YearlyAttendanceSummary = new List<object>() // Placeholder for yearly summary
  //  };

  //  try
  //  {
  //    // Fetch attendance between the given date range and studentId filter if provided
  //    var dateRangeAttendance = await _lumen.TblStudentAttendances
  //      .Where(x =>
  //              x.CreatedDate != null &&
  //      IsWithinDateRange(x.CreatedDate, startDate, endDate) &&
  //              x.ClassId == classId &&
  //              x.SectionId == sectionId &&
  //              (studentId == 0 || x.StudentRegisterId == studentId)
  //          )
  //        .Select(x => new
  //        {
  //          x.AttendanceId,
  //          x.ClassName,
  //          x.SectionName,
  //          x.StudentRegisterId,
  //          x.StudentName,
  //          x.CreatedDate,
  //          x.MarkFullDayAbsent,
  //          x.MarkHalfDayAbsent,
  //          x.Others
  //        })
  //        .ToListAsync();

  //    // Fetch attendance summary for the year range with studentId filter if provided
  //    var yearlyAttendanceSummary = await _lumen.TblStudentAttendances
  //        .Where(x => x.CreatedDate != null && TryParseAndCheckDate(x.CreatedDate, new DateTime(fromYear, 1, 1), new DateTime(toYear, 12, 31))
  //                    && (studentId == 0 || x.StudentRegisterId == studentId))
  //        .GroupBy(x => new
  //        {
  //          CreatedYear = DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year,  //DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year,
  //          CreatedMonth = DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month
  //        })
  //        .Select(g => new
  //        {
  //          Year = g.Key.CreatedYear,
  //          Month = g.Key.CreatedMonth,
  //          TotalPresent = g.Count(x => x.MarkFullDayAbsent == "true"), // Correct column name here
  //          TotalAbsent = g.Count(x => x.MarkFullDayAbsent == "false"), // Correct column name here
  //          TotalLeave = 0//g.Count(x => x.AttendanceStatus == "L") // Correct column name here
  //        })
  //        .OrderBy(x => x.Year)
  //        .ThenBy(x => x.Month)
  //        .ToListAsync();

  //    // Prepare the response
  //    res.Data = new
  //    {
  //      DateRangeAttendance = dateRangeAttendance,
  //      YearlyAttendanceSummary = yearlyAttendanceSummary
  //    };


  //    res.Msg = "Data fetched successfully.";
  //    res.ResponseCode = "200";
  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  //private static bool IsWithinDateRange(string dateString, DateTime startDate, DateTime endDate)
  //{
  //  if (DateTime.TryParse(dateString, out DateTime createdDate))
  //  {
  //    return createdDate >= startDate && createdDate <= endDate;
  //  }
  //  return false;
  //}
  //private static bool TryParseAndCheckDate(string dateString, DateTime startDate, DateTime endDate)
  //{
  //  if (DateTime.TryParse(dateString, out DateTime createdDate))
  //  {
  //    return createdDate >= startDate && createdDate <= endDate;
  //  }
  //  return false;
  //}
  public IApiResponse GetStudentAttendanceReport(DateTime startDate, DateTime endDate, int classId, int sectionId, int fromYear, int toYear, int studentId = 0)
  {
    IApiResponse res = new ApiResponse();
    var response = new
    {
      DateRangeAttendance = new List<object>(), // Placeholder for date range attendance
      YearlyAttendanceSummary = new List<object>() // Placeholder for yearly summary
    };

    try
    {
      // Fetch attendance between the given date range and studentId filter if provided
      var dateRangeAttendance = _lumen.TblStudentAttendances
          .Where(x => x.ClassId == classId &&
                      x.SectionId == sectionId &&
                      (studentId == 0 || x.StudentRegisterId == studentId))
          .AsEnumerable() // Bring the data into memory
          .Where(x => x.CreatedDate != null &&
                      IsWithinDateRange(DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), startDate, endDate)) // Parse CreatedDate to DateTime
          .Select(x => new
          {
            x.AttendanceId,
            x.ClassName,
            x.SectionName,
            x.StudentRegisterId,
            x.StudentName,
            x.CreatedDate,
            x.MarkFullDayAbsent,
            x.MarkHalfDayAbsent,
            x.Others
          })
          .ToList(); // No await needed for ToList()
      var fromDate = new DateTime(fromYear, 1, 1);
      var toDate = new DateTime(toYear, 12, 31);

      //var yearlyAttendanceSummary = _lumen.TblStudentAttendances
      //    .Where(x => (studentId == 0 || x.StudentRegisterId == studentId))
      //    .AsEnumerable() // Bring the data into memory
      //    .Where(x => TryParseAndCheckDate(x.CreatedDate, new DateTime(fromYear, 1, 1), new DateTime(toYear, 12, 31))) // Apply the filter in memory
      //    .GroupBy(x => new
      //    {
      //      CreatedYear = DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year,
      //      CreatedMonth = DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month
      //    })
      //    .Select(g => new
      //    {
      //      Year = g.Key.CreatedYear,
      //      Month = g.Key.CreatedMonth,
      //      TotalPresent = g.Count(x => x.MarkFullDayAbsent == "true"||x.Others=="true"),
      //      TotalAbsent = g.Count(x => x.MarkFullDayAbsent == "false"),
      //      TotalLeave = 0
      //    })
      //    .OrderBy(x => x.Year)
      //    .ThenBy(x => x.Month)
      //    .ToList(); 
      var yearlyAttendanceSummary = _lumen.TblStudentAttendances
          .Where(x => studentId == 0 || x.StudentRegisterId == studentId)
          .AsEnumerable()
          .Select(x => new
          {
            CreatedDate = DateTime.TryParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate) ? tempDate : (DateTime?)null,
            MarkFullDayAbsent = x.MarkFullDayAbsent ?? "",
            Others = x.Others ?? ""
          })
          .Where(x => x.CreatedDate.HasValue && x.CreatedDate >= fromDate && x.CreatedDate <= toDate)
          .GroupBy(x => new
          {
            Year = x.CreatedDate?.Year ?? 0,  // Handle null values
            Month = x.CreatedDate?.Month ?? 0
          })
          .Select(g => new
          {
            Year = g.Key.Year,
            Month = g.Key.Month,
            TotalPresent = g.Count(x => (x.MarkFullDayAbsent?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false) ||
                                   (x.Others?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false)),

            TotalAbsent = g.Count(x => (x.MarkFullDayAbsent?.Equals("false", StringComparison.OrdinalIgnoreCase) ?? false) && (x.Others?.Equals("false", StringComparison.OrdinalIgnoreCase) ?? false)),

            TotalLeave = 0 // Add logic for leave if needed
          })
          .OrderBy(x => x.Year)
          .ThenBy(x => x.Month)
          .ToList();



      // Prepare the response
      res.Data = new
      {
        DateRangeAttendance = dateRangeAttendance,
        YearlyAttendanceSummary = yearlyAttendanceSummary
      };

      res.Msg = "Data fetched successfully.";
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public IApiResponse GetMobileApp()
  {
    IApiResponse res = new ApiResponse();
    var response = new
    {
      MobileApp = new List<object>(),
      
    };

    try
    {
      var MobileApp = _lumen.MobileAppVersion.OrderByDescending(x=>x.Date).AsEnumerable() 
          .Select(x => new
          {
            x.Id,
            x.VersionName,
           x.Date
          })
          .ToList();
      res.Data = new
      {
        MobileApp = MobileApp,
       
      };

      res.Msg = "Data fetched successfully.";
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public IApiResponse SaveMobileMobileApp(MobileAppVersions mobileAppVersions)
  {
    IApiResponse res = new ApiResponse();
   

    try
    {
      mobileAppVersions.Date = DateTime.Now;
      _lumen.MobileAppVersion.Add(mobileAppVersions);
      _lumen.SaveChanges();
      res.Msg = "Data Saved successfully.";
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  public async Task<IApiResponse> GetTimeTable(int classId=0, int SectionId=0,int StaffId=0)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var conn = _lumen.Database.GetDbConnection();
      await conn.OpenAsync();

      string sql = @"Exec usp_GetTimeTableDetails " + classId + "," + SectionId + "," + StaffId + " ";

      using (var command = conn.CreateCommand())
      {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        var reader = await command.ExecuteReaderAsync();

        var result = new List<TimeTableView>();
        while (await reader.ReadAsync())
        {
          var record = new TimeTableView
          {

            DayName = reader["DayName"]?.ToString() ?? string.Empty,
            Class = reader["ClassName"]?.ToString() ?? string.Empty,
            Section = reader["SectionName"]?.ToString() ?? string.Empty,
            StaffName = reader["StaffName"]?.ToString() ?? string.Empty,
            LoadPer = reader["TotalStaffLoadPerDay"]?.ToString() ?? string.Empty,
            Subject = reader["Subject_Name"]?.ToString() ?? string.Empty,
            Period= reader["PeriodNumber"]?.ToString() ?? string.Empty,
          };


          result.Add(record);
        }

        if (result.Any())
        {
          res.Data = result;
          res.Msg = "Record fetched successfully.";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = result;
          res.Msg = "Record Not Found.";
          res.ResponseCode = "404";
        }

        await conn.CloseAsync();
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;

  }
  private static bool IsWithinDateRange(DateTime createdDate, DateTime startDate, DateTime endDate)
  {
    return createdDate >= startDate && createdDate <= endDate;
  }
  public async Task<IApiResponse> GetCalenderEvents()
  {
    var res = new ApiResponse();
    try
    {
      if (_lumen.tbl_CalendarEvents is IQueryable<tbl_CalendarEvents> queryable)
      {
        if (queryable.Provider is IDbAsyncQueryProvider)
          res.Data = await queryable.ToListAsync();
        else
          res.Data = queryable.ToList();
      }
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> GetNotice()
  {
    var res = new ApiResponse();
    try
    {
      if (_lumen.tbl_Notice is IQueryable<tbl_Notice> queryable)
      {
        if (queryable.Provider is IDbAsyncQueryProvider)
          res.Data = await queryable.ToListAsync();
        else
          res.Data = queryable.OrderByDescending(x => x.ID).ToList();
      }
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> GetEventsByDate(string date)
  {
    var res = new ApiResponse();
    try
    {
      if (!DateTime.TryParse(date, out DateTime selectedDate))
      {
        res.ResponseCode = "400";
        res.Msg = "Invalid date";
        return res;
      }

      var eventsFromDb = await _lumen.tbl_CalendarEvents
          .Where(x => x.EventDate.Date == selectedDate.Date)
          .OrderBy(x => x.EventTime)
          .ToListAsync();

      res.Data = eventsFromDb.Select(x => new
      {
        EventTime = TimeSpanToString(x.EventTime),
        EventName = x.EventName
      }).ToList();

      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = ex.Message;
    }
    return res;
  }
  public async Task<IApiResponse> DeleteEvent(int id)
  {
    var res = new ApiResponse();
    try
    {
      var data = await _lumen.tbl_CalendarEvents.FindAsync(id);
      if (data != null)
      {
        _lumen.tbl_CalendarEvents.Remove(data);
        await _lumen.SaveChangesAsync();
        res.ResponseCode = "200";
        res.Msg = "Event deleted successfully";
      }
      else
      {
        res.ResponseCode = "404";
        res.Msg = "Event not found";
      }
    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = ex.Message;
    }
    return res;
  }
  public async Task<IApiResponse> AddCalendarEvent(tbl_CalendarEvents model)
  {
    var res = new ApiResponse();
    try
    {
      //if (ModelState.IsValid)
      {
        _lumen.tbl_CalendarEvents.Add(model);
        await _lumen.SaveChangesAsync();

        res.Data = model;
        res.ResponseCode = "200";
        res.Msg = "Event added successfully";
      }
      //else
      //{
      //  res.ResponseCode = "400";
      //  res.Msg = "Invalid model";
      //}
    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = ex.Message;
    }
    return res;
  }
  public async Task<IApiResponse> AddNoticeAsync(tbl_Notice notice)
  {
    var res = new ApiResponse();
    if (notice == null)
    {
      res.Msg = "Request body cannot be null.";
      res.ResponseCode = "500";
      return res;
    }
    try
    {
      _lumen.tbl_Notice.Add(notice);
      await _lumen.SaveChangesAsync();

      var newNotice = new
      {
        notice.ID,
        notice.NoticeName,
        NoticeDate = notice.NoticeDate.ToString("dd-MM-yyyy"),
        NoticeTime = notice.CurrentDate.ToString("hh:mm tt")
      };
      res.Data = newNotice;
      res.Msg = "Notice Saved Successfully!";
    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = ex.Message;
    }
    return res;
  }
  public async Task<IApiResponse> DeleteNotice(int id)
  {
    var res = new ApiResponse();
    try
    {
      var notice = await _lumen.tbl_Notice.FirstOrDefaultAsync(x => x.ID == id);
      if (notice == null)
      {
        res.Msg = "Record not found";
        res.ResponseCode = "200";
        //return NotFound(new { success = false, message = "Record not found" });
      }
      else
      {
        _lumen.tbl_Notice.Remove(notice);
        await _lumen.SaveChangesAsync();
        res.Msg = "Notice Deleted Successfully !";
      }
    }
    catch (Exception ex)
    {
      res.Msg=ex.Message;
      res.ResponseCode = "500";
    }
    return res;
    //return Ok(new { success = true });
  }
  private string TimeSpanToString(TimeSpan time)
  {
    DateTime dt = DateTime.Today.Add(time);
    return dt.ToString("hh:mm tt"); 
  }
  private static bool TryParseAndCheckDate(string dateString, DateTime startDate, DateTime endDate)
  {
    // Parsing the date string to DateTime using the dd/MM/yyyy format
    if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime createdDate))
    {
      return createdDate >= startDate && createdDate <= endDate;
    }
    return false;
  }


  public class DashBoardCount
  {
    public int TotalStudents { get; set; }=0;
    public int NewAdmission { get; set; }=0;
    public int AbsentStudent { get; set; } = 0;
    public int TotalStaff { get; set; }= 0;
    public int NewJoinTeacher { get; set; } = 0;
    public int AbsentTeacher { get; set; } = 0;
    public decimal TotalFeeCollect { get; set; }= 0;
    public decimal TodayFeeCollection { get; set; } = 0;
    public int TC { get; set; } = 0;
  }
}
