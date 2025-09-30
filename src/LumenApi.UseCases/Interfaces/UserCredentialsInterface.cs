using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.Core.Interfaces;
using LumenApi.UseCases.CommonClasses;

namespace LumenApi.UseCases.Interfaces;
internal interface UserCredentialsInterface
{

}

public interface IUserCredentialsService
{
  Task<IApiResponse> GetRolePermission();
  Task<IApiResponse> GetRolePermissionNew();
  Task<IApiResponse> GetManageUsers();
  Task<IApiResponse> CreateUser(UserManagementViewModel tbl_UserManagementViewModel);
  IApiResponse GetStaffDetails(string userId);

  //Task<IApiResponse> GetRoleWiseMenu(long UserId);
}
