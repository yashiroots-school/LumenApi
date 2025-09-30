using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LumenApi.UseCases.CommonClasses;

public class PaymentViewModels
{

  public string Name { get; set; } = string.Empty;
  public string StudentId { get; set; } = string.Empty;
  public string Class { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public string DOB { get; set; } = string.Empty;
  public string POB { get; set; } = string.Empty;
  public string Nationality { get; set; } = string.Empty;
  public string MotherTongue { get; set; } = string.Empty;
  public string Religion { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public string Caste { get; set; } = string.Empty;
  public string BloodGroup { get; set; } = string.Empty;
  public bool IsApproved { get; set; }
  public string Batch { get; set; } = string.Empty;
  public string RoleNumber { get; set; } = string.Empty;
  public string TCBal { get; set; } = string.Empty;
  public string FeeHeadings { get; set; } = string.Empty;
  public string Feeheadingamt { get; set; } = string.Empty;
  public string ApplicationNumber { get; set; } = string.Empty;
  public float ConcessionAmt { get; set; }
  public float Concession { get; set; }
  public string DueFee { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Section { get; set; } = string.Empty;

}

//public class PaymentResultModels
//{
//  public string StudentName { get; set; } = string.Empty;
//  public string FatherName { get; set; } = string.Empty;
//  public string Contact { get; set; } = string.Empty;
//  public string Class { get; set; } = string.Empty;
//  public string Category { get; set; } = string.Empty;
//  public string RoleNumber { get; set; } = string.Empty;
//  public string TCBal { get; set; } = string.Empty;
//  //public string Batch { get; set; }
//  public string studentid { get; set; } = string.Empty;
//  public string FeeHeadings { get; set; } = string.Empty;
//  public string Feeheadingamt { get; set; } = string.Empty;
//  public string ApplicationNumber { get; set; } = string.Empty;
//  public float Concession { get; set; }
//  public float ConcessionAmt { get; set; }
//  public string Key { get; set; } = string.Empty;
//  public string Amount { get; set; } = string.Empty;
//  public string Currency { get; set; } = string.Empty;
//  public string OrdedrId { get; set; } = string.Empty;
//  public string Email { get; set; } = string.Empty;
//  public string paymentid { get; set; } = string.Empty;

//  public int classdetails { get; set; }
//  public string AccountType { get; set; } = string.Empty;
//  public string MobileNO { get; set; } = string.Empty;
//  public string SectionName { get; set; } = string.Empty;

//}

public class PaymentTransactionDetails
{
  public string TransactionStatus { get; set; } = string.Empty;
  public string TransactionError { get; set; } = string.Empty;
  public string ReferenceNo { get; set; } = string.Empty;
  public string TrackId { get; set; } = string.Empty;
  public string PaymentId { get; set; } = string.Empty;
}


public class InputPayment
{
  public string OrderId { get; set; } = string.Empty;
  public string Mid { get; set; } = string.Empty;
  public string Enckey { get; set; } = string.Empty;
  public string MeTransReqType { get; set; } = string.Empty;
  public string TrnAmt { get; set; } = string.Empty;
  public string ResponseUrl { get; set; } = string.Empty;
  public string TrnRemarks { get; set; } = string.Empty;
  public string TrnCurrency { get; set; } = string.Empty;
  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
  public string AddField9 { get; set; } = string.Empty;
  public string AddField10 { get; set; } = string.Empty;
}

public class InputPaymentResult
{
  public string OrderId { get; set; } = string.Empty;
  public string Mid { get; set; } = string.Empty;
  public string Enckey { get; set; } = string.Empty;
  public string MeTransReqType { get; set; } = string.Empty;
  public string TrnAmt { get; set; } = string.Empty;
  public string ResponseUrl { get; set; } = string.Empty;
  public string TrnRemarks { get; set; } = string.Empty;
  public string TrnCurrency { get; set; } = string.Empty;

  public string AddField9 { get; set; } = string.Empty;
  public string AuthZCode { get; set; } = string.Empty;
  public string PgMeTrnRefNo { get; set; } = string.Empty;
  public string TrnReqDate { get; set; } = string.Empty;
  public string Rrn { get; set; } = string.Empty;
  public string StatusCode { get; set; } = string.Empty;
  public string StatusDesc { get; set; } = string.Empty;
  public string ResponseCode { get; set; } = string.Empty;
  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
  public string AddField10 { get; set; } = string.Empty;
}

public class ResultPayment
{
  public string StatusDesc { get; set; } = string.Empty;
  public string ReqMsg { get; set; } = string.Empty;

}

public class PaymentStatus
{
  public string OrderId { get; set; } = string.Empty;
  public string Mid { get; set; } = string.Empty;
  public string Enckey { get; set; } = string.Empty;

  public string pgTrnRefNo { get; set; } = string.Empty;

  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
}
public class ResultPaymentstatus
{
  public string StatusCode { get; set; } = string.Empty;
  public string ReqMsg { get; set; } = string.Empty;

}

public class CancelPayment
{
  public string OrderId { get; set; } = string.Empty;
  public string Mid { get; set; } = string.Empty;
  public string Enckey { get; set; } = string.Empty;

  public string pgTrnRefNo { get; set; } = string.Empty;

  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
}

public class ResultCancelPayment
{
  public string OrderId { get; set; } = string.Empty;

  public string pgTrnRefNo { get; set; } = string.Empty;

  public string StatusCode { get; set; } = string.Empty;

  public string StatusDescription { get; set; } = string.Empty;

  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
}

public class RefundPayment
{
  public string OrderId { get; set; } = string.Empty;
  public string Mid { get; set; } = string.Empty;
  public string Enckey { get; set; } = string.Empty;
  public string Amount { get; set; } = string.Empty;
  public string pgTrnRefNo { get; set; } = string.Empty;

  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
}

public class ResultRefundPayment
{
  public string OrderId { get; set; } = string.Empty;

  public string pgTrnRefNo { get; set; } = string.Empty;

  public string StatusCode { get; set; } = string.Empty;

  public string StatusDescription { get; set; } = string.Empty;

  public string AddField1 { get; set; } = string.Empty;
  public string AddField2 { get; set; } = string.Empty;
  public string AddField3 { get; set; } = string.Empty;
  public string AddField4 { get; set; } = string.Empty;
  public string AddField5 { get; set; } = string.Empty;
  public string AddField6 { get; set; } = string.Empty;
  public string AddField7 { get; set; } = string.Empty;
  public string AddField8 { get; set; } = string.Empty;
}

public class PaymentResultSummary
{
  public string StudentId { get; set; } = string.Empty;
  public string StudentName { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public string Course { get; set; } = string.Empty;
  public string PaymentStatus { get; set; } = string.Empty;
  public string TransactionID { get; set; } = string.Empty;
  public string PaymentMode { get; set; } = string.Empty;
  public string TransactionAmount { get; set; } = string.Empty;
  public string TransactionDate { get; set; } = string.Empty;
  public string ReferenceNumber { get; set; } = string.Empty;
  public string Last_Name { get; set; } = string.Empty;
}

