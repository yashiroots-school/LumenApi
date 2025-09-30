using System.Security.Claims;
using DinkToPdf;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.UseCases.CommonClasses;
using LumenApi.Web.Helpers;
using LumenApi.Web.Models.Params;
using LumenApi.Web.Services;

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Build.Framework;
using Org.BouncyCastle.Ocsp;
using Rotativa.AspNetCore;
using LumenApi.Web.ViewModels;
using DinkToPdf.Contracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;




//using IronPdf;
namespace LumenApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ExamController(IExamInterface examInterface, IActionContextAccessor actionContextAccessor, ReportCardService reportCardService,
  IHttpContextAccessor httpContextAccessor, ILogger<ExamController> logger
  //Converter converter
  ) : ControllerBase
{
  private IExamInterface _examInterface = examInterface;
  IApiResponse? res;
  private readonly IActionContextAccessor _actionContextAccessor = actionContextAccessor;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
  private readonly ILogger<ExamController> _logger = logger;


  private readonly ReportCardService _reportCardService = reportCardService;
  //private readonly IConverter _converter = converter;

  [HttpPost("GetPublishData")]
  public async Task<IApiResponse> GetPublishData(PublishDetail objPublishDetail)
  {
    res = new ApiResponse();
    try
    {


      res = await _examInterface.GetPublishData(objPublishDetail);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("FreezeUnfreezeData")]
  public async Task<IApiResponse> FreezeUnfreezeData(FreezeUnfreezeDTO objFreeze)
  {
    res = new ApiResponse();
    try
    {
      res = await _examInterface.FreezeUnfreezeData(objFreeze);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("GetFreezingData")]
  public async Task<IApiResponse> GetFreezingData(FreezeUnfreezeDTO objFreeze)
  {
    res = new ApiResponse();
    try
    {
      res = await _examInterface.GetFreezingData(objFreeze);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetStaffList")]
  public async Task<IApiResponse> GetStaffList()
  {
    res = new ApiResponse();
    try
    {
      var data = await _examInterface.getStaffList();
      res = data;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("GetStaffSubject")]
  public async Task<IApiResponse> GetStaffSubject(int staffId, int classId, int sectionId)
  {
    res = new ApiResponse();
    try
    {
      res = await _examInterface.GetStaffSubjects(staffId, classId, sectionId);
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("GetClassList")]
  public async Task<IApiResponse> GetClassList(int staff_Id)
  {
    res = new ApiResponse();
    try
    {
      var data = await _examInterface.GetClassList(staff_Id);
      res = data;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("GetStaffClassList")]
  public async Task<IApiResponse> GetStaffClassList(int staff_Id)
  {
    res = new ApiResponse();
    try
    {
      var data = await _examInterface.GetStaffClassList(staff_Id);
      res = data;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetSectionList")]
  public async Task<IApiResponse> GetSectionList(int staffId, int classId)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetSectionList(staffId, classId);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetStaffSectionList")]
  public async Task<IApiResponse> GetStaffSectionList(int staffId, int classId)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetStaffSectionList(staffId, classId);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetTermDropList")]
  public async Task<IApiResponse> GetTermDropList()
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetTermDropList();
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("GetBatchDropList")]
  public async Task<IApiResponse> GetBatchDropList()
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetBatchDropList();
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("FillMarksreport")]
  public async Task<IApiResponse> StudentByClassSection(ReportFillmarks _objfilmarks)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.StudentByClassSection(_objfilmarks.classId, _objfilmarks.sectionId, _objfilmarks.testId, _objfilmarks.termId, _objfilmarks.staffId, _objfilmarks.batchId);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("ReportCard")]
  public async Task<IApiResponse> ReportCard(ReportCard _objreportcad)
  {
    res = new ApiResponse();
    try
    { 
      var result = await _examInterface.ReportCard(_objreportcad.classId, _objreportcad.sectionId, _objreportcad.termId, _objreportcad.batchId);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("getPrintReport")]
  public async Task<IApiResponse> getPrintReport(int rolnum, int term)
  {
    res = new ApiResponse();
    try
    {
     var result = await _examInterface.GradAll(rolnum, term);
      res = result;
      byte[] pdfBytes = Convert.FromBase64String(result.Data.ToString()!);
     // res= System.IO.File(pdfBytes, "application/pdf", "ConvertedDocument.pdf") ;
     return res;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpPost("SaveFillmarkReport")]
  public async Task<IApiResponse> SaveFillmarkReport(List<StudentObtainedMark> rowData)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.SaveFillmarkReport(rowData);
      res = result;
     // byte[] pdfBytes = Convert.FromBase64String(result.Data.ToString()!);
      // res= System.IO.File(pdfBytes, "application/pdf", "ConvertedDocument.pdf") ;
      return res;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpPost("FillCoScholasticAreasRepor")]
  public async Task<IApiResponse> FillCoScholasticAreasRepor(FillCoScholasticAreasReporModel _objmodel)
  {
    res = new ApiResponse();
    try
    {
      //classId=208&sectionId=244&termId=3
      // LumenApi.Infrastructure.Data.Lumen090923Context lumen=new LumenApi.Infrastructure.Data.Lumen090923Context();
      var result = await _examInterface.FillCoScholasticAreasRepor(_objmodel);
      res = result;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }


  [HttpPost("SaveFillCoScholasticMarks")]
  public async Task<IApiResponse> SaveFillCoScholasticMarks(List<CoScholasticObtainedModel> rowData)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.SaveFillCoScholasticMarks(rowData);
      res = result;
     // byte[] pdfBytes = Convert.FromBase64String(result.Data.ToString()!);
      // res= System.IO.File(pdfBytes, "application/pdf", "ConvertedDocument.pdf") ;
      return res;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  [HttpGet("ReportCardData")]
  public async Task<IActionResult> StudentReportCardData(int studentId, int termId, int batchId, int ClassId, int SectionId)
  {
    res = new ApiResponse();
    try
    {
      var printReportCard = await _reportCardService.GetReportCardDataAsync(studentId, termId, batchId,null);
      return Ok(printReportCard);

    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      Console.Write(ex.ToString());
      throw;
    }
  }
  [HttpGet("HoldUnHoldStudentReportCard")]
  public async Task<IApiResponse> HoldUnHoldStudentReportCard(int StudentId, int term, int Batch, int classid, string Remark, bool isHold)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.HoldUnHoldStudentReportCard(StudentId, term, Batch, classid, Remark, isHold);
      res = result;
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }



  [HttpGet("PrintReportCardData")]
  public async Task<IActionResult> PrintStudentReportCardData(int studentId, int termId, int batchId, int ClassId, int SectionId,string?ApplicationNo,int id=1)
  {
    res = new ApiResponse();
    
    try
    {
      var printReportCard = await _reportCardService.GetReportCardDataForStMaryThuraAsync(studentId, termId, batchId,ApplicationNo);
      //if(id==2)
      //{
      //  printReportCard = await _reportCardService.GetReportCardDataForStMaryThuraAsync(studentId, termId, batchId, ApplicationNo);
      //}
      //PrintReportCardData printReportCard = new PrintReportCardData();
      //var pdf = new ViewAsPdf("Print", printReportCard)
      //{
      //  FileName = "ReportCard.pdf",
      //  PageSize = Rotativa.AspNetCore.Options.Size.A4,
      //  PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
      //  CustomSwitches = "--enable-local-file-access --debug-javascript --javascript-delay 1000"

      //  // CustomSwitches = "--enable-local-file-access --no-stop-slow-scripts --timeout 10000" // Adjust as needed
      //  // CustomSwitches = "--enable-local-file-access --lowquality --dpi 300 --no-stop-slow-scripts --cache-dir=/path/to/temp"
      //};
      //byte[] pdfData = await pdf.BuildFile(ControllerContext);

      //if (pdfData == null || pdfData.Length == 0)
      //{
      //  _logger.LogError("Failed to generate PDF file for StudentId={studentId}, TermId={termId}, BatchId={batchId}",
      //                           studentId, termId, batchId);
      //  throw new Exception("Failed to generate PDF file.");
      //}

      //return File(pdfData, "application/pdf", "StudentReport.pdf");

      return Ok(printReportCard);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while generating the report card for StudentId={studentId}, TermId={termId}, BatchId={batchId}",
                             studentId, termId, batchId);
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      Console.Write(ex.ToString());
      throw;
    }
  }

  [HttpGet("PrintStudentReportCardDataForCLass")]
  public async Task<IActionResult> PrintStudentReportCardDataForCLass(int termId, int BatchId, int classId, int sectionId)
  {
    res = new ApiResponse();

    try
    {
      //List<PrintReportCardData> printReportCard = new List<PrintReportCardData>();
       var printReportCard = await _reportCardService.GetReportCardDataByClassAsync( termId,  BatchId,  classId,  sectionId);
      //PrintReportCardData printReportCard = new PrintReportCardData();
      var pdf = new ViewAsPdf("ClassWiseReportClas", printReportCard)
      {
        FileName = "ReportCard.pdf",
        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        CustomSwitches = "--enable-local-file-access --debug-javascript --javascript-delay 1000"

        // CustomSwitches = "--enable-local-file-access --no-stop-slow-scripts --timeout 10000" // Adjust as needed
        // CustomSwitches = "--enable-local-file-access --lowquality --dpi 300 --no-stop-slow-scripts --cache-dir=/path/to/temp"
      };
      byte[] pdfData = await pdf.BuildFile(ControllerContext);

      if (pdfData == null || pdfData.Length == 0)
      {
        _logger.LogError("Failed to generate PDF file for class={classId}, TermId={termId}, BatchId={BatchId}",
                                  classId, termId, BatchId);
        throw new Exception("Failed to generate PDF file.");
      }

      return File(pdfData, "application/pdf", "StudentReport.pdf");

      //return Ok(printReportCard);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while generating the report card for classId={classId}, TermId={termId}, BatchId={BatchId}",
                             classId, termId, BatchId);
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      Console.Write(ex.ToString());
      throw;
    }
  }
  [HttpGet("GetAllAssignmentDetails")]
  public async Task<IApiResponse> GetAllAssignmentDetails(int classid, int sectionId, int SubjectId = 0)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetHomeWorkList(classid, sectionId, SubjectId);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpGet("GetAssignmentById")]
  public async Task<IApiResponse> GetAssignmentById(int Id)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.GetAssignmentById(Id);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("UpdateAssignment")]
  public async Task<IApiResponse> UpdateAssignment(TblAssignment tbl_Assignment)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.UpdateAssignment(tbl_Assignment);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("AddHomeWork")]
  public async Task<IApiResponse> AddHomeWork(TblAssignment tbl_Assignment)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.AddHomeWork(tbl_Assignment);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  [HttpPost("DeleteHomeWork")]
  public async Task<IApiResponse> DeleteHomeWork(int Id)
  {
    res = new ApiResponse();
    try
    {
      var result = await _examInterface.DeleteAssignment(Id);
      res = result;
    }
    catch (Exception ex)
    {

      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  //[HttpGet("GeneratePdf")]
  //public async Task<IActionResult> GenerateReportCardPdf(int studentId, int termId, int batchId)
  //{
  //  var printReportCard = await _reportCardService.GetReportCardDataAsync(studentId, termId, batchId);


}
