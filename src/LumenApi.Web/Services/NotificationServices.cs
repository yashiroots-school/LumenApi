
using System.Data;
using System.Linq;
using FirebaseAdmin.Messaging;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using static LumenApi.Web.Controllers.NotificationController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;

namespace LumenApi.Web.Services;

public class NotificationServices(Lumen090923Context lumen): INotificationServices
{
  private readonly Lumen090923Context _lumen = lumen;

  //public async Task<IApiResponse> SendPushToMultipleUsers([FromBody] PushNotificationMultiRequest request)
  //{
  //  IApiResponse res = new ApiResponse();

  //  if (request.UserIds == null || !request.UserIds.Any())
  //  {
  //    res.Msg = "UserIds list is empty";
  //    res.ResponseCode = "404";
  //    return res;
  //  }

  //  try
  //  {
  //    // Convert userIds from string to int
  //    var userIdsInt = request.UserIds.Select(int.Parse).ToList();

  //    // Get tokens for given userIds
  //    var tokensWithUserIds = await _lumen.Tbl_FireBaseToken
  //        .Where(t => userIdsInt.Contains(t.UserId) && !string.IsNullOrEmpty(t.Token))
  //        .Select(t => new { t.UserId, t.Token })
  //        .ToListAsync();

  //    if (!tokensWithUserIds.Any())
  //    {
  //      res.Msg = "No device tokens found for the selected users.";
  //      res.ResponseCode = "404";
  //      return res;
  //    }

  //    foreach (var item in tokensWithUserIds)
  //    {
  //      var log = new Tbl_NotificationLog
  //      {
  //        UserId = item.UserId,
  //        Token = item.Token??"",
  //        Title = request.Title,
  //        Body = request.Body,
  //        SentAt = DateTime.UtcNow
  //      };

  //      try
  //      {
  //        // Send push notification
  //        await SendPushNotificationToToken(item.Token??"", request.Title, request.Body);
  //        log.Status = "Success";
  //      }
  //      catch (Exception ex)
  //      {
  //        log.Status = "Failed";
  //        log.ErrorMessage = ex.Message;
  //      }

  //      _lumen.Tbl_NotificationLog.Add(log);
  //    }

  //    // Save all logs
  //    await _lumen.SaveChangesAsync();

  //    res.Msg = $"Push notifications sent to {tokensWithUserIds.Count} devices.";
  //    res.ResponseCode = "200";
  //    return res;
  //  }
  //  catch (Exception ex)
  //  {

  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //    return res;
  //  }
  //}
  public async Task<IApiResponse> SendPushToMultipleUsers([FromBody] PushNotificationMultiRequest request)
  {
    IApiResponse res = new ApiResponse();

    if (request.UserIds == null || !request.UserIds.Any())
    {
      res.Msg = "UserIds list is empty";
      res.ResponseCode = "404";
      return res;
    }

    try
    {
      // Convert userIds from string to int
      var userIdsInt = request.UserIds.Select(int.Parse).ToList();

      // Create DataTable for TVP
      var tvp = new DataTable();
      tvp.Columns.Add("Id", typeof(int));
      foreach (var id in userIdsInt)
      {
        tvp.Rows.Add(id);
      }

      // List to store tokens
      var tokensWithUserIds = new List<FirebaseTokenDto>();

      // ADO.NET connection from EF Core
      using (var conn = _lumen.Database.GetDbConnection())
      {
        await conn.OpenAsync();

        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = "dbo.GetFirebaseTokensForUsers";
          cmd.CommandType = System.Data.CommandType.StoredProcedure;

          var param = new SqlParameter("@UserIds", SqlDbType.Structured)
          {
            TypeName = "dbo.IntList", // your TVP type in SQL
            Value = tvp
          };

          cmd.Parameters.Add(param);

          using (var reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              tokensWithUserIds.Add(new FirebaseTokenDto
              {
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                Token = reader.GetString(reader.GetOrdinal("Token"))
              });
            }
          }
        }

        if (!tokensWithUserIds.Any())
        {
          res.Msg = "No device tokens found for the selected users.";
          res.ResponseCode = "404";
          return res;
        }

        // Send notifications and log
        foreach (var item in tokensWithUserIds)
        {
          var log = new Tbl_NotificationLog
          {
            UserId = item.UserId,
            Token = item.Token ?? "",
            Title = request.Title,
            Body = request.Body,
            SentAt = DateTime.UtcNow
          };

          try
          {
            // Call your push notification method
            await SendPushNotificationToToken(item.Token ?? "", request.Title, request.Body);
            log.Status = "Success";
          }
          catch (Exception ex)
          {
            log.Status = "Failed";
            log.ErrorMessage = ex.Message;
          }

          _lumen.Tbl_NotificationLog.Add(log);
        }

        await _lumen.SaveChangesAsync();

        res.Msg = $"Push notifications sent to {tokensWithUserIds.Count} devices.";
        res.ResponseCode = "200";
        return res;
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      return res;
    }
  }



  private async Task SendPushNotificationToToken(string token, string title, string body)
  {
    var message = new Message
    {
      Token = token,
      Notification = new Notification
      {
        Title = title,
        Body = body
      }
    };

    await FirebaseMessaging.DefaultInstance.SendAsync(message);
  }
  private async Task<int> GetUserIdByTokenAsync(string token)
  {
    return await _lumen.Tbl_FireBaseToken
        .Where(t => t.Token == token)
        .Select(t => (int)t.UserId)
        .FirstOrDefaultAsync();
  }
  //public async Task<IApiResponse> SaveMessage(MessageRequest request)
  //{
  //  IApiResponse res = new ApiResponse();
  //  if (request.StudentIds == null || !request.StudentIds.Any())
  //  {
  //    res.Msg = "No students selected";
  //    res.ResponseCode = "404";
  //    return res;
  //    //return BadRequest("No students selected.");

  //  }
  //  else
  //  {
  //    byte[]? fileBytes = null;
  //    string? fileName = null;

  //    if (request.Attachment != null)
  //    {
  //      using (var ms = new MemoryStream())
  //      {
  //        await request.Attachment.CopyToAsync(ms);
  //        fileBytes = ms.ToArray();
  //        fileName = request.Attachment.FileName;
  //      }
  //    }
  //    var _connectionString = _lumen.Database.GetConnectionString();
  //    using (var con = new SqlConnection(_connectionString))
  //    {
  //      await con.OpenAsync();

  //      foreach (var studentId in request.StudentIds)
  //      {
  //        using (var cmd = new SqlCommand("USP_StudentMessages_Save", con))
  //        {
  //          cmd.CommandType = CommandType.StoredProcedure;

  //          // For insert -> pass null; For update -> pass existing MessageId
  //          cmd.Parameters.AddWithValue("@MessageId", DBNull.Value);
  //          cmd.Parameters.AddWithValue("@StudentId", studentId);
  //          cmd.Parameters.AddWithValue("@Title", request.Title ?? (object)DBNull.Value);
  //          cmd.Parameters.AddWithValue("@Body", request.Body ?? (object)DBNull.Value);
  //          cmd.Parameters.AddWithValue("@Attachment", (object?)fileBytes ?? DBNull.Value);
  //          cmd.Parameters.AddWithValue("@AttachmentName", (object?)fileName ?? DBNull.Value);
  //          cmd.Parameters.AddWithValue("@UserId", request.UserId ?? "System");

  //          var result = await cmd.ExecuteScalarAsync();
  //          long messageId = result != null ? Convert.ToInt64(result) : 0;
  //        }
  //      }
  //    }
  //    res.Msg = "Message(s) saved successfully.";
  //    res.ResponseCode = "200";
  //    return res;
  //    // return Ok(new { success = true, message = "Message(s) saved successfully." });
  //  }
  //}
  public async Task<IApiResponse> SaveMessage(MessageRequest request)
  {
    IApiResponse res = new ApiResponse();

    if (request.StudentIds == null || !request.StudentIds.Any())
    {
      res.Msg = "No students selected";
      res.ResponseCode = "404";
      return res;
    }

    byte[]? fileBytes = null;
    string? fileName = null;

    // Convert Base64 string to byte[] if present
    if (!string.IsNullOrEmpty(request.AttachmentBase64))
    {
      try
      {
        fileBytes = Convert.FromBase64String(request.AttachmentBase64);
        fileName = request.AttachmentFileName;
      }
      catch (FormatException)
      {
        res.Msg = "Attachment is not a valid Base64 string.";
        res.ResponseCode = "400";
        return res;
      }
    }

    var _connectionString = _lumen.Database.GetConnectionString();
    using (var con = new SqlConnection(_connectionString))
    {
      await con.OpenAsync();

      foreach (var studentId in request.StudentIds)
      {
        using (var cmd = new SqlCommand("USP_StudentMessages_Save", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@MessageId", DBNull.Value);
          cmd.Parameters.AddWithValue("@StudentId", studentId);
          cmd.Parameters.AddWithValue("@Title", request.Title ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@Body", request.Body ?? (object)DBNull.Value);
          cmd.Parameters.Add("@Attachment", SqlDbType.VarBinary).Value = (object?)fileBytes ?? DBNull.Value;
          cmd.Parameters.Add("@AttachmentName", SqlDbType.NVarChar, 255).Value = (object?)fileName ?? DBNull.Value;
          //cmd.Parameters.AddWithValue("@Attachment", (object?)fileBytes ?? DBNull.Value);
          //cmd.Parameters.AddWithValue("@AttachmentName", (object?)fileName ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@UserId", request.UserId ?? "System");

          var result = await cmd.ExecuteScalarAsync();
          long messageId = result != null ? Convert.ToInt64(result) : 0;
        }
      }
    }

    res.Msg = "Message(s) saved successfully.";
    res.ResponseCode = "200";
    return res;
  }

  public async Task<IApiResponse> SaveMessageAsync(long senderId,long receiverId,string messageText,byte[]? attachment = null,string? attachmentName = null)
  {
    var res = new ApiResponse();
    var _connectionString = _lumen.Database.GetConnectionString();

    try
    {
      using (SqlConnection conn = new SqlConnection(_connectionString))
      using (SqlCommand cmd = new SqlCommand("USP_ChatMessages_Save", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@SenderId", senderId);
        cmd.Parameters.AddWithValue("@ReceiverId", receiverId);
        cmd.Parameters.AddWithValue("@MessageText", messageText);

        if (attachment != null)
          cmd.Parameters.AddWithValue("@Attachment", attachment);
        else
          cmd.Parameters.AddWithValue("@Attachment", DBNull.Value);

        if (!string.IsNullOrEmpty(attachmentName))
          cmd.Parameters.AddWithValue("@AttachmentName", attachmentName);
        else
          cmd.Parameters.AddWithValue("@AttachmentName", DBNull.Value);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
      }

      res.ResponseCode = "200";
      res.Msg = "Message saved successfully.";
    
    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = "Error saving message: " + ex.Message;
      
    }

    return res;
  }
  public List<ChatMessage> GetChatHistory(long user1, long user2)
  {
    var messages = new List<ChatMessage>();
    var _connectionString = _lumen.Database.GetConnectionString();

    using (SqlConnection conn = new SqlConnection(_connectionString))
    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.UDF_GetChatHistory(@User1, @User2) ORDER BY CreatedDate", conn))
    {
      cmd.CommandType = CommandType.Text;
      cmd.Parameters.AddWithValue("@User1", user1);
      cmd.Parameters.AddWithValue("@User2", user2);

      conn.Open();
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        while (reader.Read())
        {
          var msg = new ChatMessage
          {
            MessageId = reader.GetInt64(reader.GetOrdinal("MessageId")),
            SenderId = reader.GetInt64(reader.GetOrdinal("SenderId")),
            ReceiverId = reader.GetInt64(reader.GetOrdinal("ReceiverId")),
            MessageText = reader["MessageText"].ToString()??string.Empty,
            AttachmentName = reader["AttachmentName"].ToString()??string.Empty,
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            Attachment = reader.IsDBNull(reader.GetOrdinal("Attachment"))? null: (byte[])reader["Attachment"]
          };
          messages.Add(msg);
        }
      }
    }

    return messages;
  }
  public List<StudentMessage> GetMessageHistory(long userId)
  {
    var messages = new List<StudentMessage>();
    var _connectionString = _lumen.Database.GetConnectionString();

    using (SqlConnection conn = new SqlConnection(_connectionString))
    using (SqlCommand cmd = new SqlCommand("exec GetMessageByUserId @UserId", conn))
    {
      cmd.CommandType = CommandType.Text;
      cmd.Parameters.AddWithValue("@UserId", userId);

      conn.Open();
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        while (reader.Read())
        {
          var msg = new StudentMessage
          {
            MessageId = reader.GetInt64(reader.GetOrdinal("MessageId")),
            StudentId = reader.GetInt64(reader.GetOrdinal("StudentId")),
            Title = reader["Title"].ToString() ?? string.Empty,
            Body = reader["Body"].ToString() ?? string.Empty,
            //Attachment = reader["Attachment"] == DBNull.Value ? null : reader["Attachment"].ToString(),
            Attachment = reader.IsDBNull(reader.GetOrdinal("Attachment"))? null: Convert.ToBase64String((byte[])reader["Attachment"]),
            AttachmentName = reader["AttachmentName"].ToString() ?? string.Empty,
            CreatedBy = reader["CreatedBy"].ToString() ?? string.Empty,
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : reader["ModifiedBy"].ToString(),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
            Type = reader["Type"].ToString() ?? string.Empty
          };
          messages.Add(msg);
        }
      }
    }

    return messages;
  }
  public async Task<IApiResponse> GetStudentsListforMessage(int? ClassId=null, int? sectionId = null, string? gender=null)
  {
    var res = new ApiResponse();
    var _connectionString = _lumen.Database.GetConnectionString();
    var studentList = new List<StudentDetailsWithUserName>();
    try
    {
      using (SqlConnection conn = new SqlConnection(_connectionString))
      using (SqlCommand cmd = new SqlCommand("USP_GetStudentsByClassSectionwithusername", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Class", ClassId);
        cmd.Parameters.AddWithValue("@Section", sectionId);
        cmd.Parameters.AddWithValue("@gender", gender);
        conn.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (await reader.ReadAsync())
          {
            var msg = new StudentDetailsWithUserName
            {
              UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
              UserName = reader["UserName"].ToString() ?? string.Empty,
              Email = reader["Email"].ToString() ?? string.Empty,
              Description = reader["Description"].ToString() ?? string.Empty,
              ApplicationNumber = reader["ApplicationNumber"].ToString() ?? string.Empty,
              StudentId = reader.IsDBNull(reader.GetOrdinal("StudentId"))
                                ? null
                                : reader.GetInt32(reader.GetOrdinal("StudentId")),
              Name = reader["Name"].ToString() ?? string.Empty,
              Class = reader["Class"].ToString() ?? string.Empty,
              Section = reader["Section"].ToString() ?? string.Empty,
              Gender = reader["Gender"].ToString() ?? string.Empty,
              DOB = reader["DOB"].ToString() ?? string.Empty,
              Token = reader["Token"].ToString() ?? string.Empty,
            };
            studentList.Add(msg);
          }
        }
      }
      res.Data = studentList;
      res.ResponseCode = "200";
      res.Msg = "Message saved successfully.";

    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = "Error saving message: " + ex.Message;

    }

    return res;
  }
  public async Task<IApiResponse> GetAllUSerWithToken()
  {
    var res = new ApiResponse();
    var _connectionString = _lumen.Database.GetConnectionString();
    var studentList = new List<AllUserNamewithToken>();
    try
    {
      using (SqlConnection conn = new SqlConnection(_connectionString))
      using (SqlCommand cmd = new SqlCommand("GetAllUserDetails", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (await reader.ReadAsync())
          {
            var msg = new AllUserNamewithToken
            {
              UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
              UserName = reader["UserName"].ToString() ?? string.Empty,
              Email = reader["Email"].ToString() ?? string.Empty,
              Description = reader["Description"].ToString() ?? string.Empty,
              Token = reader["Token"].ToString() ?? string.Empty,
              StudentId = reader.IsDBNull(reader.GetOrdinal("StudentId"))
                                ? null
                                : reader.GetInt32(reader.GetOrdinal("StudentId")),
              StaffId = reader.IsDBNull(reader.GetOrdinal("StaffId"))
                                ? null: reader.GetInt32(reader.GetOrdinal("StaffId")),
              Name = reader["Name"].ToString() ?? string.Empty,
              Class = reader["Class"].ToString() ?? string.Empty,
              Section = reader["Section"].ToString() ?? string.Empty,
             

            };
            studentList.Add(msg);
          }
        }
      }
      res.Data = studentList;
      res.ResponseCode = "200";
      res.Msg = "Message saved successfully.";

    }
    catch (Exception ex)
    {
      res.ResponseCode = "500";
      res.Msg = "Error saving message: " + ex.Message;

    }

    return res;
  }
}
public class FirebaseTokenDto
{
  public int UserId { get; set; }
  public string Token { get; set; } = string.Empty;
}
