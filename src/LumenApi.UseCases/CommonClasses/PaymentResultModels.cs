using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.UseCases.CommonClasses;
public class PaymentResultModels
{

  public string StudentName { get; set; } = string.Empty;
  public string FatherName { get; set; } = string.Empty;
  public string Contact { get; set; } = string.Empty;
  public string Class { get; set; } = string.Empty;
  public string Section { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public string RoleNumber { get; set; } = string.Empty;
  public string TCBal { get; set; } = string.Empty;
  public string studentid { get; set; } = string.Empty;
  public string FeeHeadings { get; set; } = string.Empty;
  public string Feeheadingamt { get; set; } = string.Empty;
  public string ApplicationNumber { get; set; } = string.Empty;
  public float Concession { get; set; }
  public float ConcessionAmt { get; set; }
  public string Key { get; set; } = string.Empty;
  public string Amount { get; set; } = string.Empty;
  public string Currency { get; set; } = string.Empty;
  public string OrdedrId { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string paymentid { get; set; } = string.Empty;
  public int classdetails { get; set; }
  public string AccountType { get; set; } = string.Empty;
  public string MobileNO { get; set; } = string.Empty;

  public string? atomTokenId { get; set; }

  //merchTxnId and orderID
  public string? TrackID { get; set; }
  public string? custEmail { get; set; }
  public string? custMobile { get; set; }
  public string? merchId { get; set; }
  public string? returnurl { get; set; }

}
public class PaymentTransactionId
{
  public string? Paymentid { get; set; }

  public string? Orderid { get; set; }

  public string? Merchant_Key { get; set; }

  public string? Secret_Key { get; set; }

}
