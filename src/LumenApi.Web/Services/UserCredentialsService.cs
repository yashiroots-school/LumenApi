using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LumenApi.Web.Services;

public class UserCredentialsService(Lumen090923Context lumen) : IUserCredentialsService
{
  private readonly Lumen090923Context _lumen = lumen;
  private readonly IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();//passwordHasher;
  public async Task<IApiResponse> GetManageUsers()
  {
    IApiResponse res = new ApiResponse();
    try
    {

      var query = await _lumen.AspNetUsers.ToListAsync();

      if (query == null)
      {
        res.Data = query!;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = query;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;

  }

  public async Task<IApiResponse> GetRolePermission()
  {
    IApiResponse res = new ApiResponse();
    try
    {

      var query = await _lumen.AspNetRoles.ToListAsync();

      if (query == null)
      {
        res.Data = query!;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = query;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      res.Data = ex.Data;
    }
    return res;

  }

  public async Task<IApiResponse> GetRolePermissionNew()
  {
    IApiResponse res = new ApiResponse();
    try
    {

      var query = await _lumen.RolePagePermissions.ToListAsync();

      if (query == null)
      {
        res.Data = query!;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = query;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;

  }

  public IApiResponse GetStaffDetails(string userId)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var query = _lumen.StafsDetails
        .Select(s => new
        {
          Staffid = s.StafId,
          UserId = s.UserId,
          Email = s.Email,
          Address = s.Address,
          Name = s.Name,
          Avatar = s.StaffSignatureFile
        }).Where((details) => details.UserId == userId);

      if (query == null)
      {
        res.Data = query!;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = query;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  public async Task<IApiResponse> CreateUser(UserManagementViewModel tbl_UserManagementViewModel)
  {
    IApiResponse res = new ApiResponse();
    TblUserManagement tbl_UserManagement = new TblUserManagement
    {
      Description = tbl_UserManagementViewModel.Description,
      Email = tbl_UserManagementViewModel.Email,
      Password = tbl_UserManagementViewModel.Password,
      UserName = tbl_UserManagementViewModel.UserName
    };
    _lumen.TblUserManagements.Add(tbl_UserManagement);
    _lumen.SaveChanges();
    var user = await _lumen.AspNetUsers.FindAsync(tbl_UserManagement.Email);
    if (user!.UserId > 0)
    {
      if (user == null)
      {
        var fakeUser = new IdentityUser(tbl_UserManagement.UserName);
        var password = _passwordHasher.HashPassword(fakeUser, tbl_UserManagement.Password);
        AspNetUser aspNetUser = new AspNetUser()
        {
          UserName = tbl_UserManagement!.UserName,
          Email = tbl_UserManagement.Email,
          PasswordHash = password,
          UserId = tbl_UserManagement.UserId,
          PhoneNumber = null,
          IsEnable = true

        };
        await _lumen.AspNetUsers.AddAsync(aspNetUser);
        _lumen.SaveChanges();
        var data = await _lumen.AspNetUsers.FindAsync(aspNetUser.Email);
        if (data != null)
        {
          string id = data.Id;
          AspNetRole aspNetRole = new AspNetRole()
          {
            Id = id,
            Name = tbl_UserManagementViewModel.UserRole,
          };
          await _lumen.AspNetRoles.AddAsync(aspNetRole);
          _lumen.SaveChanges();
        }
        res.Data = data!;
        res.Msg = "User created successfully.";
        res.ResponseCode = StatusCodes.Status201Created.ToString();
      }
      else
      {
        res.Data = null!;
        res.Msg = "User already exists..";
        res.ResponseCode = StatusCodes.Status208AlreadyReported.ToString();
      }
    }

    return res;
  }

  //public  async Task<IApiResponse> GetRoleWiseMenu(long UserId)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {

  //    //AspNetUser? currentUser = await _lumen.AspNetUsers.Where(x => x.UserId == UserId).FirstOrDefaultAsync();
  //    //var currentUser = await _lumen.AspNetUsers.ro //FindById(UserId);
  //    //var roles = userManager.FindById(userId).Roles;
  //    //var roleId = roles.FirstOrDefault(r => r.UserId == userId).RoleId;
  //    //var roleName = RoleManager.FindById(roleId);

  //    //res.Data = role!;
  //    //if (query == null)
  //    //{
  //    //  res.Data = query!;
  //    //  res.Msg = "Record Not Found.";
  //    //  res.ResponseCode = "404";
  //    //}
  //    //else
  //    //{
  //    //  res.Data = query;
  //    //  res.Msg = "Record fetched successfully.";
  //    //  res.ResponseCode = "200";
  //    //}

  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;

  //}

  //public async Task<IApiResponse> CreateUser(UserManagementViewModel tbl_zUserManagementViewModel)
  //{
  //  Tbl_UserManagement tbl_UserManagement = new Tbl_UserManagement
  //  {
  //    Description = tbl_UserManagementViewModel.Description,
  //    Email = tbl_UserManagementViewModel.Email,
  //    Password = tbl_UserManagementViewModel.Password,
  //    UserName = tbl_UserManagementViewModel.UserName
  //  };
  //  var usermanagement = _context.Tbl_UserManagement.Add(tbl_UserManagement);
  //  _context.SaveChanges();
  //  if (usermanagement.UserId > 0)
  //  {
  //    Random rnd = new Random();
  //    int rndnumber = rnd.Next(1, 999999);
  //    UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
  //    PasswordHasher PasswordHash = new PasswordHasher();
  //    if (UserManager.FindByEmail(tbl_UserManagementViewModel.Email) == null)
  //    {
  //      ApplicationUser admin = new ApplicationUser
  //      {
  //        UserName = usermanagement.UserName,
  //        Email = usermanagement.Email,
  //        PasswordHash = PasswordHash.HashPassword(usermanagement.Password),
  //        UserId = usermanagement.UserId,
  //        PhoneNumber = null,
  //        IsEnable = true
  //      };

  //      IdentityResult result = UserManager.Create(admin);
  //      if (result.Succeeded == true)
  //      {
  //        var data = UserManager.FindByEmail(admin.Email);
  //        if (data != null)
  //        {
  //          string id = data.Id;
  //          UserManager.AddToRole(id, tbl_UserManagementViewModel.UserRole);
  //        }
  //      }

  //    }
  //  }

  //  return RedirectToAction("ManageUsers");
  //}
}
