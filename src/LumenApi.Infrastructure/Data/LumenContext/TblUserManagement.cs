using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblUserManagement
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Description { get; set; }
}
public partial class Tbl_FireBaseToken
{
  public int Id { get; set; }

  public int UserId { get; set; }

  public string? Token { get; set; }
}
public class Tbl_NotificationLog
{
  public int Id { get; set; }
  public int UserId { get; set; }  // or string, depending on your UserId type
  public string Token { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public string Status { get; set; } = "Pending"; // Success / Failed
  public DateTime SentAt { get; set; } = DateTime.UtcNow;
  public string? ErrorMessage { get; set; }
}
