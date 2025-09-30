using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Helpers;
using LumenApi.Web.Models.Params;
using LumenApi.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserCredentialsController(IUserCredentialsService userCredentials) : ControllerBase
{
  IUserCredentialsService _userCredentials = userCredentials;
  IApiResponse res = null!;

  [HttpGet("GetManageUsers")]
  public async Task<IApiResponse> GetManageUsers()
  {
    res = new ApiResponse();
    try
    {
      res = await _userCredentials.GetManageUsers();
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpGet("GetRolePermission")]
  public async Task<IApiResponse> GetRolePermission()
  {
    res = new ApiResponse();
    try
    {
      res = await _userCredentials.GetRolePermission();
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetStaffDetails")]
  public IApiResponse GetStaffDetails()
  {
    res = new ApiResponse();
    try
    {
      var userClaims = HttpContext.Items["User"] as IEnumerable<Claim>;

      var userNameClaim = userClaims?.FirstOrDefault(x => x.Type == "UserId");
      var userId = userNameClaim?.Value;

      if (!String.IsNullOrEmpty(userId))
      {
        res = _userCredentials.GetStaffDetails(userId);
      }
      
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpGet("GetRolePermissionNew")]
  public async Task<IApiResponse> GetRolePermissionNew()
  {
    res = new ApiResponse();
    try
    {
      res = await _userCredentials.GetRolePermissionNew();
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("CreateRolePermissionNew")]
  public async Task<IApiResponse> CreateUser(UserManagementViewModel userManagementViewModel)
  {
    res = new ApiResponse();
    try
    {
      res = await _userCredentials.CreateUser(userManagementViewModel);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


}
