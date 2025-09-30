using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.UseCases.CommonClasses;
internal class AttendanceClass
{

}

public class UserManagementViewModel
{
  public int UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string UserRole { get; set; } = string.Empty;
  public string ApplicationNumber { get; set; } = string.Empty;
  public string StudentName { get; set; } = string.Empty;
}

