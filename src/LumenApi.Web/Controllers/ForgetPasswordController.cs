using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Mvc;

namespace LumenApi.Web.Controllers;
[Route("/[controller]")]
public class ForgetPasswordController(IAuthService authService) : Controller
{
  private readonly IAuthService _authService = authService;

  [HttpGet("CreateNewPassword")]
  public async Task<IActionResult> CreateNewPassword()
  {
    try
    {
      
      string queryParams = HttpContext.Request.Query["UserId"]!;
      if (!string.IsNullOrEmpty(queryParams))
      {
        ICreatePassword userData = await _authService.GeUserDataById(Convert.ToInt32(queryParams));

        if (userData != null)
        {
          return View(userData);
        }
        else { return View(new UserCreatePassword()); }
      }
      else
      {
        return View(new UserCreatePassword());
      }
    }
    catch (Exception)
    {
      return new NotFoundResult();
    }

  }

}
