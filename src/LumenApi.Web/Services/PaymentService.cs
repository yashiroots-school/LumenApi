using System;
using System.Configuration;
//using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Web;
using Azure;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Infrastructure.Data.LumenContext;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models;
using LumenApi.Web.Models.Params;
using LumenApi.Web.Models.PaymentModels;
using LumenApi.Web.Models.PaymentModels.BusinessModel;
using LumenApi.Web.Models.PaymentModels.NTTPaymentGateway;
using LumenApi.Web.Services;
using LumenApi.Web.ViewModels;
using MailKit.Search;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using static LumenApi.Web.Services.DashboardServices;
using static LumenApi.Web.Services.PaymentService;

//using SchoolManagement.Website.Models;
//using PaymentInputModel = LumenApi.Web.Models.PaymentModels.PaymentInputModel;
//using PaymentInputModel = LumenApi.UseCases.CommonClasses.PaymentInputModel;

namespace LumenApi.Web.Services;

public class PaymentService : IPaymentServies
{
  IConfiguration configuration;
  private readonly Lumen090923Context _lumen;
  private const string BaseUrl = "http://atom.in?";
  private readonly EncrypDecrpt _encrypDecrpt;
  //private readonly mySettingsSection _settings;
  //public class PaymentService(Lumen090923Context lumen,  IConfigurationRoot builder) : IPaymentService
  public PaymentService(Lumen090923Context lumen, IConfiguration builder, EncrypDecrpt encrypDecrpt)
  {
    configuration = builder;
    _lumen = lumen;
    _encrypDecrpt = encrypDecrpt;
    //_settings = settings;
    //_settings = settings;
    // _settings = settings;
  }
  public async Task<PaymentResultModels> PreapareInput(PaymentInputModel objPaymentInputModel)
  {
    IApiResponse res = new ApiResponse();
    string? orderId = string.Empty;
    string? TraceID = string.Empty;
    string?token=string.Empty;
    string Key = string.Empty;
    string Secret = string.Empty;

    //PaymentResultModelsParam paymentResultModels= new PaymentResultModelsParam();
    PaymentResultModels paymentResultModels = new PaymentResultModels();
    await Task.Run(async() =>
    {

      var DataListId = _lumen.TblDataLists.FirstOrDefault(x => x.DataListName != null && x.DataListName.ToLower() == "class")?.DataListId.ToString();
      var ClassList = _lumen.TblDataListItems.Where(e => e.DataListId == DataListId).ToList();
      // paymentResultModels.StudentName = objPaymentInputModel.Name;
      paymentResultModels.Class = objPaymentInputModel.Class;
      paymentResultModels.Category = objPaymentInputModel.Category;
      //paymentResultModels.RoleNumber = objPaymentInputModel.RoleNumber;
      var classdetails = ClassList.FirstOrDefault(x => x.DataListItemName == objPaymentInputModel.Class)?.DataListItemId;
      paymentResultModels.TCBal = objPaymentInputModel.TCBal;
      paymentResultModels.FeeHeadings = objPaymentInputModel.FeeHeadings;
      paymentResultModels.Feeheadingamt = objPaymentInputModel.Feeheadingamt;
      paymentResultModels.Concession = objPaymentInputModel.Concession;
      paymentResultModels.ConcessionAmt = objPaymentInputModel.ConcessionAmt;
      paymentResultModels.Amount = objPaymentInputModel.Feeheadingamt;

      string PaymentAmount = paymentResultModels.Feeheadingamt;
      paymentResultModels.Currency = "INR";
      paymentResultModels.Email = objPaymentInputModel.Email;
      int studentid = objPaymentInputModel.StudentId;
      //var studentDetails = _lumen.Students.Where(x => x.StudentId == studentid).FirstOrDefault();
      var studentDetails = _lumen.Students.Where(x => x.StudentId == studentid).FirstOrDefault();
      
     
      //paymentResultModels.MobileNO = studentDetails?.Mobile ?? "";
     
      paymentResultModels.studentid = objPaymentInputModel.StudentId.ToString();
      paymentResultModels.StudentName = studentDetails?.Name ?? "";
      var famildetails = _lumen.FamilyDetails.FirstOrDefault(x => x.StudentRefId == studentid);
      paymentResultModels.Contact = famildetails == null ? "" : famildetails?.Fmobile ?? "";
      paymentResultModels.FatherName= famildetails == null ? "" : famildetails?.FatherName ?? "";
      paymentResultModels.RoleNumber = studentDetails?.RollNo?.ToString() ?? "";
      #region Payment Bank
      var keyvalue = GetKeySecretGateWay(paymentResultModels.classdetails, paymentResultModels.FeeHeadings, objPaymentInputModel.PaymentGatewayName);

      PaymentTransactionId paymentTransactionId = new PaymentTransactionId();
      paymentTransactionId.Merchant_Key = Key = keyvalue.FirstOrDefault().Key;
      paymentTransactionId.Secret_Key = Secret = keyvalue.FirstOrDefault().Value;
      paymentResultModels.AccountType = keyvalue.LastOrDefault().Value;
      Payverify.Payverify objPayverify = new Payverify.Payverify();
      if (objPaymentInputModel.PaymentGatewayName == "Atomic" || objPaymentInputModel.PaymentGatewayName == "PhonePe" || objPaymentInputModel.PaymentGatewayName == "Paytm"|| objPaymentInputModel.PaymentGatewayName == "AUTUM"|| objPaymentInputModel.PaymentGatewayName == "atom")
      {
        objPaymentInputModel.PaymentGatewayName = "Atomic";
        //EncrypDecrpt.encpassphrase= EncrypDecrpt.encsalt = Secret;
        //EncrypDecrpt.Decpassphrase = EncrypDecrpt.Decsalt = Secret;
        //Payverify.Payverify objPayverify = PaymentApiCall(paymentResultModels);
        //   Payverify.Payverify objPayverify = await PaymentApiCallAsync(paymentResultModels);

        Func<Task> myAction = async () =>
        {
          objPayverify = await PaymentApiCallAsync(paymentResultModels);
        };
        await myAction();
        if (!string.IsNullOrEmpty(objPayverify.atomTokenId))
        {
          var mer = objPayverify.merchId;
          var tok = objPayverify.atomTokenId;
          var mob = objPayverify.custMobile;
          var Email = objPayverify.custEmail;
          TraceID = orderId = objPayverify.TrackID;
          var Orderid = orderId;
          token = tok;
          paymentResultModels.OrdedrId = Orderid!;
          paymentResultModels.merchId = mer;
          paymentResultModels.atomTokenId = tok;
          paymentResultModels.custMobile = mob; 
          paymentResultModels.custEmail = Email;
          var ReturnURL= string.Format(configuration.GetSection("PayUSettings").GetSection("atomtechReturnUrl").Value??string.Empty);
          paymentResultModels.returnurl = ReturnURL;
        }
        else
        {

          //ViewBag.error = "Some error found on payment gateway";
        }

      }

      var paymentgatewayName = objPaymentInputModel.PaymentGatewayName;
      if (studentDetails != null)
      {
        var Section = _lumen.TblDataListItems.Where(e => e.DataListItemId == studentDetails.SectionId).FirstOrDefault();
        paymentResultModels.Section = Section?.DataListItemName ?? "";
      }
      if (res.ResponseCode == "")
      {
        //TempData["StudentDetails"] = paymentResultModels;
        //TempData["MerchantDetails"] = paymentTransactionId;
        tbl_PaymentTransactionDetails objinsert_tbl_PaymentTransactionDetails = new tbl_PaymentTransactionDetails()
        {
          TxnDate = DateTime.Now.ToString("dd/MM/yyyy"),
          Amount = PaymentAmount,
          TransactionId = TraceID!.ToString(),
          Pmntmode = "Online",
          StudentId = Convert.ToInt32(studentid),
          TrackId = orderId!,
          PaymentId = token,
          FeeIds = await GetFeeName(objPaymentInputModel.FeeHeadings),
          FeeAmounts = await GetFormattedFeePlans(objPaymentInputModel.FeeHeadings, studentDetails?.ClassId ?? 0, studentDetails?.BatchId ?? 0, studentDetails?.Medium ?? "ENGLISH")
        };
        _lumen.tbl_PaymentTransactionDetails.Add(objinsert_tbl_PaymentTransactionDetails);
        _lumen.SaveChanges();
      }

    });
    #endregion
    //});
    return paymentResultModels;

  }
  public async Task<string> GetFeeName(string feeIdCsv)
  {
    if (string.IsNullOrWhiteSpace(feeIdCsv))
      return string.Empty;

    try
    {
      // Safe parsing and validation
      var feeIds = feeIdCsv.Split(',')
          .Select(id => id.Trim())
          .Where(id => !string.IsNullOrEmpty(id) && int.TryParse(id, out _))
          .Select(int.Parse)
          .ToList();

      if (!feeIds.Any())
        return string.Empty;

      // Create comma-separated string for SQL IN clause
      var feeIdList = string.Join(",", feeIds);

      // Parameterized SQL query to prevent SQL injection
      var sql = @"
            SELECT FeeName 
            FROM FeeHeadings 
            WHERE FeeId IN (SELECT value FROM STRING_SPLIT(@feeIds, ',')) ";

      var results = await _lumen.FeeHeadings
          .FromSqlRaw(sql, new SqlParameter("@feeIds", feeIdList))
          .Select(f => f.FeeName)
          .ToListAsync();

      return string.Join(",", results);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in GetFeeNameUsingRawSqlAsync: {ex.Message}");
      return string.Empty;
    }
  }

  public async Task<string> GetFormattedFeePlans(string feeIdCsv, int classId, int batchId, string medium)
  {
    if (string.IsNullOrWhiteSpace(feeIdCsv))
      return string.Empty;

    var feeIds = feeIdCsv.Split(',')
        .Select(id => id.Trim())
        .Where(id => !string.IsNullOrEmpty(id) && int.TryParse(id, out _))
        .Select(int.Parse)
        .ToList();

    if (!feeIds.Any())
      return string.Empty;

    try
    {
      // Create parameterized SQL to avoid SQL injection
      var feeIdList = string.Join(",", feeIds);

      var sql = @"
            SELECT FeeId, FeeValue 
            FROM FeePlans 
            WHERE ClassId = @classId 
            AND Batch_Id = @batchId 
            AND Medium = @medium 
            AND FeeId IN (SELECT value FROM STRING_SPLIT(@feeIds, ','))";

      var results = await _lumen.FeePlans
          .FromSqlRaw(sql,
              new SqlParameter("@classId", classId),
              new SqlParameter("@batchId", batchId),
              new SqlParameter("@medium", medium),
              new SqlParameter("@feeIds", feeIdList))
          .Select(f => new { f.FeeId, f.FeeValue })
          .ToListAsync();

      return string.Join(",", results.Select(r => $"{r.FeeId}~{r.FeeValue}"));
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in GetFormattedFeePlansUsingRawSql: {ex.Message}");
      return string.Empty;
    }
  }
  public string GetFormattedFeePlansAsync(string feeIdCsv, int classId, int BatchId, string Medium)
  {
    // Parse comma-separated string into a list of integers
    var feeIds = feeIdCsv
        .Split(',')
        .Select(id => int.Parse(id.Trim()))
        .ToList();

    // using (var dbContext = new _lumen) // Replace with your actual DbContext
    {
      var results = _lumen.FeePlans
    .Where(f => f.ClassId == classId &&
                f.BatchId == BatchId &&
                f.Medium == Medium &&
                feeIds.Contains(f.FeeId))
    .Select(f => new { f.FeeId, f.FeeValue })
    .ToList();

      return string.Join(",", results.Select(r => $"{r.FeeId}~{r.FeeValue}"));
    }
  }
  #region NTT Data Code
  public async Task<Payverify.Payverify> PaymentApiCallAsync(PaymentResultModels paymentResultModels)
  {
    try
    {
      //string directory = @"C:\ATUM";

      //if (!Directory.Exists(directory))
      //  Directory.CreateDirectory(directory);

      //Console.WriteLine($"Log directory: {directory}");

      // Map request data
      Payrequest.Payrequest objre = new Payrequest.Payrequest(_lumen);
      var mapdata = objre.RequestMap(paymentResultModels);
      var json = JsonConvert.SerializeObject(mapdata);

      // Encrypt the data
      //EncrypDecrpt _EncrypDecrpt = new EncrypDecrpt();
      string encryptVal = _encrypDecrpt.Encrypt(json);

      // Validate configuration values
      var authUrl = configuration["PayUSettings:atomtechAuthurl"];
      var merchId = configuration["PayUSettings:atomtechmerchId"];

      if (string.IsNullOrEmpty(authUrl) || string.IsNullOrEmpty(merchId))
        throw new Exception("Required configuration values (atomtechAuthurl or atomtechmerchId) are missing.");

      string apiUrl = string.Format(authUrl, merchId, "&", encryptVal);

      //// Log request data
      //File.AppendAllText(Path.Combine(directory, "RequestData.txt"), json + Environment.NewLine);
      //File.AppendAllText(Path.Combine(directory, "RequestDataURL.txt"), apiUrl + Environment.NewLine);

      // Use HttpClient for async request
      using (var httpClient = new HttpClient())
      {
        try
        {
          // Set security protocols
          ServicePointManager.Expect100Continue = true;
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
          ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
          ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

          var response = await httpClient.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));

          if (response.IsSuccessStatusCode)
          {
            var jsonresponse = await response.Content.ReadAsStringAsync();
            var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");

            var uri = new Uri(configuration["PayUSettings:atomtechResulturl"] + result);
            var query = HttpUtility.ParseQueryString(uri.Query);
            string? encData = query.Get("encData");

            if (!string.IsNullOrEmpty(encData))
            {
              string Decryptval = _encrypDecrpt.decrypt(encData);
              var objPayverify = JsonConvert.DeserializeObject<Payverify.Payverify>(Decryptval);

              if (objPayverify != null)
              {
                objPayverify.merchId = mapdata?.payInstrument?.merchDetails?.merchId;
                objPayverify.TrackID = mapdata?.payInstrument?.merchDetails?.merchTxnId;
                objPayverify.custEmail = mapdata?.payInstrument?.custDetails?.custEmail;
                objPayverify.custMobile = mapdata?.payInstrument?.custDetails?.custMobile;
              }

              return objPayverify ?? new Payverify.Payverify(); // Ensure non-null return
            }
          }
        }
        catch (Exception ex)
        {
          // Log exception
          File.AppendAllText(@"C:\ATUM\ErrorLog.txt", ex.ToString() + Environment.NewLine);
          throw;
        }

        return new Payverify.Payverify(); // Return empty Payverify object in case of failure
      }
    }
    catch (Exception ex)
    {
      // Log the exception and rethrow
      File.AppendAllText(@"C:\ATUM\ErrorLog.txt", ex.ToString() + Environment.NewLine);
      throw;
    }
  }
  public Dictionary<string, string> GetKeySecretGateWay(int? classdetails, string FeeHeadings, string PaymentGatewayName)
  {

    Dictionary<string, string> objdickeysecret = new Dictionary<string, string>();
    string key = "";
    string secret = "";
    TblMerchantclass tblMerchantclass = new TblMerchantclass();
    List<Tbl_CreateMerchantId> tbl_CreateMerchantIds = new List<Tbl_CreateMerchantId>();
    var merchantids = _lumen.Tbl_CreateMerchantId.Select(s => new Tbl_CreateMerchantId
    {
      Merchant_Id = s.Merchant_Id,
      School_Id = s.School_Id,
      Bank_Id = s.Bank_Id,
      Branch_Id = s.Branch_Id,
      MerchantName_Id = s.Merchant_Id,
      MerchantMID = s.MerchantMID,
      MerchantKey = s.MerchantKey,
      AddedDate = s.AddedDate,
      ModifiedDate = s.ModifiedDate,
      CurrentYear = s.CurrentYear,
      IP = s.IP,

    }).ToList();
    var mercantdetailes = _lumen.Tbl_MerchantName.Select(s => new Tbl_MerchantName
    {
      MerchantName_Id = s.MerchantName_Id,
      MerchantName = s.MerchantName,
      School_Id = s.School_Id,
      Bank_Id = s.Bank_Id,
      Branch_Id = s.Branch_Id,
      AddedDate = s.AddedDate,
      ModifiedDate = s.ModifiedDate,
      CurrentYear = s.CurrentYear,
      IP = s.IP
    }).ToList()!;
    //Active Merchant
    var merchantdata = _lumen.Tbl_SchoolSetup.Select(x => new Tbl_SchoolSetup
    {
      Schoolsetup_Id = x.Schoolsetup_Id,
      School_Id = x.School_Id,
      Bank_Id = x.Bank_Id,
      Branch_Id = x.Branch_Id,
      AddedDate = x.AddedDate,

      Merchant_nameId = x.Merchant_nameId,
      Status = x.Status,
      CurrentYear = x.CurrentYear,
      IP = x.IP
    }).FirstOrDefault(x => x.Status == "Active")!;
    //var Branchs = _lumen.TblCreateBranches.ToList()!;
    var Banks = _lumen.Tbl_CreateBank.ToList()!;
    #region new code
    if (merchantdata != null)
    {
      foreach (var item in merchantids)
      {
        if (item.Bank_Id == merchantdata.Bank_Id && item.Branch_Id == merchantdata.Branch_Id && item.School_Id == merchantdata.School_Id)
        {
          tbl_CreateMerchantIds.Add(new Tbl_CreateMerchantId
          {
            MerchantMID = item.MerchantMID,
            MerchantKey = item.MerchantKey,
            MerchantName_Id = item.MerchantName_Id
          });
        }
      }
    }
    if (FeeHeadings == "Transport Fee,Transport Fee(IIndTerm)" || FeeHeadings == "Transport Fee" || FeeHeadings == "Transport Fee(IIndTerm)" || FeeHeadings == "Transport Fee(IIndTerm),Transport Fee")
    {
      foreach (var item in tbl_CreateMerchantIds)
      {
        var merchantname = mercantdetailes.FirstOrDefault(x => x.MerchantName == "Transport")?.MerchantName_Id;
        if (item.MerchantName_Id == merchantname)
        {
          key = item.MerchantMID;
          secret = item.MerchantKey;
          objdickeysecret.Add(key, secret);
        }
      }
      objdickeysecret.Add("Transport", "Transport");
    }
    else if (classdetails != null && classdetails.ToString() == "207" || classdetails.ToString() == "208" || classdetails.ToString() == "209" || classdetails.ToString() == "210")
    {
      //PRE PRIMARY SCHOOL
      foreach (var item in tbl_CreateMerchantIds)
      {
        var merchantname = mercantdetailes.FirstOrDefault(x => x.MerchantName == "Nursery")?.MerchantName_Id;
        if (item.MerchantName_Id == merchantname)
        {
          key = item.MerchantMID;
          secret = item.MerchantKey;
          objdickeysecret.Add(key, secret);
        }
      }
      objdickeysecret.Add("Nursery", "Nursery");
    }
    else
    {
      //CARMEL TERESA SCHOOL
      foreach (var item in tbl_CreateMerchantIds)
      {
        var merchantname = mercantdetailes.FirstOrDefault(x => x.MerchantName == "Primary")?.MerchantName_Id;
        if (item.MerchantName_Id == merchantname)
        {
          key = item.MerchantMID;
          secret = item.MerchantKey;
          objdickeysecret.Add(key, secret);
        }
      }
      objdickeysecret.Add("Primary", "Primary");
    }
    #endregion

    return objdickeysecret;
  }
  public PaymentResultModels MapPaymentResultModels(PaymentViewModels paymentViewModels)
  {
    List<TblDataListItem> classListItems = new List<TblDataListItem>();
    if (paymentViewModels == null)
      throw new ArgumentNullException(nameof(paymentViewModels));

    PaymentResultModels paymentResultModels = new PaymentResultModels();
    var classList = _lumen.TblDataLists
     .FirstOrDefault(x => x.DataListName.ToLower() == "class")?.DataListId;

    if (classList != null)
    {
      classListItems = _lumen.TblDataListItems
          .Where(e => e.DataListId == classList.ToString())
          .ToList();
    }
    var classDetails = classListItems.FirstOrDefault(x => x.DataListItemName == paymentViewModels.Class)?.DataListId;

    paymentResultModels.StudentName = paymentViewModels.Name;
    paymentResultModels.Class = paymentViewModels.Class;
    paymentResultModels.Section = paymentViewModels.Section;
    paymentResultModels.Category = paymentViewModels.Category;
    paymentResultModels.RoleNumber = paymentViewModels.RoleNumber;
    paymentResultModels.TCBal = paymentViewModels.TCBal;
    paymentResultModels.FeeHeadings = paymentViewModels.FeeHeadings;
    paymentResultModels.Feeheadingamt = paymentViewModels.Feeheadingamt;
    paymentResultModels.studentid = paymentViewModels.StudentId;
    paymentResultModels.Concession = paymentViewModels.Concession;
    paymentResultModels.ConcessionAmt = paymentViewModels.ConcessionAmt;
    paymentResultModels.Amount = paymentViewModels.TCBal;
    paymentResultModels.Currency = "INR";
    paymentResultModels.Email = paymentViewModels.Email;

    int studentId = Convert.ToInt32(paymentResultModels.studentid);

    // Uncomment if Mobile number needs to be retrieved
    // paymentResultModels.MobileNO = _context.Students.FirstOrDefault(x => x.StudentId == studentId)?.Mobile;

    return paymentResultModels;
  }
  public async Task<string> FetchPayment(string paymentId)
  {
    using (var client = new HttpClient())
    {
      string? key = _lumen.Tbl_CreateMerchantId.Select(x => x.MerchantKey).FirstOrDefault();
      string? password = _lumen.Tbl_CreateMerchantId.Select(x => x.Password).FirstOrDefault();

      var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{key}:{password}"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

      var response = await client.GetAsync(BaseUrl + $"payments/{paymentId}");

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();  // Return response as JSON string
      }
      else
      {
        throw new Exception("Failed to fetch payment details.");
      }
    }
  }

  public async Task<string> CapturePayment(string paymentId, int amount)
  {
    using (var client = new HttpClient())
    {
      string? key = _lumen.Tbl_CreateMerchantId.Select(x => x.MerchantKey).FirstOrDefault();
      string? password = _lumen.Tbl_CreateMerchantId.Select(x => x.Password).FirstOrDefault();
      var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{key}:{password}"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

      var data = new
      {
        amount = amount  // Amount to be captured (in paise, 100 paise = 1 INR)
      };

      var json = JsonConvert.SerializeObject(data);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      var response = await client.PostAsync(BaseUrl + $"payments/{paymentId}/capture", content);

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();  // Return response as JSON string
      }
      else
      {
        throw new Exception("Failed to capture payment.");
      }
    }
  }
  public async Task<string> PaymentSuccessResult(PaymentResultModels paymentViewModels, PaymentTransactionId paymentTransaction, string paymentgatewayName)
  {
    int stuid = 0;
    InputPaymentResult objPaymentStatus = new InputPaymentResult();
    PaymentTransactionDetails objinsert_tbl_PaymentTransactionDetails;
    if (paymentgatewayName == "Atomic" || paymentgatewayName == "PhonePe" || paymentgatewayName == "Paytm" || paymentgatewayName == "AUTUM" || paymentgatewayName == "atom")
    {
      objinsert_tbl_PaymentTransactionDetails = new PaymentTransactionDetails()
      {
        TransactionStatus = "SUCCESS",
        PaymentId = paymentViewModels.paymentid ?? paymentViewModels.atomTokenId ?? string.Empty,
        TrackId = paymentTransaction?.Orderid ?? string.Empty,
        ReferenceNo = paymentTransaction?.Orderid ?? string.Empty,
        TransactionError = ""
      };
    }
    else
    {
      objinsert_tbl_PaymentTransactionDetails = new PaymentTransactionDetails()
      {
        TransactionStatus = "Captured",
        PaymentId = paymentViewModels.paymentid ?? paymentViewModels.atomTokenId ?? string.Empty,
        TrackId = paymentTransaction?.Orderid ?? string.Empty,
      };
    }
    if (paymentViewModels != null)
    {
      objPaymentStatus.AddField1 = paymentViewModels.StudentName;
      objPaymentStatus.TrnAmt = paymentViewModels.TCBal;
      objPaymentStatus.AddField4 = paymentViewModels.ConcessionAmt.ToString();
      objPaymentStatus.AddField8 = paymentViewModels.FeeHeadings;
      stuid = Convert.ToInt32(paymentViewModels.studentid);

      var orderId = paymentTransaction?.Orderid;
      var paymentDetails = _lumen.tbl_PaymentTransactionDetails.FirstOrDefault(x => x.StudentId == stuid && x.TrackId == orderId);

      //if (paymentDetails != null)
      //{
      //  _lumen.Entry(paymentDetails).CurrentValues.SetValues(objinsert_tbl_PaymentTransactionDetails);
      //  await _lumen.SaveChangesAsync();
      //}
      if (paymentDetails != null)
      {
        paymentDetails.TransactionStatus = objinsert_tbl_PaymentTransactionDetails.TransactionStatus;
        // Record exists, update it
        _lumen.Entry(paymentDetails).CurrentValues.SetValues(objinsert_tbl_PaymentTransactionDetails);
        await _lumen.SaveChangesAsync();
      }
      else
      {
        // Record does not exist, insert new
        tbl_PaymentTransactionDetails objinsert_tbl_PaymentTransactionDetails2 = new tbl_PaymentTransactionDetails()
        {
          TxnDate = DateTime.Now.ToString("dd/MM/yyyy"),
          Amount = paymentViewModels.Amount,
          TransactionId = paymentTransaction?.Paymentid ?? string.Empty,
          Pmntmode = "Online",
          StudentId = Convert.ToInt32(paymentViewModels.studentid),
          //TrackId = orderId!
          PaymentId = paymentTransaction?.Paymentid ?? string.Empty,
          TrackId = paymentTransaction?.Orderid ?? string.Empty,
          ReferenceNo = paymentTransaction?.Orderid ?? string.Empty,
          TransactionError = ""

        };
        await _lumen.SaveChangesAsync();
      }

      //Save changes(either update or insert)
     

      return await genericFeeCalculator(objPaymentStatus, stuid);
     
      }
    else
    {
      return "Invalid payment result data."; ;
    }
  }
  public async Task<string> genericFeeCalculator(InputPaymentResult objPaymentStatus, int stuid)
  {
    try
    {
      var studentdetails = _lumen.Students.FirstOrDefault(x => x.StudentId == stuid);
      //var studentdetails = _lumen.StudentsRegistrations.FirstOrDefault(x => x.StudentRegisterId == stuid);
      int calssid = Convert.ToInt32(studentdetails?.ClassId);
      int Categoryid = Convert.ToInt32(studentdetails?.SectionId);
      //var classDetail = _lumen.TblDataListItems.Where(x => x.DataListId == _lumen.TblDataLists.FirstOrDefault(c => c.DataListName.ToLower() == "Class").DataListId.ToString()).FirstOrDefault(x => x.DataListItemId == calssid);
      //var categoryDetail = _lumen.TblDataListItems.Where(x => x.DataListId == _lumen.TblDataLists.FirstOrDefault(c => c.DataListName.ToLower() == "category").DataListId.ToString()).FirstOrDefault(x => x.DataListItemId == Categoryid);
      var classDataList = _lumen.TblDataLists.FirstOrDefault(c => c.DataListName.ToLower() == "class");
      var classDetail = classDataList != null
          ? _lumen.TblDataListItems
              .Where(x => x.DataListId == classDataList.DataListId.ToString())
              .FirstOrDefault(x => x.DataListItemId == calssid)
          : null;

      var categoryDataList = _lumen.TblDataLists.FirstOrDefault(c => c.DataListName.ToLower() == "category");
      var categoryDetail = categoryDataList != null
          ? _lumen.TblDataListItems
              .Where(x => x.DataListId == categoryDataList.DataListId.ToString())
              .FirstOrDefault(x => x.DataListItemId == Categoryid)
          : null;

      List<FeeHeading> feeHeadingList = _lumen.FeeHeadings.ToList(); //_FeeHeadingsRepository.GetAll().ToList();
      var unicNumber = Guid.NewGuid();
      var Batchdetails = _lumen.TblBatches.Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission==true).Select(x => x.BatchId).ToArray();
      //var feeplans = _lumen.FeePlans.Where(x => x.ClassId == calssid && Batchdetails.Contains(x.BatchId)).ToList();
      var feeplans = _lumen.FeePlans.ToList()
                .Where(x => x.ClassId == calssid && Batchdetails.Contains(x.BatchId))
                .ToList();

      string feeHeadingNames = string.Empty;
      string feeHeadingAmts = string.Empty;
      string feeHeadingIDs = string.Empty;
      string batchname = string.Empty;
      string feetypeid = string.Empty;
      if (objPaymentStatus.AddField8 != null && objPaymentStatus.AddField8 != "")
      {
        string[] FeeHeadingSplit = objPaymentStatus.AddField8.Split(',');
        if (FeeHeadingSplit != null)
        {
          var fh = FeeHeadingSplit.Take(FeeHeadingSplit.Length).ToList();

          if (fh.Count > 0)
          {
            for (int i = 0; i < fh.Count; i++)
            {
              if (fh[i] != null && fh[i] != "")
              {
                string[] FeeheadingSecond = fh[i].Split(',');
                int feeid = Convert.ToInt32(FeeheadingSecond[0]);
                float feeamont= feeplans.FirstOrDefault(x => x.FeeId == feeid)?.FeeValue ?? 0;
                //float feeamont = (float)Convert.ToDecimal(FeeheadingSecond[1]);
                FeeHeading feeHeading = feeHeadingList?.FirstOrDefault(x => x.FeeId == feeid) ?? new FeeHeading();
                batchname = feeplans.FirstOrDefault(x => x.FeeId == feeHeading?.FeeId)?.BatchId.ToString() ?? string.Empty;
                feetypeid = feeplans.FirstOrDefault(x => x.FeeId == feeHeading.FeeId)?.FeeTypeId.ToString() ?? string.Empty;
                if (feeHeadingNames != string.Empty)
                  feeHeadingNames = string.Join(",", feeHeadingNames, feeHeading.FeeName);
                else
                  feeHeadingNames = feeHeading?.FeeName ?? string.Empty;
                if (feeHeadingAmts != string.Empty)
                  feeHeadingAmts = string.Join(",", feeHeadingAmts, Convert.ToString(feeamont));
                else
                  feeHeadingAmts = Convert.ToString(feeamont);
                if (feeHeadingIDs != string.Empty)
                  feeHeadingIDs = string.Join(",", feeHeadingIDs, Convert.ToString(FeeheadingSecond[0]));
                else
                  feeHeadingIDs = Convert.ToString(FeeheadingSecond[0]);
              }

            }
          }
        }
        feeHeadingIDs = objPaymentStatus.AddField8;
      }

      #region oldcode

      TblFeeReceipt tblFeeReceipts = new TblFeeReceipt();
      tblFeeReceipts.FeeHeadingIds = feeHeadingIDs;
      string feeMonths = "Jun";

      if (!string.IsNullOrWhiteSpace(batchname))
      {
        int tempBatchId = 0;
        int.TryParse(batchname, out tempBatchId);
        if (tempBatchId != 0)
        {
          batchname = _lumen.TblBatches.Where(x => x.BatchId == tempBatchId).Select(x => x.BatchId.ToString()).FirstOrDefault()??string.Empty;
        }
      }

      tblFeeReceipts.BalanceAmt = 0;
      tblFeeReceipts.OldBalance = 0;
      //tblFeeReceipts.BankName = "";
      tblFeeReceipts.CategoryId = Convert.ToInt32(categoryDetail?.DataListItemId);
      tblFeeReceipts.CategoryName = categoryDetail?.DataListItemName;
      tblFeeReceipts.ClassId = Convert.ToInt32(studentdetails?.ClassId);
      tblFeeReceipts.ClassName = classDetail?.DataListItemName;
      tblFeeReceipts.Type = feetypeid;
      tblFeeReceipts.BatchName = batchname;
      //tblFeeReceipts.FeePaidDate = feeReceiptViewModel.DateTimeVal != null ? feeReceiptViewModel.DateTimeVal : DateTime.Now;
      tblFeeReceipts.AddedDate = DateTime.Now;
      tblFeeReceipts.Concession = 0;
      tblFeeReceipts.ConcessionAmt = float.Parse(objPaymentStatus.AddField4);
      tblFeeReceipts.LateFee = 0;
      tblFeeReceipts.PaidMonths = "Jun";
      tblFeeReceipts.PayHeadings = feeHeadingNames;
      tblFeeReceipts.PaymentMode = "Online";
      tblFeeReceipts.ReceiptAmt = float.Parse(objPaymentStatus.TrnAmt);
      // tblFeeReceipts.Remark = "Online Payment";
      tblFeeReceipts.StudentId = stuid;
      tblFeeReceipts.StudentName = objPaymentStatus.AddField1;
      tblFeeReceipts.TotalFee = float.Parse(objPaymentStatus.TrnAmt);
      tblFeeReceipts.FeePaids = feeHeadingAmts;
      tblFeeReceipts.FeeReceiptsOneTimeCreator = unicNumber.ToString();
      tblFeeReceipts.DueAmount = "0"; //objPaymentStatus.TrnAmt;
      tblFeeReceipts.PaidAmount = objPaymentStatus.TrnAmt;



      objPaymentStatus.AddField2 = classDetail?.DataListItemName??string.Empty;
      objPaymentStatus.AddField3 = categoryDetail?.DataListItemName ?? string.Empty;


      if (feeMonths.Contains("Jan"))
      {
        tblFeeReceipts.Jan = true;
      }
      if (feeMonths.Contains("Feb"))
      {
        tblFeeReceipts.Feb = true;
      }
      if (feeMonths.Contains("Mar"))
      {
        tblFeeReceipts.Mar = true;
      }
      if (feeMonths.Contains("Apr"))
      {
        tblFeeReceipts.Apr = true;
      }
      if (feeMonths.Contains("May"))
      {
        tblFeeReceipts.May = true;
      }
      if (feeMonths.Contains("Jun"))
      {
        tblFeeReceipts.Jun = true;
      }
      if (feeMonths.Contains("Jul"))
      {
        tblFeeReceipts.Jul = true;
      }
      if (feeMonths.Contains("Aug"))
      {
        tblFeeReceipts.Aug = true;
      }
      if (feeMonths.Contains("Sep"))
      {
        tblFeeReceipts.Sep = true;
      }
      if (feeMonths.Contains("Oct"))
      {
        tblFeeReceipts.Oct = true;
      }
      if (feeMonths.Contains("Nov"))
      {
        tblFeeReceipts.Nov = true;
      }
      if (feeMonths.Contains("Dec"))
      {
        tblFeeReceipts.Dec = true;
      }
      _lumen.TblFeeReceipts.Add(tblFeeReceipts);
      await _lumen.SaveChangesAsync();

      ////

      var feereceiptsid = _lumen.TblFeeReceipts.FirstOrDefault(x => x.StudentId == stuid && x.FeeReceiptsOneTimeCreator == unicNumber.ToString());
      return feereceiptsid?.FeeReceiptId.ToString() ?? "Receipt generation failed.";
      

      #endregion

     
    }
    catch (Exception e)
    {

      return $"Error: {e.Message}";
    }
  }
  public class TblMerchantclass
  {
    public int MerchantName_Id { get; set; }
    public string? MerchantName { get; set; }
    public int School_Id { get; set; }
    public int Bank_Id { get; set; }
    public int Branch_Id { get; set; }
    public string? Schoolname { get; set; }
    public string? BankName { get; set; }
    public string? BranchName { get; set; }
    public int MerchantId { get; set; }
    public string? MerchantKey { get; set; }
    public string? MerchantMID { get; set; }
    public int Schoolsetup_Id { get; set; }
    public string? Status { get; set; }
    public string? Feeconfiguration { get; set; }

  }
  #endregion
  #region Marchent Details
  public async Task<IApiResponse> MarchentDetail()
  {
    IApiResponse res = new ApiResponse();
    MarchentDetails marchentDetails = new MarchentDetails();
    var schoolSetup = await _lumen.Tbl_CreateMerchantId.FirstOrDefaultAsync();
    if (schoolSetup == null)
    {
      marchentDetails=new MarchentDetails();
    }
    else {
      marchentDetails.MarchentId = schoolSetup.MerchantMID ;
      marchentDetails.UserId = schoolSetup.UserId ?? string.Empty;
      marchentDetails.EncryptKey = string.Format(configuration.GetSection("PayUSettings").GetSection("atomtechEncrptkey").Value ?? string.Empty);
      marchentDetails.DecryptKey = string.Format(configuration.GetSection("PayUSettings").GetSection("atomtechDecrptkey").Value ?? string.Empty);
      marchentDetails.Password = schoolSetup.Password??string.Empty;
      marchentDetails.ReturnUrl = string.Format(configuration.GetSection("PayUSettings").GetSection("atomtechReturnUrl").Value ?? string.Empty);
    }
    res.Data = marchentDetails;
    res.ResponseCode = "200";
    return res;
  }
  #endregion
  public async Task<FeeDetails> GetStudentFeesAsync(string applicationNumber)
  {
    float totalAmount = 0;


    var student = _lumen.Students
        .Where(x => x.ApplicationNumber == applicationNumber.ToString()).FirstOrDefault();


    if (student == null)
    {
      throw new Exception("Student not found");
    }
    var studentData = _lumen.Students
        .Where(x => x.ApplicationNumber == student.ApplicationNumber).FirstOrDefault();

    if (studentData == null)
    {
      throw new Exception("Student  details not found");
    }

    var activeBatches = _lumen.TblBatches
    .Where(x => x.IsActiveForPayments)
    .Select(x => x.BatchId)
    .ToList();

    var classListId = _lumen.TblDataLists
        .Where(x => x.DataListName == "class")
        .Select(x => x.DataListId)
        .FirstOrDefault();

    var className = _lumen.TblDataListItems
        .Where(e => e.DataListId == classListId.ToString() && e.DataListItemId == studentData.ClassId)
        .Select(x => x.DataListItemName)
        .FirstOrDefault();

    if (string.IsNullOrEmpty(className))
    {
      throw new Exception("Class Name not found");
    }

    var feePlans = _lumen.FeePlans
      .Where(x => x.ClassName == className &&
      x.FeeTypeId == 222 &&
      studentData.Medium != null
      && x.Medium != null && x.Medium.Contains(studentData.Medium))
        .ToList();




    var totalFeeReceipts = _lumen.TblFeeReceipts
        .Where(x => x.StudentId == studentData.StudentId)
        .ToList();

    var feeDetails = new List<FeeDetailsDto>();


    foreach (var feePlan in feePlans)
    {
      var feeReceipts = totalFeeReceipts
          .Where(x => x.FeeHeadingIds != null && x.FeeHeadingIds.Split(',').Contains(feePlan.FeeId.ToString()))
          .ToList();

      var totalPaid = feeReceipts.Any()
          ? feeReceipts.Sum(x => string.IsNullOrEmpty(x.PaidAmount) ? 0 : Convert.ToInt64(x.PaidAmount))
          : 0;

      var balance = feePlan.FeeValue - totalPaid;
      if (balance > 0)
      {
        if (!((studentData.CurrentYear == studentData.BatchId && feePlan.FeeName == "Re-Admission") || (studentData.CurrentYear != studentData.BatchId && feePlan.FeeName == "New Admission")))
        {
          totalAmount += balance;
          feeDetails.Add(new FeeDetailsDto
          {
            FeeId = feePlan.FeeId,
            FeeName = feePlan.FeeName,
            FeeValue = feePlan.FeeValue,
            PaidAmount = totalPaid,
            Balance = balance
          });
        }
      }
    }
      return await Task.FromResult(new FeeDetails
      {
        FeeDetailsDto = feeDetails,
        TotalAmount = totalAmount
      });
    
  }
  public async Task<IApiResponse> GetStudentDetail(string ApplicationNo)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var studentData = await (from s in _lumen.Students
                               join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                               join d in _lumen.TblDataListItems on s.SectionId equals d.DataListItemId
                               join f in _lumen.FamilyDetails on s.StudentId equals f.StudentRefId into fJoin
                               from f in fJoin.DefaultIfEmpty() // Left join in case FamilyDetails doesn't exist
                               where s.ApplicationNumber == ApplicationNo
                               select new
                               {
                                 s.StudentId,
                                 s.BatchId,
                                 s.ClassId,
                                 s.SectionId,
                                 s.Name,
                                 s.LastName,
                                 Class = c.DataListItemName,
                                 Section = d.DataListItemName,
                                 s.RollNo,
                                 FatherName = f != null ? f.FatherName : "",
                                 MotherName = f != null ? f.MotherName : "",
                                 ContactNo = s.Mobile,
                                 FatherEmail = f != null ? f.Femail : "",
                                 Email = s.ParentEmail
                               }).FirstOrDefaultAsync(); // Use FirstOrDefault instead of ToList

      if (studentData == null)
      {
        //res.Data = null;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
        return res;
      }

      var result = new
      {
        studentData.StudentId,
        studentData.Name,
        studentData.LastName,
        studentData.Class,
        studentData.Section,
        studentData.RollNo,
        studentData.FatherName,
        studentData.MotherName,
        studentData.ContactNo,
        studentData.FatherEmail,
        studentData.Email,
        Attendance = Attendance(studentData.BatchId, studentData.ClassId, studentData.StudentId, studentData.SectionId)
      };

      res.Data = result;
      res.Msg = "Record fetched successfully.";
      res.ResponseCode = "200";
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      // Log the exception
    }
    return res;
  }

  public string Attendance(int BatchId, int ClassId, long StudentID, int? SectionId = 0)
  {
    try
    {
      var ActualAttendance = _lumen.TblStudentAttendances
          .Where(x => x.StudentRegisterId == StudentID &&
                     x.ClassId == ClassId &&
                     x.SectionId == SectionId &&
                     x.BatchId == BatchId)
          .ToList();

      double attendedDays = 0;
      double attendedHalfDays = 0;

      foreach (var item in ActualAttendance)
      {
        if (item.MarkFullDayAbsent == "True")
        {
          attendedDays++;
        }
        if (item.MarkHalfDayAbsent == "True")
        {
          attendedHalfDays++;
        }
        if (item.Others == "True")
        {
          attendedDays++;
        }
      }

      int totalAttendedDays = Convert.ToInt32(attendedDays + (attendedHalfDays / 2));
      string Attendance = totalAttendedDays + "/" + ActualAttendance.Count;
      return Attendance;
    }
    catch (Exception ex)
    {
      // Log the error
      return $"Error calculating attendance{ex.Message}";
    }
  }
  public class MarchentDetails
  {
    public string MarchentId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string EncryptKey { get; set; } = string.Empty;
    public string DecryptKey { get; set; } = string.Empty;
    public string Password { get; set; } =string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;

  }

}
