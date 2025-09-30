using System.Runtime.Intrinsics.X86;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Helpers;
using LumenApi.Web.Models.Exam.ReportCardModel;
using Microsoft.AspNetCore.Mvc;


namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController(INotificationServices notification) : ControllerBase
{
  private INotificationServices _notification = notification;
  IApiResponse? res;



  [HttpPost("SendPushToMultipleUsers")]
  public async Task<IApiResponse> SendPushToMultipleUsers([FromBody] PushNotificationMultiRequest request)
  {
    res = new ApiResponse();
    try
    {
      res = await _notification.SendPushToMultipleUsers(request);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("SaveorUpdateMessage")]
  public async Task<IApiResponse> SaveorUpdateMessage([FromBody] MessageRequest request)
  {
    res = new ApiResponse();
    try
    {
      res = await _notification.SaveMessage(request);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("SaveorUpdateChatMessage")]
  public async Task<IApiResponse> SaveorUpdateChatMessage(long senderId, long receiverId, string messageText, byte[]? attachment = null, string? attachmentName = null)
  {
    res = new ApiResponse();
    try
    {
      res = await _notification.SaveMessageAsync(senderId, receiverId, messageText, attachment, attachmentName);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("GetChatHistory")]
  public  IApiResponse GetChatHistory(long user1, long user2)
  {
    res = new ApiResponse();
    try
    {
      List<ChatMessage> ch =  _notification.GetChatHistory(user1, user2);
      res.Data = ch;
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("GetMessageHistory")]
  public IApiResponse GetMessageHistory(long userId)
  {
    res = new ApiResponse();
    try
    {
      List<StudentMessage> ch = _notification.GetMessageHistory(userId);
      res.Data = ch;
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("GetStudentsListforMessage")]
  public async Task<IApiResponse> GetStudentsListforMessage(int? ClassId, int? SectioId, string? Gender)
  {
    res = new ApiResponse();
    try
    {
      res = await _notification.GetStudentsListforMessage(ClassId, SectioId, Gender);
      //res.Data = ch;
      //res.ResponseCode = "200";
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetAllUSerWithToken")]
  public async Task<IApiResponse> GetAllUSerWithToken()
  {
    res = new ApiResponse();
    try
    {
      res = await _notification.GetAllUSerWithToken();
      //res.Data = ch;
      //res.ResponseCode = "200";
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
}

