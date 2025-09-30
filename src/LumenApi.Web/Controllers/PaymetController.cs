using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
//using LumenApi.Web.Models.PaymentModels;
//using LumenApi.Web.Models.PaymentModels.BusinessModel;
using LumenApi.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using PaymentInputModel = LumenApi.Web.Models.PaymentModels.PaymentInputModel;
using LumenApi.UseCases.Interfaces;
using LumenApi.Infrastructure.Data.LumenContext;
using LumenApi.Web.Models.PaymentModels.NTTPaymentGateway;
using Microsoft.AspNetCore.Mvc.Razor;
using LumenApi.Web.Models.PaymentModels;
using PaymentInputModel = LumenApi.UseCases.CommonClasses.PaymentInputModel;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static LumenApi.Web.Services.PaymentService;
using MailKit.Search;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
//using Razorpay.Api;

namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymetController : ControllerBase
{
  private readonly IPaymentServies _paymentInterface;//= Interface;
  IApiResponse? res;
  private readonly EncrypDecrpt _encrypDecrpt;

  //private IStudentInterfaces _student;
  // public  PaymetController(IStudentInterfaces student)
  public PaymetController(IPaymentServies Interface, EncrypDecrpt encrypDecrpt)
  {
    _paymentInterface = Interface;
    _encrypDecrpt = encrypDecrpt;
    //_student=student;
  }
  [HttpPost("PreapareInput")]
  public async Task<IApiResponse> PreapareInput( PaymentInputModel objPaymentInputModel)
  {
    res = new ApiResponse();
    try
    {


      var PaymentResultModelsResponse = await _paymentInterface.PreapareInput(objPaymentInputModel);
      res.Data = PaymentResultModelsResponse;
      //var PaymentResultModelsResponse = await _student.PreapareInput(objPaymentInputModel);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost]
  [Route("CapturePaymentResponse")]

  public async Task<IActionResult> PaymentSuccess(PaymentResultModels paymentViewModels, string paymentgatewayName)
  {
    if (paymentViewModels == null || string.IsNullOrEmpty(paymentgatewayName))
    {
      return BadRequest("Invalid payment result data.");
    }

    var paymentTransaction = new PaymentTransactionId
    {
      // Paymentid = paymentViewModels.paymentid ?? string.Empty,
      Paymentid = paymentViewModels.paymentid?? paymentViewModels.atomTokenId?? string.Empty,
      Orderid = paymentViewModels.TrackID ?? string.Empty
    };

    string result = await _paymentInterface.PaymentSuccessResult(paymentViewModels, paymentTransaction, paymentgatewayName);

    return Ok(new { message = result });
  }





  public class CapturePaymentRequest
  {
    public string Encdata { get; set; } = string.Empty;
  }


  [HttpGet("get-student-fees")]
  public async Task<IActionResult> GetStudentFeesAsync(string applicationNumber)
  {
   
    try
    {
      
      var feeDetailsResult = await _paymentInterface.GetStudentFeesAsync(applicationNumber);

      return Ok(new
      {
        FeeDetails = feeDetailsResult.FeeDetailsDto,
        TotalAmount = feeDetailsResult.TotalAmount
      });
    }
    catch (Exception ex)
    {
      
      return StatusCode(500, new
      {
        message = "An error occurred while processing the request",
        error = ex.Message
      });
    }
  }
  [HttpGet("MarchentDetails")]
  public async Task<IApiResponse> MarchentDetails()
  {
    res = new ApiResponse();
    try
    {
      res = await _paymentInterface.MarchentDetail();
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetStudnetDetails")]
  public async Task<IApiResponse> GetStudnetDetails(string ApplicatioNo)
  {
    res = new ApiResponse();
    try
    {
      res = await _paymentInterface.GetStudentDetail(ApplicatioNo);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


}
