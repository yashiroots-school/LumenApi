using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
namespace LumenApi.Web.Services;


public class AuthService(
  Lumen090923Context lumen) : IAuthService
{

  private readonly Lumen090923Context _lumen = lumen;

  public async Task<IApiResponse> GetUserCreditials(IEmployeeLoginInterface EmpLogingData)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var query = from userManagement in _lumen.TblUserManagements
                  join user in _lumen.AspNetUsers on userManagement.UserId equals user.UserId
                  where userManagement.UserName == EmpLogingData.Email && userManagement.Password == EmpLogingData.Password
                  select new UserLoginData
                  {
                    UserId = userManagement.UserId,
                    UserName = userManagement.UserName!,
                    Email = userManagement.Email!,
                    UserGuId = user.Id,
                    DepartmentId = user.DepartmentId,
                    RoldeName = user.Roles.FirstOrDefault()!.Name,
                    UserRoleId = user.Roles.FirstOrDefault()!.Id,

                  };
      UserLoginData? userLoginData = await query.FirstOrDefaultAsync();
      if (userLoginData == null)
      {
        res.Data = userLoginData!;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        if (userLoginData.RoldeName == "Student")
        {
          userLoginData.ApplicationNo = _lumen.StudentsRegistrations.Where(x => x.UserId == userLoginData.UserId.ToString()).Select(x => x.ApplicationNumber).FirstOrDefault() ?? string.Empty;
        }
        if (userLoginData.RoldeName == "Staff")
        {
          userLoginData.StaffId = _lumen.StafsDetails.Where(x => x.UserId == userLoginData.UserId.ToString()).Select(x => x.StafId).FirstOrDefault();
        }
        IAuthTokenResponse token = GenerateJsonWebToken(userLoginData);
        if (token.Token_type == null)
        {
          res.Data = null!;
          res.Msg = "Token Generation Falied.";
          res.ResponseCode = "200";
        }
        else
        {
          try
          {
            var existingToken = await _lumen.Tbl_FireBaseToken
                                    .FirstOrDefaultAsync(x => x.UserId == token.UserId);

            if (existingToken != null)
            {
              existingToken.Token = EmpLogingData.FireBaseToken;
              _lumen.Tbl_FireBaseToken.Update(existingToken);
            }
            else
            {
              var newToken = new Tbl_FireBaseToken
              {
                UserId = (int)token.UserId,
                Token = EmpLogingData.FireBaseToken
              };
              await _lumen.Tbl_FireBaseToken.AddAsync(newToken);
            }

            await _lumen.SaveChangesAsync();
          }
          catch (Exception ex)
          {
            // ✅ Log the actual inner exception for debugging
            Console.WriteLine("🔥 Firebase Token Error: " + (ex.InnerException?.Message ?? ex.Message));
          }
          if (token.UserRoleName == "Staff")
          {
            var staffDetails = _lumen.StafsDetails.Where(x => Convert.ToInt64(x.UserId) == token.UserId).ToList();
            string jsonResponse = JsonConvert.SerializeObject(staffDetails);
            res.AdditionalData = jsonResponse; //_lumen.StafsDetails.Where(x => Convert.ToInt64(x.UserId) == token.UserId).ToString()??"";
          }
          if (token.UserRoleName == "Student")
          {
            //var studenmtdetails  = _lumen.StudentsRegistrations.Where(x => Convert.ToInt64(x.UserId) == token.UserId).ToString() ?? "";
            //string jsonResponse = JsonConvert.SerializeObject(studenmtdetails);
            //res.AdditionalData = jsonResponse;
            var staffDetails = _lumen.StudentsRegistrations.Where(x => Convert.ToInt64(x.UserId) == token.UserId).ToList();
            string jsonResponse = JsonConvert.SerializeObject(staffDetails);
            res.AdditionalData = jsonResponse;
            // res.AdditionalData = _lumen.Students.Where(x => Convert.ToInt64(x.UserId) == token.UserId).ToString() ?? "";
          }

          res.Data = token;
          res.Msg = "Token Generated.";
          res.ResponseCode = "200";
        }
      }

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;

  }






  public async Task<UserManagementModel> GetResetLinkData(IRestPassword restPasswordData)
  {
    UserManagementModel userData;
    try
    {
      userData = await _lumen.TblUserManagements.Where(e => e.Email == restPasswordData.Email).Select(e => new UserManagementModel
      {
        Email = e.Email,
        UserName = e.UserName,
        Description = e.Description,
        UserId = e.UserId

      }).SingleAsync();
    }
    catch (Exception)
    {
      userData = null!;
    }
    return userData;
  }
  public Task<bool> IsStudentAccessingOwnData(HttpContext context, string? requestedApplicationNo)
  {
    var user = context.User;

    if (user == null || !(user.Identity?.IsAuthenticated ?? false))
      return Task.FromResult(false);

    var role = user.FindFirst(ClaimTypes.Role)?.Value;
    if (role == null)
      return Task.FromResult(false);

    var tokenApplicationNo = user.FindFirst("ApplicationNo")?.Value;

    if (role != "Student")
      return Task.FromResult(true);

    if (string.IsNullOrWhiteSpace(tokenApplicationNo) || string.IsNullOrWhiteSpace(requestedApplicationNo))
      return Task.FromResult(false);

    return Task.FromResult(tokenApplicationNo == requestedApplicationNo);
  }




  public async Task<ICreatePassword> GeUserDataById(int userId)
  {
    ICreatePassword userData;
    try
    {
      userData = await _lumen.TblUserManagements.Where(e => e.UserId == userId).Select(e => new UserCreatePassword
      {
        Email = e.Email,
        UserName = e.UserName,
        Description = e.Description,
        UserId = Convert.ToString(e.UserId)
      }).SingleAsync();
    }
    catch (Exception)
    {
      userData = null!;
    }
    return userData;
  }

  public async Task<bool> CreateEditNewPassword(ICreatePassword userData)
  {
    bool isCreateEdit;
    try
    {
      // TblUserManagement? fetchedData = await _lumen.TblUserManagements.FindAsync(Convert.ToInt32(userData.UserId));
      TblUserManagement? fetchedData = await _lumen.TblUserManagements.FindAsync((userData.UserName));
      if (fetchedData != null)
      {
        fetchedData.Password = userData.Password;
        await _lumen.SaveChangesAsync();
        isCreateEdit = true;
      }
      else
      {
        isCreateEdit = false;
      }
    }
    catch (Exception)
    {
      isCreateEdit = false;
    }
    return isCreateEdit;
  }

  public IAuthTokenResponse GenerateJsonWebToken(UserLoginData userData)
  {
    IAuthTokenResponse TokenRes = new TokenResponse();
    try
    {
      var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This_is_Lumen_Seceret_Key_For_Jwt"));
      var credintials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
      var exp = DateTime.Now.AddDays(1);

      IEnumerable<Claim> claims;

      if (userData.RoldeName == "Student")
      {
        claims = new[]
        {
                new Claim("Email", userData.Email!),
                new Claim("UserId", userData.UserId.ToString()),
                new Claim("UserName", userData.UserName),
                new Claim("ApplicationNo", userData.ApplicationNo!),
                new Claim("RoleName", userData.RoldeName!),
                new Claim(ClaimTypes.Role, userData.UserRoleId)
            };
      }
      else if(userData.RoldeName == "Administrator")
      {
        claims = new[]
        {
                new Claim("Email", userData.Email!),
                new Claim("UserId", userData.UserId.ToString()),
                new Claim("UserName", userData.UserName),
                new Claim("RoleName", userData.RoldeName!),
                new Claim(ClaimTypes.Role, userData.UserRoleId)
            };
      }
      else
      {
        claims = new[]
       {
                new Claim("Email", userData.Email!),
                new Claim("UserId", userData.UserId.ToString()),
                new Claim("UserName", userData.UserName),
                new Claim("RoleName", userData.RoldeName!),
                new Claim("StaffId", userData.StaffId.ToString()),
                new Claim(ClaimTypes.Role, userData.UserRoleId)
            };
        
      }

      var token = new JwtSecurityToken(
          issuer: "issuer",
          audience: "issuer",
          claims: claims,
          expires: exp,
          signingCredentials: credintials
      );

      TokenRes.Access_token = new JwtSecurityTokenHandler().WriteToken(token);
      TokenRes.Expires_in = 0; //(int)(exp - DateTime.Now).TotalSeconds; // Changed from exp.Millisecond which is wrong
      TokenRes.Token_type = "Bearer";
      TokenRes.UserRoleId = userData.UserRoleId;
      TokenRes.UserRoleName = userData.RoldeName;
      TokenRes.UserId = userData.UserId;
    }
    catch (Exception)
    {
      TokenRes = new TokenResponse();
    }

    return TokenRes;
  }
  public async Task<IApiResponse> UpdateUserCredentialsAsync(string Password,string? username=null,string? email=null,string? userId=null)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      var newPasswordHash = GeneratePasswordHash(Password);
      var newSecurityStamp = Guid.NewGuid().ToString(); 
      var connectionString = _lumen.Database.GetConnectionString();

      using (SqlConnection conn = new SqlConnection(connectionString))
      using (SqlCommand cmd = new SqlCommand("UpdateUserCredentials", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;

        // Parameters add karo
        cmd.Parameters.AddWithValue("@Username", (object?)username ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", (object?)email ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UserId", (object?)userId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NewPassword", Password);
        cmd.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
        cmd.Parameters.AddWithValue("@NewSecurityStamp", newSecurityStamp);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
      }

      res.Msg = "User credentials updated successfully";
      res.ResponseCode = "200";
    }
    catch (SqlException sqlEx)
    {
      res.Msg = "SQL Error: " + sqlEx.Message;
      res.ResponseCode = "500";
    }
    catch (Exception ex)
    {
      res.Msg = "Error: " + ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }
  public string GeneratePasswordHash(string password)
  {
    var hasher = new PasswordHasher<object>();
    string hashedPassword = hasher.HashPassword(new object(), password);
    return hashedPassword;
  }
  public async Task<IApiResponse> UpdateUserEmailAsync(string userId, string email)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      var connectionString = _lumen.Database.GetConnectionString();

      using (SqlConnection conn = new SqlConnection(connectionString))
      using (SqlCommand cmd = new SqlCommand("UpdateUserEmail", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@Email", email);

        await conn.OpenAsync();
        int rows = await cmd.ExecuteNonQueryAsync();

        res.Msg = rows < 0 ? "Email updated successfully" : "No record updated";
        res.ResponseCode = "200";
      }
    }
    catch (SqlException sqlEx)
    {
      res.Msg = "SQL Error: " + sqlEx.Message;
      res.ResponseCode = "500";
    }
    catch (Exception ex)
    {
      res.Msg = "Error: " + ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }

  //public IAuthTokenResponse GenerateJsonWebToken(UserLoginData userData)
  //{
  //  IAuthTokenResponse TokenRes = new TokenResponse();
  //  try
  //  {
  //    var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This_is_Lumen_Seceret_Key_For_Jwt"));
  //    var credintials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
  //    var exp = DateTime.Now.AddDays(1);
  //    IEnumerable<Claim> claims = new[]{
  //      new Claim("Email",userData.Email!),
  //      new Claim("UserId",userData.UserId.ToString()),
  //      new Claim("UserName",userData.UserName!),

  //      new Claim(ClaimTypes.Role,userData.UserRoleId)
  //    };
  //    var token = new JwtSecurityToken(
  //      "issuer",
  //      "issuer",
  //      claims,
  //      expires: exp,
  //      signingCredentials: credintials
  //      );
  //    TokenRes.Access_token = new JwtSecurityTokenHandler().WriteToken(token);
  //    TokenRes.Expires_in = exp.Millisecond;
  //    TokenRes.Token_type = "Bearer";
  //    TokenRes.UserRoleId = userData.UserRoleId;
  //    TokenRes.UserRoleName=userData.RoldeName;
  //    TokenRes.UserId=userData.UserId;
  //  }
  //  catch (Exception)
  //  {
  //    TokenRes = new TokenResponse();
  //  }
  //  return TokenRes;
  //}


}



