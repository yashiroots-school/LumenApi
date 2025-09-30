using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using static LumenApi.Web.Services.ExamService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LumenApi.Web.ViewModels;
using System.Data.Entity;
using LumenApi.Web.Services;

namespace LumenApi.Web.Controllers;
public class ReportCardController(Lumen090923Context lumen, ReportCardService reportCardService) : Controller
{
  private readonly Lumen090923Context _lumen = lumen;
  private readonly ReportCardService _reportCardService = reportCardService;
  public async Task<IActionResult> Print(int studentId, int termId, int BatchId, int ClassId, int SectionId)
  {
    //int BatchId = 20;
    //int studentId = 2311;
    //int termId = 10;
    ViewBag.SchoolNewName = "Nirmala Convent S.S. School";
    ViewBag.newAddress = "New Delhi";
    ViewBag.current_Year = "2024";

    var printReportCard = await _reportCardService.GetReportCardDataAsync(studentId, termId, BatchId,null);

    return View(printReportCard);
  }

  public string GetGrade(decimal percentage, int classid)
  {
    // Query the database to get the appropriate grade
    var grade = _lumen.GradingCriterias
        .Where(g => percentage >= g.MinimumPercentage && percentage <= g.MaximumPercentage && classid == g.ClassID)
        .Select(g => g.Grade)
        .FirstOrDefault();

    // If no grade is found (percentage outside the grading range), return "N/A" or handle as needed.
    return grade ?? "D";
  }
}
