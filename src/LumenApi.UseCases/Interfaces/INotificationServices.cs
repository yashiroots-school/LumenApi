using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.Core.Interfaces;
using LumenApi.UseCases.CommonClasses;
//using LumenApi.Web.ViewModels;
//using LumenApi.Web.Models.Exam.ReportCardModel;
namespace LumenApi.UseCases.Interfaces;
public interface INotificationServices
{
  Task<IApiResponse> SendPushToMultipleUsers(PushNotificationMultiRequest request);
  Task<IApiResponse> SaveMessage(MessageRequest request);
  Task<IApiResponse> SaveMessageAsync(long senderId, long receiverId, string messageText, byte[]? attachment = null, string? attachmentName = null);
  List<ChatMessage> GetChatHistory(long user1, long user2);
  List<StudentMessage> GetMessageHistory(long userId);
  Task<IApiResponse> GetStudentsListforMessage(int? ClassId = null, int? sectionId = null, string? gender = null);
  Task<IApiResponse> GetAllUSerWithToken();
}
