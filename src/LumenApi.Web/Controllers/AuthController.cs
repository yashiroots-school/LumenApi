using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Infrastructure.Data;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IEmailSender emailSender) : ControllerBase
{
  public IEmailSender _emailSender = emailSender;
  private readonly IAuthService _authService = authService;
  IApiResponse res = null!;

  [HttpPost("CreateEmployeeLogin")]
  public async Task<IApiResponse> CreateEmployeeLogin([FromBody] EmployeeLoginParams EmpLoginData)
  {
    res = new ApiResponse();
    try
    {
      res = await _authService.GetUserCreditials(EmpLoginData);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpPost("RestPasswordLink")]
  public async Task<IApiResponse> RestPasswordLink([FromBody] ResetPassWordParams ResetData)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var currentContxt = HttpContext.Request.Scheme.ToString() + "://" + HttpContext.Request.Host.ToString();

      UserManagementModel userData = (UserManagementModel)await _authService.GetResetLinkData(ResetData);
      if (userData != null)
      {
        var resetLink = currentContxt + "/ForgetPassword/CreateNewPassword?UserId=" + userData.UserId;
        await _emailSender.SendEmailAsync(userData.Email!, "pranaybansod59@gmail.com", "test", resetLink);
        res.Data = resetLink;
        res.Data = resetLink;
        res.Msg = "Link generated successfully.";
        res.ResponseCode = "201";
      }
      else
      {
        res.Data = null!;
        res.ResponseCode = "201";
        res.Msg = "Please provide valid emailId.";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.Data = ex.Data;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpPost("CreateNewPassword")]
  public async Task<IApiResponse> CreateNewPassword([FromBody] UserCreatePassword userData)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var x = HttpContext.Request;
      bool isEdited = await _authService.CreateEditNewPassword(userData);
      if (isEdited)
      {
        res.Data = isEdited;
        res.Msg = "Password successfully created.";
        res.ResponseCode = "201";
      }
      else
      {
        res.Data = isEdited;
        res.Msg = "Failure in creating new password.";
        res.ResponseCode = "200";
      }
    }
    catch (Exception ex)
    {
      res.Data = ex.Data;
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }
  [HttpPost("UpdateUserCredentials")]
  public async Task<IApiResponse> UpdateUserCredentialsAsync(string Password, string? username = null, string? email = null, string? userId = null)
  {
    IApiResponse res = new ApiResponse();
    try
    {
    res=  await _authService.UpdateUserCredentialsAsync(Password, username, email, userId);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.Data = ex.Data;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("UpdateUserEmail")]
  public async Task<IApiResponse> UpdateUserEmailAsync(string userId, string email)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      res = await _authService.UpdateUserEmailAsync(userId, email);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.Data = ex.Data;
      res.ResponseCode = "500";
    }
    return res;
  }

}
