using LumenApi.Web.Services;
using Microsoft.AspNetCore.Mvc;
using LumenApi.Web.Helpers;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using static System.Collections.Specialized.BitVector32;

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class DashboardController(IDashBoardService dashBoard) : Controller
{
  private readonly IDashBoardService _dashBoard = dashBoard;
  IApiResponse? res;
  [HttpPost("AdminDashBoard")]
  public async Task<IApiResponse> AdminDashBoartd(string RoleName)
  {
     res = new ApiResponse();
    try
    {
      res = await _dashBoard.DashBoard(RoleName);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("StudentsDashBoardAttendanceDetails")]
  public IActionResult StudentsDashBoardAttendanceDetails(DateTime startDate, DateTime endDate, int classId, int sectionId, int fromYear, int toYear, int studentId = 0)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      res = _dashBoard.GetStudentAttendanceReport(startDate, endDate, classId, sectionId, fromYear, toYear, studentId);
      return Ok(new
      {
        Message = res.Msg,
        ResponseCode = res.ResponseCode,
        Data = res.Data // Assuming res.Data holds the actual attendance report
      });
    }
    catch (Exception ex)
    {
      //res.Msg = ex.Message;
      //res.ResponseCode = "500";
      return StatusCode(500, new
      {
        Message = ex.Message,
        ResponseCode = "500"
      });
    }
   // return res;
  }
  [HttpGet("MobileAppVersion")]
  public IActionResult GetMobileAppVersions()
  {
    IApiResponse res = new ApiResponse();
    try
    {

      res = _dashBoard.GetMobileApp();
      return Ok(new
      {
        Message = res.Msg,
        ResponseCode = res.ResponseCode,
        Data = res.Data // Assuming res.Data holds the actual attendance report
      });
    }
    catch (Exception ex)
    {
      //res.Msg = ex.Message;
      //res.ResponseCode = "500";
      return StatusCode(500, new
      {
        Message = ex.Message,
        ResponseCode = "500"
      });
    }
    // return res;
  }
  [HttpPost("SaveMobileAppVersion")]
  public IActionResult SaveMobileAppVersion(MobileAppVersions mobileAppVersions)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      res = _dashBoard.SaveMobileMobileApp(mobileAppVersions);
      return Ok(new
      {
        Message = res.Msg,
        ResponseCode = res.ResponseCode,
        Data = res.Data // Assuming res.Data holds the actual attendance report
      });
    }
    catch (Exception ex)
    {
      //res.Msg = ex.Message;
      //res.ResponseCode = "500";
      return StatusCode(500, new
      {
        Message = ex.Message,
        ResponseCode = "500"
      });
    }
    // return res;
  }
  [HttpPost("TimeTable")]
  public async Task<IApiResponse> TimeTable(int classId=0, int SectionId=0, int StaffId=0)
  {
    res = new ApiResponse();
    try
    {
      res = await _dashBoard.GetTimeTable( classId,  SectionId,  StaffId);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
}
