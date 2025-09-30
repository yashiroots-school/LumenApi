using System.ComponentModel.DataAnnotations;
using LumenApi.Core.Interfaces;
using LumenApi.UseCases.CommonClasses;

namespace LumenApi.UseCases.Interfaces;
internal interface AuthInterface
{
  string UserName { get; }

  string Email { get; }
  string Password { get; }
  string Description { get; }
}
public interface IAuthTokenResponse
{
  public string Token_type { get; set; }
  public string Access_token { get; set; }
  public long Expires_in { get; set; }
  public long UserId { get; set; }
  public string UserRoleName { get; set; }
  public string UserRoleId { get; set; }
}


public interface IEmployeeLoginInterface
{
  string Email { get; set; }

  string Password { get; set; }
  string FireBaseToken { get; set; }
}


public interface IRestPassword
{
  string? Email { get; set; }
}


public interface ICreatePassword
{
  //string? Email { get; set; }
  //string? UserId { get; set; }
  //string? Password { get; set; }
  //string? ConfirmPassword { get; set; }


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
public interface IAuthService
{
  Task<IApiResponse> GetUserCreditials(IEmployeeLoginInterface EmpLogingData);
  Task<UserManagementModel> GetResetLinkData(IRestPassword restPasswordData);
  Task<ICreatePassword> GeUserDataById(int userId);
  Task<bool> CreateEditNewPassword(ICreatePassword userData);
  IAuthTokenResponse GenerateJsonWebToken(UserLoginData userData);
  Task<IApiResponse> UpdateUserCredentialsAsync(string Password, string? username = null, string? email = null, string? userId = null);
  Task<IApiResponse> UpdateUserEmailAsync(string userId, string email);
 
}

