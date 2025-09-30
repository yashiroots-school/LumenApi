using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Web.Helpers;
using LumenApi.Web.Models.Params;
using LumenApi.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
{
  private readonly IAttendanceService _attendanceService=attendanceService;

  [HttpPost("ViewStudentAttendance")]
  public async Task<IApiResponse> GetStudentAttendance(StudentAttendanceParams studentAttendance)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res=await _attendanceService.GetStudentAttendance(studentAttendance);

    }
    catch (Exception ex)
    {
      res.Msg= ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("StudentAttendance")]
  public async Task<IApiResponse> SaveStudentAttendance([FromBody] List<AttendanceParams> AttendanceBody)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      if (AttendanceBody == null || !AttendanceBody.Any())
      {
        res.Msg = "No attendance data provided.";
        res.ResponseCode = "400";
        return res;
      }
      else
      {
        var attendanceDataList = AttendanceBody.Cast<IStudentAttendance>().ToList();
        res = await _attendanceService.SaveStudentAttendance(AttendanceBody);
        //res = await _attendanceService.SaveStudenAttendance(AttendanceBody);
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("EditStudentAttendance")]
  public async Task<IApiResponse> EditStudentAttendance([FromBody] List<AttendanceParams> AttendanceBody)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res = await _attendanceService.EditStudenAttendance(AttendanceBody);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("DeleteStudentAttendance")]
  public async Task<IApiResponse> DeleteStudentAttendance([FromBody] List<AttendanceParams> attendanceDataList)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res = await _attendanceService.DeleteStudenAttendance(attendanceDataList);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("SaveOrUpdateStaffAttendance")]
  public async Task<IApiResponse> SaveOrUpdateStaffAttendanceAsync(List<TblStaffAttendance> model)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res = await _attendanceService.SaveOrUpdateStaffAttendanceAsync(model);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("ViewStaffAttendance")]
  public async Task<IApiResponse> ViewStaffAttendance(string startDate, string endDate, int? staffId = null)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res = await _attendanceService.GetAttendanceByDateRangeAsync(startDate, endDate, staffId);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
}
