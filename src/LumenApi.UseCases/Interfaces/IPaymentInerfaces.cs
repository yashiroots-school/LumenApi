using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.UseCases.CommonClasses;
using LumenApi.Core.Interfaces;
using LumenApi.Web.Models;
using LumenApi.Web.Models.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LumenApi.UseCases.Interfaces;
//public interface IPaymentInerfaces
//{
//  Task<PaymentResultModels> PreapareInput(PaymentInputModel objPaymentInputModel);
//  //Task <Payverify> PaymentApiCall(PaymentResultModels paymentResultModels);
//  Dictionary<string, string> GetKeySecretGateWay(int classdetails, string FeeHeadings, string PaymentGatewayName);
//}
internal interface IPaymentInerfaces { }
public interface IPaymentServies
{
  public Task<PaymentResultModels> PreapareInput(PaymentInputModel objPaymentInputModel);
  public Task<FeeDetails> GetStudentFeesAsync(string applicationNumber);
  public Task<string> FetchPayment(string paymentId);
  public Task<string> CapturePayment(string paymentId, int amount);
  public  Task<string> PaymentSuccessResult(PaymentResultModels paymentViewModels, PaymentTransactionId paymentTransaction, string paymentgatewayName);
  public Task<IApiResponse> MarchentDetail();
  Task<IApiResponse> GetStudentDetail(string ApplicationNo);
  // Payverify.Payverify PaymentApiCall(PaymentResultModels paymentResultModels);
  //Dictionary<string, string> GetKeySecretGateWay(int classdetails, string FeeHeadings, string PaymentGatewayName);
}

