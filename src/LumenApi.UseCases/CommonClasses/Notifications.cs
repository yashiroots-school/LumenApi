using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LumenApi.UseCases.CommonClasses;
public class MessageRequest
{
  public long MessageId { get; set; } = 0;
  public string Title { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public List<long> StudentIds { get; set; } = new List<long>();
  // public IFormFile? Attachment { get; set; }
  public string? AttachmentBase64 { get; set; } // new
  public string? AttachmentFileName { get; set; } // new
  public string UserId { get; set; } = string.Empty; // who is sending message
}
public class ChatMessage
{
  public long MessageId { get; set; }
  public long SenderId { get; set; }
  public long ReceiverId { get; set; }
  public string MessageText { get; set; } = string.Empty;
  public string AttachmentName { get; set; } = string.Empty;
  public DateTime CreatedDate { get; set; }
  public byte[]? Attachment { get; set; }
}
public class StudentDetailsWithUserName
{
  public long UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;

  public string ApplicationNumber { get; set; } = string.Empty;

  public long? StudentId { get; set; }  // nullable if some users don't have a student record

  public string Name { get; set; } = string.Empty;  // concatenated Name + Last_Name

  public string Class { get; set; } = string.Empty;  // cls.DataListItemName
  public string Section { get; set; } = string.Empty; // sec.DataListItemName

  public string Gender { get; set; } = string.Empty;
  public string? DOB { get; set; }  // nullable if DOB can be null
  public string Token { get; set; } = string.Empty;
}
public class AllUserNamewithToken
{
  public string Name { get; set; } = string.Empty;
  public string UserName { get; set; } = string.Empty;
  public int? UserId { get; set; }
  public string Email { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
  public int? StudentId { get; set; }
  public int? StaffId { get; set; }
  public string Class { get; set; } = string.Empty;
  public string Section { get; set; } = string.Empty;
}
public class StudentMessage
{
  public long MessageId { get; set; }
  public long StudentId { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public string? Attachment { get; set; } = string.Empty;
  public string AttachmentName { get; set; } = string.Empty;
  public string CreatedBy { get; set; } = string.Empty;  // nvarchar in DB
  public DateTime CreatedDate { get; set; }
  public string? ModifiedBy { get; set; } = string.Empty;
  public DateTime? ModifiedDate { get; set; } 
  public bool IsDeleted { get; set; }

  // Computed column from query
  public string Type { get; set; } = string.Empty; // "Sent" / "Receive"
}
