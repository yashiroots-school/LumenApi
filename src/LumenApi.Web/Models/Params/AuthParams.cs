using System.ComponentModel.DataAnnotations;
using LumenApi.UseCases.Interfaces;
namespace LumenApi.Web.Models.Params;


public class AuthParams
{
}



public class EmployeeLoginParams : IEmployeeLoginInterface
{
  public required string Email { get; set; }


  public required string Password { get; set; }
  public required string FireBaseToken { get; set; }
}



public class ResetPassWordParams:IRestPassword
{
  [Required]
  [EmailAddress]
  public string? Email { get; set;}
 
}

public class UserCreatePassword : ICreatePassword
{
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
  public string? UserId { get; set; }

  [Required]
  public string? Password { get; set; }

  [Required]
  [Compare("Password")]
  public string? ConfirmPassword { get; set; }


  public string? UserName { get; set; }

  public string? Description { get; set; }


}

