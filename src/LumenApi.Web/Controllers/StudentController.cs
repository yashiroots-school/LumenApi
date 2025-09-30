using System.Security.Claims;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentController(IStudentInterfaces  student)  : ControllerBase
{
  private IStudentInterfaces _student = student;
  IApiResponse? res;
  [HttpGet("GetStudentDetails")]
  public async Task<IApiResponse> GetStudentDetials()
  {
    res = new ApiResponse();
    try
    {
      IEnumerable<Claim> user = (IEnumerable<Claim>)HttpContext.Items["User"]!;
      //var userId = user.First(x => x.Type == "UserId");
      var userId = user.First(x => x.Type == "UserId");
      res = await _student.GetStudentDetials(userId.ToString());
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetStudentDetailsByClassSection")]
  public async Task<IApiResponse> GetStudentDetailsByClassSection(int ClassId,int SectionId)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStudentDetialsbyClassSection(ClassId, SectionId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetStudentCurrentYearResult")]
  public async Task<IApiResponse> GetStudentDetailsByClassSection(int StudentId, long BatchId)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStudentResultsByClassSection( StudentId,  BatchId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetStudnetDetails")]
  public async Task<IApiResponse> GetStudnetDetails(string ApplicatioNo)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStudentDetail(ApplicatioNo);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("StudnetsDetails")]
  public async Task<IApiResponse> StudnetsDetails(long BatchId, int classId, int SectionId, long StudentId = 0)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStusdnetDetails( BatchId,  classId,  SectionId,  StudentId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetStudnetSummeryBatchWise")]
  public async Task<IApiResponse> GetStudnetSummeryBatchWise(long  StudentId,int BatchId)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStudentProfileSummeryBatchWise(StudentId, BatchId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetStaffDetails")]
  public async Task<IApiResponse> GetStaffDetails(long StaffId=0)
  {
    res = new ApiResponse();
    try
    {
      res = await _student.GetStaffDetails(StaffId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [Authorize]
  [HttpGet("GetUserDetails")]
  public async Task<IApiResponse> GetUserDetails()
  {
    IApiResponse res = new ApiResponse();

    var identity = HttpContext.User.Identity as ClaimsIdentity;

    if (identity == null || !identity.IsAuthenticated)
    {
      res.Msg = "Unauthorized";
      res.ResponseCode = "401";
      return res;
    }

    var claims = identity.Claims;
    var role = claims.FirstOrDefault(c => c.Type == "RoleName")?.Value;

    if (string.IsNullOrEmpty(role))
    {
      res.Msg = "Role not found in token.";
      res.ResponseCode = "400";
      return res;
    }

    var userId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
    var userName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
    var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

    if (role == "Student")
    {
      var applicationNo = claims.FirstOrDefault(c => c.Type == "ApplicationNo")?.Value;

      if (string.IsNullOrEmpty(applicationNo))
      {
        res.Msg = "Application number not found.";
        res.ResponseCode = "400";
        return res;
      }

      var studentRes = await _student.GetStudentProfile(applicationNo);

      res = studentRes;
      //else
      //{
      //  res.Msg = "Student record not found.";
      //  res.ResponseCode = "404";
      //}
    }
    else if(role=="Administrator")
    {
      res.Msg = "Unknown role.";
      res.ResponseCode = "400";
    }
    else
    {
      var staffId = claims.FirstOrDefault(c => c.Type == "StaffId")?.Value;

      if (string.IsNullOrEmpty(staffId))
      {
        res.Msg = "StaffId not found in token.";
        res.ResponseCode = "400";
        return res;
      }

      res = await _student.GetStaffDetails(Convert.ToInt64(staffId)); // assuming it's a sync method returning IApiResponse
    }
   

    return res;
  }


}
