using System.Data;
using LumenApi.Web.ViewModels;
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


using static LumenApi.Web.Services.ExamService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Globalization;
using System.Linq;
using System.Diagnostics.SymbolStore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LumenApi.Web.Services;

public class ReportCardService(Lumen090923Context lumen)
{
  private readonly Lumen090923Context _lumen = lumen;
  public async Task<PrintReportCardData> GetReportCardDataAsync(int studentId, int termId, int BatchId, string? Application)
  {
    if (studentId == 0 && Application != null)
    {

      studentId = _lumen.Students.Where(x => x.ApplicationNumber == Application).Select(x => x.StudentId).FirstOrDefault();
    }
    var result = from tr in _lumen.TblTestRecords
                 where tr.BatchId == BatchId && tr.StudentId == studentId
                       && _lumen.Tbl_PublishDetail.Any(pd => pd.ClassId == tr.ClassId && pd.IsPublish == true && pd.TermId==termId && pd.BatchId==BatchId)
                       && !_lumen.Tbl_HoldDetail.Any(hd => hd.StudentId == tr.StudentId && hd.IsHold == true && hd.TermId == termId && hd.BatchId == BatchId)
                 select tr;
    var query = result.ToList();
    if (query != null && query.Count()>0)
    { 
        
        var commandText = "EXEC Sp_GetStudentReportCardData @Batch_Id, @StudentId, @TermId";

        var parameters = new[]
        {
                new SqlParameter("@Batch_Id", BatchId),
                new SqlParameter("@StudentId", (studentId == 0) ? DBNull.Value : (object)studentId),
                new SqlParameter("@TermId", termId)
              };
        var printReportCard = new PrintReportCardData();
        var records = new List<TermSubjectMarks>();
        var connectionString = _lumen.Database.GetConnectionString();
        var schoolDetails = _lumen.TblCreateSchools
               .Select(x => new
               {
                 x.SchoolName,
                 x.Address,
                 x.CurrentYear,
                 x.UploadImage,


               })
               .FirstOrDefault();

        var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
         ? "/Content/Default/default-logo.jpeg"
         : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";
        Console.WriteLine($"SchoolLogo Path: {schoolLogoPath}");


        using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
        {
          var studentData = new StudentData
          {
            BatchID = BatchId,
            StudentID = studentId,
            TermID = termId,
            SectionID = termId,
            SchoolName = schoolDetails?.SchoolName,
            //newAddress = schoolDetails?.Address,
            CurrentYear = schoolDetails?.CurrentYear,
            SchoolLogo = schoolLogoPath
          };

          await connection.OpenAsync();
          using (var command = connection.CreateCommand())
          {
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            //commandText.Replace("@Batch_Id", BatchId.ToString());
            //commandText.Replace("@StudentId", studentId.ToString());
            //commandText.Replace("@TermId", termId.ToString());

            using (SqlCommand _command = new SqlCommand(commandText, connection))
            {
              using (SqlDataReader reader = await command.ExecuteReaderAsync())
              {
                while (await reader.ReadAsync())
                {
                  studentData.StudentName = reader["StudentName"].ToString() ?? "";
                  studentData.FatherName = reader["FatherName"].ToString() ?? "";
                  studentData.MotherName = reader["MotherName"].ToString() ?? "";
                  studentData.ScholarNo = reader["ScholarNo"].ToString() ?? "";
                  studentData.RollNo = reader["RollNo"].ToString() ?? "";
                  studentData.DateOfBirth = reader["DateOfBirth"].ToString() ?? "";
                  studentData.RankInClass = reader["RankInClass"].ToString() ?? "";
                  studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
                  studentData.ClassName = reader["ClassName"].ToString() ?? "";
                  studentData.SectionName = reader["SectionName"].ToString() ?? "";
                  studentData.Attendance = 0; //float.Parse(reader["Attendance"]?? 0);
                  studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
                  studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
                  studentData.Remark = reader["Remark"].ToString() ?? "";
                  studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "");
                  //studentData.ClassID = reader["ClassID"] != DBNull.Value && int.TryParse(reader["ClassID"].ToString(), out int classId)? classId: 0;
                  studentData.SchoolLogo = schoolLogoPath;
                  studentData.SchoolName = schoolDetails?.SchoolName;
                  //studentData.newAddress = schoolDetails?.Address;
                  studentData.CurrentYear = schoolDetails?.CurrentYear;
                  studentData.BatchID = BatchId;
                  studentData.TermID = termId;
                  //studentData.SectionID = sectionId;
                }

                if (await reader.NextResultAsync())
                {
                  while (await reader.ReadAsync())
                  {
                    records.Add(new TermSubjectMarks
                    {
                      TestID = Convert.ToInt32(reader["TestID"].ToString()),
                      TermName = reader["Term"].ToString() ?? "",
                      SubjectName = reader["Subject"].ToString() ?? "",
                      Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? ""),
                      TestType = reader["TestType"].ToString() ?? "",
                      MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? ""),
                      Grade = (reader["Grade"].ToString() ?? ""),
                      IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "")
                    });

                  }
                }
              }

            }

            printReportCard = new PrintReportCardData
            {
              StudentData = studentData,

            };
          }
          var grouped_records = (from r in records
                                 group r by r.SubjectName into g
                                 select g).ToList();

          var GroupedSubjects = new List<GroupedSubjects>();
          foreach (var g in grouped_records)
          {
            var subject = new GroupedSubjects
            {
              SubjectName = g.Key,
              Terms = new List<SubjectTermRecord>(),
              IsOptional = g.Select(x => x.IsOptional).FirstOrDefault(),
            };
            foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
            {
              var PracticalMark = g.Where(x => x.SubjectName == g.Key
              && x.TermName == t.TermName
              && x.TestType.ToUpper() == "PRACTICAL")
                  .Select(x => x.Mark).FirstOrDefault();
              var PracticalMaxMark = g.Where(x => x.SubjectName == g.Key
             && x.TermName == t.TermName
             && x.TestType.ToUpper() == "PRACTICAL")
                 .Select(x => x.MaximumMarks).FirstOrDefault();

              var totalMark = t.Mark + PracticalMark;
              decimal percentage = Math.Round((int)totalMark / (t.MaximumMarks + PracticalMaxMark) * 100, 2);

              var grade = t.IsOptional ? t.Grade : GetGrade(percentage, studentData.ClassID, termId, BatchId);
              subject.Terms.Add(new SubjectTermRecord
              {
                Name = t.TermName,
                TheoryMark = t.Mark,
                PracticalMark = PracticalMark,
                MaximumMarks = t.MaximumMarks + PracticalMaxMark,
                TotallMark = t.Mark + PracticalMark,
                Grade = grade,
                IsOptional = t.IsOptional,
              });
            }
            GroupedSubjects.Add(subject);
          }

          printReportCard.GroupedSubjects = GroupedSubjects;


          List<GroupedTerms> grouped_terms_records = new List<GroupedTerms>();
          var grouped_by_term_records = (from r in records
                                         where r.IsOptional != true
                                         group r by new
                                         {
                                           r.TermName,
                                           r.TestType
                                         } into g
                                         select g).ToList();

          foreach (var tr in grouped_by_term_records)
          {
            var totalMarks = tr.Sum(x => x.Mark);
            var maxMarksForGroup = tr.FirstOrDefault()?.MaximumMarks;
            var totalMaxMarksForTermAndTestType = records
                .Where(x => x.TermName == tr.Key.TermName && x.TestType == tr.Key.TestType)
                .Sum(x => x.MaximumMarks);

            decimal _maxMarksForGroup = Convert.ToDecimal(maxMarksForGroup);
            decimal _totalMaxMarksForTermAndTestType = Convert.ToDecimal(totalMaxMarksForTermAndTestType);
            decimal percentage = 0;
            if (_totalMaxMarksForTermAndTestType > 0)
            {
              percentage = Math.Round((totalMarks / _totalMaxMarksForTermAndTestType) * 100, 2);
            }


            var grade = GetGrade(percentage, studentData.ClassID, termId, BatchId);

            grouped_terms_records.Add(new GroupedTerms
            {
              Term = tr.Key.TermName,
              TestType = tr.Key.TestType,
              Total = totalMarks,
              MaximumMarks = maxMarksForGroup ?? 0,
              Percentage = percentage,
              Grade = grade
            });
          }
          printReportCard.ObtainedPercent = 0;
          decimal overallTotalMarks = records.Sum(x => x.Mark);
          decimal overallTotalMaxMarks = records.Sum(x => x.MaximumMarks);

          if (overallTotalMaxMarks > 0) // Avoid division by zero
          {
            printReportCard.ObtainedPercent = Math.Round((overallTotalMarks / overallTotalMaxMarks) * 100, 2);
            printReportCard.ObtainedMarks= overallTotalMarks;
          }
          printReportCard.GroupedTerms = grouped_terms_records;
          printReportCard.Term = records.Select(x => x.TermName).Distinct().ToList();

      


        var firstCoSchResult = _lumen.TblCoScholasticResults
            .Where(c => c.ClassId == printReportCard.StudentData.ClassID &&
                        //c.SectionId == printReportCard.StudentData.SectionID &&
                        c.TermId == termId &&
                        c.StudentId == printReportCard.StudentData.StudentID)
            .OrderBy(c => c.Id)
            .FirstOrDefault();
        List<CoscholasticAreaDatas> coscholasticAreaData = new();

        if (firstCoSchResult != null)
        {
          var coscholasticResult = (from cog in _lumen.TblCoScholasticObtainedGrades
                                    join cr in _lumen.TblCoScholastics on cog.CoscholasticId equals cr.Id into crJoin
                                    from cr in crJoin.DefaultIfEmpty()
                                    where cog.ObtainedCoScholasticId == firstCoSchResult.Id &&
                                          cog.batchId == BatchId
                                    select new
                                    {
                                      CoscholasticID = cog.Id,
                                      Title = cr.Title,
                                      ObtainedGrade = cog.ObtainedGrade,
                                      Term = _lumen.TblTerms
                                            .Where(x => x.TermId == termId)
                                            .Select(x => x.TermName)
                                            .FirstOrDefault()
                                    }).ToList();

          coscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
          {
            Name = item.Title,
            ObtainedGrade = item.ObtainedGrade,
            Term = item.Term
          }).ToList();
        }

        // Step 3: Assign to report card
        printReportCard.CoscholasticAreaData = coscholasticAreaData;

        var gradinglist = _lumen.GradingCriterias
              .Where(x =>
               x.BatchID == BatchId
              && x.ClassID == printReportCard.StudentData.ClassID && x.TermID == termId
              ).Select(x => new GradingCriterias
              {
                MaximumPercentage = x.MaximumPercentage,
                MinimumPercentage = x.MinimumPercentage,
                Grade = x.Grade,
                GradeDescription = x.GradeDescription
              })
              .ToList();
          printReportCard.GradingCriteria = gradinglist;
          string termName = _lumen.TblTerms.Where(x => Convert.ToInt32(x.TermId) == termId).Select(t => t.TermName).FirstOrDefault() ?? "";

        if (GroupedSubjects == null || !GroupedSubjects.Any())
        {
          printReportCard.GroupedSubjects = null;
          printReportCard.Result = "";
        }
        else
        {
          printReportCard.Result = "Pass";
        }
        if (printReportCard.GroupedSubjects != null && printReportCard.GroupedSubjects.Any(g => g.Terms != null && g.Terms.Any(t => t.Grade == "D"|| t.Grade == "E")))
        {
          printReportCard.Result = "";
        }
        return printReportCard;
        }
    }
    else{
      var printReportCard = new PrintReportCardData();
      return printReportCard;
    }

  }
  //public async Task<PrintReportCardData> GetReportCardDataForStMaryThuraAsync(int studentId, int termId, int BatchId, string? Application)
  //{
  //  if (studentId == 0 && Application != null)
  //  {
  //    studentId = _lumen.Students
  //        .Where(x => x.ApplicationNumber == Application)
  //        .Select(x => x.StudentId)
  //        .FirstOrDefault();
  //  }

  //  var result = from tr in _lumen.TblTestRecords
  //               where tr.BatchId == BatchId && tr.StudentId == studentId
  //                     && _lumen.Tbl_PublishDetail.Any(pd => pd.ClassId == tr.ClassId && pd.IsPublish == true && pd.TermId == termId && pd.BatchId == BatchId)
  //                     && !_lumen.Tbl_HoldDetail.Any(hd => hd.StudentId == tr.StudentId && hd.IsHold == true && hd.TermId == termId && hd.BatchId == BatchId)
  //               select tr;

  //  var query = result.ToList();
  //  if (query == null || query.Count == 0)
  //    return new PrintReportCardData();

  //  var commandText = "EXEC Sp_GetStudentReportCardData @Batch_Id, @StudentId, @TermId";
  //  var parameters = new[]
  //  {
  //      new SqlParameter("@Batch_Id", BatchId),
  //      new SqlParameter("@StudentId", (studentId == 0) ? DBNull.Value : (object)studentId),
  //      new SqlParameter("@TermId", termId)
  //  };

  //  var printReportCard = new PrintReportCardData();
  //  var records = new List<TermSubjectMarksStMaryThura>();
  //  var connectionString = _lumen.Database.GetConnectionString();

  //  var schoolDetails = _lumen.TblCreateSchools
  //      .Select(x => new
  //      {
  //        x.SchoolName,
  //        x.Address,
  //        x.CurrentYear,
  //        x.UploadImage
  //      })
  //      .FirstOrDefault();

  //  var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
  //      ? "/Content/Default/default-logo.jpeg"
  //      : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";

  //  using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
  //  {
  //    var studentData = new StudentData
  //    {
  //      BatchID = BatchId,
  //      StudentID = studentId,
  //      TermID = termId,
  //      SectionID = termId,
  //      SchoolName = schoolDetails?.SchoolName,
  //      CurrentYear = schoolDetails?.CurrentYear,
  //      SchoolLogo = schoolLogoPath
  //    };

  //    await connection.OpenAsync();
  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = commandText;
  //      command.CommandType = CommandType.Text;
  //      command.Parameters.AddRange(parameters);

  //      using (SqlDataReader reader = await command.ExecuteReaderAsync())
  //      {
  //        while (await reader.ReadAsync())
  //        {
  //          studentData.StudentName = reader["StudentName"].ToString() ?? "";
  //          studentData.FatherName = reader["FatherName"].ToString() ?? "";
  //          studentData.MotherName = reader["MotherName"].ToString() ?? "";
  //          studentData.ScholarNo = reader["ScholarNo"].ToString() ?? "";
  //          studentData.RollNo = reader["RollNo"].ToString() ?? "";
  //          studentData.DateOfBirth = reader["DateOfBirth"].ToString() ?? "";
  //          studentData.RankInClass = reader["RankInClass"].ToString() ?? "";
  //          studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
  //          studentData.ClassName = reader["ClassName"].ToString() ?? "";
  //          studentData.SectionName = reader["SectionName"].ToString() ?? "";
  //          studentData.Attendance = 0;
  //          studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
  //          studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
  //          studentData.Remark = reader["Remark"].ToString() ?? "";
  //          studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "0");
  //        }

  //        if (await reader.NextResultAsync())
  //        {
  //          while (await reader.ReadAsync())
  //          {
  //            records.Add(new TermSubjectMarksStMaryThura
  //            {
  //              TestID = Convert.ToInt32(reader["TestID"].ToString()),
  //              TermName = reader["Term"].ToString() ?? "",
  //              SubjectName = reader["Subject"].ToString() ?? "",
  //              Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? "0"),
  //              TestType = reader["TestType"].ToString() ?? "",
  //              MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? "0"),
  //              MinimumMarks = decimal.Parse(reader["MinimumMarks"].ToString() ?? "0"),
  //              Grade = reader["Grade"].ToString() ?? "",
  //              IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "false")
  //            });
  //          }
  //        }
  //      }

  //      printReportCard = new PrintReportCardData
  //      {
  //        StudentData = studentData,
  //      };
  //    }

  //    // Group subjects
  //    var grouped_records = (from r in records
  //                           group r by r.SubjectName into g
  //                           select g).ToList();

  //    var GroupedSubjects = new List<GroupedSubjects>();
  //    foreach (var g in grouped_records)
  //    {
  //      var subject = new GroupedSubjects
  //      {
  //        SubjectName = g.Key,
  //        Terms = new List<SubjectTermRecord>(),
  //        IsOptional = g.Select(x => x.IsOptional).FirstOrDefault(),
  //      };

  //      foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
  //      {
  //        var PracticalMark = g.Where(x => x.SubjectName == g.Key
  //                        && x.TermName == t.TermName
  //                        && x.TestType.ToUpper() == "PRACTICAL")
  //                        .Select(x => x.Mark).FirstOrDefault();

  //        var PracticalMaxMark = g.Where(x => x.SubjectName == g.Key
  //                        && x.TermName == t.TermName
  //                        && x.TestType.ToUpper() == "PRACTICAL")
  //                        .Select(x => x.MaximumMarks).FirstOrDefault();

  //        var totalMark = t.Mark + PracticalMark;
  //        decimal percentage = 0;
  //        if (t.MaximumMarks + PracticalMaxMark > 0)
  //          percentage = Math.Round((totalMark / (t.MaximumMarks + PracticalMaxMark)) * 100, 2);

  //        var grade = t.IsOptional ? t.Grade : GetGrade(percentage, studentData.ClassID, termId, BatchId);

  //        subject.Terms.Add(new SubjectTermRecord
  //        {
  //          Name = t.TermName,
  //          TheoryMark = t.Mark,
  //          PracticalMark = PracticalMark,
  //          MaximumMarks = t.MaximumMarks + PracticalMaxMark,
  //          TotallMark = t.Mark + PracticalMark,
  //          Grade = grade,
  //          IsOptional = t.IsOptional,
  //        });
  //      }
  //      GroupedSubjects.Add(subject);
  //    }

  //    printReportCard.GroupedSubjects = GroupedSubjects;

  //    // Group by terms
  //    List<GroupedTerms> grouped_terms_records = new List<GroupedTerms>();
  //    var grouped_by_term_records = (from r in records
  //                                   where r.IsOptional != true
  //                                   group r by new { r.TermName, r.TestType } into g
  //                                   select g).ToList();

  //    foreach (var tr in grouped_by_term_records)
  //    {
  //      var totalMarks = tr.Sum(x => x.Mark);
  //      var totalMaxMarksForTermAndTestType = records
  //          .Where(x => x.TermName == tr.Key.TermName && x.TestType == tr.Key.TestType)
  //          .Sum(x => x.MaximumMarks);

  //      decimal percentage = 0;
  //      if (totalMaxMarksForTermAndTestType > 0)
  //        percentage = Math.Round((totalMarks / totalMaxMarksForTermAndTestType) * 100, 2);

  //      var grade = GetGrade(percentage, studentData.ClassID, termId, BatchId);

  //      grouped_terms_records.Add(new GroupedTerms
  //      {
  //        Term = tr.Key.TermName,
  //        TestType = tr.Key.TestType,
  //        Total = totalMarks,
  //        MaximumMarks = totalMaxMarksForTermAndTestType,
  //        Percentage = percentage,
  //        Grade = grade
  //      });
  //    }

  //    // Overall
  //    decimal overallTotalMarks = records.Sum(x => x.Mark);
  //    decimal overallTotalMaxMarks = records.Sum(x => x.MaximumMarks);

  //    // --- RULE 1: Exclude lowest marks (except protected subject IDs) ---
  //    if (new[] { 614, 615, 416, 417, 414, 415 }.Contains(studentData.ClassID))
  //    {
  //      var protectedIds = new List<int> { 43, 44, 45, 54, 94 };

  //      var subjectToExclude = records
  //    .Where(r => r.TestID.HasValue && !protectedIds.Contains((int)r.TestID.Value))
  //    .OrderBy(r => r.Mark)
  //    .FirstOrDefault();

  //      if (subjectToExclude != null)
  //      {
  //        overallTotalMarks -= subjectToExclude.Mark;
  //        overallTotalMaxMarks -= subjectToExclude.MaximumMarks;
  //      }
  //    }

  //    if (overallTotalMaxMarks > 0)
  //    {
  //      printReportCard.ObtainedPercent = Math.Round((overallTotalMarks / overallTotalMaxMarks) * 100, 2);
  //      printReportCard.ObtainedMarks = overallTotalMarks;
  //    }

  //    printReportCard.GroupedTerms = grouped_terms_records;
  //    printReportCard.Term = records.Select(x => x.TermName).Distinct().ToList();

  //    // --- RULE 2: For class 205, 206 check minimum marks ---
  //    if (new[] { 205, 206 }.Contains(studentData.ClassID))
  //    {
  //      bool failed = false;
  //      foreach (var subject in printReportCard.GroupedSubjects ?? new List<GroupedSubjects>())
  //      {
  //        foreach (var term in subject.Terms)
  //        {
  //          if (term.TheoryMark < records.First(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() != "PRACTICAL").MinimumMarks ||
  //              term.PracticalMark < records.FirstOrDefault(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() == "PRACTICAL")?.MinimumMarks)
  //          {
  //            failed = true;
  //            break;
  //          }
  //        }
  //        if (failed) break;
  //      }

  //      if (failed)
  //        printReportCard.Result = ""; // Blank
  //    }

  //    // Default Result
  //    if (GroupedSubjects == null || !GroupedSubjects.Any())
  //      printReportCard.Result = "";
  //    else
  //      printReportCard.Result = "Pass";

  //    if (printReportCard.GroupedSubjects != null &&
  //        printReportCard.GroupedSubjects.Any(g => g.Terms.Any(t => t.Grade == "D" || t.Grade == "E")))
  //    {
  //      printReportCard.Result = "";
  //    }

  //    return printReportCard;
  //  }
  //}
  public async Task<PrintReportCardData> GetReportCardDataForStMaryThuraoldAsync(
    int studentId, int termId, int BatchId, string? Application)
  {
    if (studentId == 0 && Application != null)
    {
      studentId = _lumen.Students
          .Where(x => x.ApplicationNumber == Application)
          .Select(x => x.StudentId)
          .FirstOrDefault();
    }

    var result = from tr in _lumen.TblTestRecords
                 where tr.BatchId == BatchId && tr.StudentId == studentId
                       && _lumen.Tbl_PublishDetail.Any(pd =>
                           pd.ClassId == tr.ClassId &&
                           pd.IsPublish == true &&
                           pd.TermId == termId &&
                           pd.BatchId == BatchId)
                       && !_lumen.Tbl_HoldDetail.Any(hd =>
                           hd.StudentId == tr.StudentId &&
                           hd.IsHold == true &&
                           hd.TermId == termId &&
                           hd.BatchId == BatchId)
                 select tr;

    var query = result.ToList();
    if (query == null || query.Count == 0)
      return new PrintReportCardData();

    var commandText = "EXEC Sp_GetStudentReportCardData @Batch_Id, @StudentId, @TermId";
    var parameters = new[]
    {
        new SqlParameter("@Batch_Id", BatchId),
        new SqlParameter("@StudentId", (studentId == 0) ? DBNull.Value : (object)studentId),
        new SqlParameter("@TermId", termId)
    };

    var printReportCard = new PrintReportCardData();
    var records = new List<TermSubjectMarksStMaryThura>();
    var connectionString = _lumen.Database.GetConnectionString();

    var schoolDetails = _lumen.TblCreateSchools
        .Select(x => new
        {
          x.SchoolName,
          x.Address,
          x.CurrentYear,
          x.UploadImage
        })
        .FirstOrDefault();

    var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
        ? "/Content/Default/default-logo.jpeg"
        : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";

    using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
    {
      var studentData = new StudentData
      {
        BatchID = BatchId,
        StudentID = studentId,
        TermID = termId,
        SectionID = termId,
        SchoolName = schoolDetails?.SchoolName,
        CurrentYear = schoolDetails?.CurrentYear,
        SchoolLogo = schoolLogoPath
      };

      await connection.OpenAsync();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = commandText;
        command.CommandType = CommandType.Text;
        command.Parameters.AddRange(parameters);

        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            studentData.StudentName = reader["StudentName"].ToString() ?? "";
            studentData.FatherName = reader["FatherName"].ToString() ?? "";
            studentData.MotherName = reader["MotherName"].ToString() ?? "";
            studentData.ScholarNo = reader["ScholarNo"].ToString() ?? "";
            studentData.RollNo = reader["RollNo"].ToString() ?? "";
            studentData.DateOfBirth = reader["DateOfBirth"].ToString() ?? "";
            studentData.RankInClass = reader["RankInClass"].ToString() ?? "";
            studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
            studentData.ClassName = reader["ClassName"].ToString() ?? "";
            studentData.SectionName = reader["SectionName"].ToString() ?? "";
            studentData.Attendance = 0;
            studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
            studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
            studentData.Remark = reader["Remark"].ToString() ?? "";
            studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "0");
          }

          if (await reader.NextResultAsync())
          {
            while (await reader.ReadAsync())
            {
              var rawMark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? "0");

              // 👇 Rule applied: -1 / -2 treated as 0
              var normalizedMark = (rawMark == -1 || rawMark == -2) ? 0 : rawMark;

              records.Add(new TermSubjectMarksStMaryThura
              {
                TestID = Convert.ToInt64(reader["TestID"]), // keep as long
                TermName = reader["Term"].ToString() ?? "",
                SubjectName = reader["Subject"].ToString() ?? "",
                Mark = normalizedMark,
                TestType = reader["TestType"].ToString() ?? "",
                MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? "0"),
                MinimumMarks = decimal.Parse(reader["MinimumMarks"].ToString() ?? "0"),
                Grade = reader["Grade"].ToString() ?? "",
                IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "false")
              });
            }
          }
        }

        printReportCard = new PrintReportCardData
        {
          StudentData = studentData,
        };
      }

      // --- Group Subjects ---
      var grouped_records = (from r in records
                             group r by r.SubjectName into g
                             select g).ToList();

      var GroupedSubjects = new List<GroupedSubjects>();
      foreach (var g in grouped_records)
      {
        var subject = new GroupedSubjects
        {
          SubjectName = g.Key,
          Terms = new List<SubjectTermRecord>(),
          IsOptional = g.Select(x => x.IsOptional).FirstOrDefault(),
        };

        foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
        {
          var PracticalMark = g.Where(x => x.SubjectName == g.Key
                          && x.TermName == t.TermName
                          && x.TestType.ToUpper() == "PRACTICAL")
                          .Select(x => x.Mark).FirstOrDefault();

          var PracticalMaxMark = g.Where(x => x.SubjectName == g.Key
                          && x.TermName == t.TermName
                          && x.TestType.ToUpper() == "PRACTICAL")
                          .Select(x => x.MaximumMarks).FirstOrDefault();

          var totalMark = t.Mark + PracticalMark;
          decimal percentage = 0;
          if (t.MaximumMarks + PracticalMaxMark > 0)
            percentage = Math.Round((totalMark / (t.MaximumMarks + PracticalMaxMark)) * 100, 2);

          var grade = t.IsOptional ? t.Grade : GetGrade(percentage, studentData.ClassID, termId, BatchId);

          subject.Terms.Add(new SubjectTermRecord
          {
            Name = t.TermName,
            TheoryMark = t.Mark,
            PracticalMark = PracticalMark,
            MaximumMarks = t.MaximumMarks + PracticalMaxMark,
            TotallMark = totalMark,
            Grade = grade,
            IsOptional = t.IsOptional,
          });
        }
        GroupedSubjects.Add(subject);
      }

      printReportCard.GroupedSubjects = GroupedSubjects;

      // --- Group by Terms ---
      var grouped_terms_records = new List<GroupedTerms>();
      var grouped_by_term_records = (from r in records
                                     where r.IsOptional != true
                                     group r by new { r.TermName, r.TestType } into g
                                     select g).ToList();

      foreach (var tr in grouped_by_term_records)
      {
        var totalMarks = tr.Sum(x => x.Mark);
        var totalMaxMarksForTermAndTestType = records
            .Where(x => x.TermName == tr.Key.TermName && x.TestType == tr.Key.TestType)
            .Sum(x => x.MaximumMarks);

        decimal percentage = 0;
        if (totalMaxMarksForTermAndTestType > 0)
          percentage = Math.Round((totalMarks / totalMaxMarksForTermAndTestType) * 100, 2);

        var grade = GetGrade(percentage, studentData.ClassID, termId, BatchId);

        grouped_terms_records.Add(new GroupedTerms
        {
          Term = tr.Key.TermName,
          TestType = tr.Key.TestType,
          Total = totalMarks,
          MaximumMarks = totalMaxMarksForTermAndTestType,
          Percentage = percentage,
          Grade = grade
        });
      }

      // --- Overall ---
      decimal overallTotalMarks = records.Sum(x => x.Mark);
      decimal overallTotalMaxMarks = records.Sum(x => x.MaximumMarks);

      // RULE 1: Exclude lowest marks (except protected subject IDs)
      if (new[] { 614, 615, 416, 417, 414, 415 }.Contains(studentData.ClassID))
      {
        var protectedIds = new List<long> { 43, 44, 45, 54, 94 };

        var subjectToExclude = records
            .Where(r => r.TestID.HasValue && !protectedIds.Contains(r.TestID.Value)) // protected subjects exclude from selection
            .OrderBy(r => (r.Mark < 0 ? 0 : r.Mark)) // -1/-2 -> 0
            .FirstOrDefault();

        if (subjectToExclude != null)
        {
          overallTotalMarks -= (subjectToExclude.Mark < 0 ? 0 : subjectToExclude.Mark); // subtract with 0 handling
          overallTotalMaxMarks -= subjectToExclude.MaximumMarks;
        }
      }

      // RULE 2: For class 205, 206 check minimum marks
      if (new[] { 205, 206,614,615, 416, 417, 414, 415 }.Contains(studentData.ClassID))
      {
        bool failed = false;
        foreach (var subject in printReportCard.GroupedSubjects ?? new List<GroupedSubjects>())
        {
          foreach (var term in subject.Terms)
          {
            if (term.TheoryMark <
                records.First(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() != "PRACTICAL").MinimumMarks
                || term.PracticalMark <
                records.FirstOrDefault(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() == "PRACTICAL")?.MinimumMarks)
            {
              failed = true;
              break;
            }
          }
          if (failed) break;
        }

        if (failed)
          printReportCard.Result = ""; // Blank
      }

      // Default Result
      if (GroupedSubjects == null || !GroupedSubjects.Any())
        printReportCard.Result = "";
      else
        printReportCard.Result = "Pass";

      if (printReportCard.GroupedSubjects != null &&
          printReportCard.GroupedSubjects.Any(g => g.Terms.Any(t => t.Grade == "D" || t.Grade == "E")))
      {
        printReportCard.Result = "";
      }

      return printReportCard;
    }
  }
  public async Task<PrintReportCardData> GetReportCardDataForStMaryThuraAsync(int studentId, int termId, int BatchId, string? Application)
  {
    if (studentId == 0 && Application != null)
    {

      studentId = _lumen.Students.Where(x => x.ApplicationNumber == Application).Select(x => x.StudentId).FirstOrDefault();
    }
    var result = from tr in _lumen.TblTestRecords
                 where tr.BatchId == BatchId && tr.StudentId == studentId
                       && _lumen.Tbl_PublishDetail.Any(pd => pd.ClassId == tr.ClassId && pd.IsPublish == true && pd.TermId == termId && pd.BatchId == BatchId)
                       && !_lumen.Tbl_HoldDetail.Any(hd => hd.StudentId == tr.StudentId && hd.IsHold == true && hd.TermId == termId && hd.BatchId == BatchId)
                 select tr;
    var query = result.ToList();
    if (query != null && query.Count() > 0)
    {

      var commandText = "EXEC Sp_GetStudentReportCardData @Batch_Id, @StudentId, @TermId";

      var parameters = new[]
      {
                new SqlParameter("@Batch_Id", BatchId),
                new SqlParameter("@StudentId", (studentId == 0) ? DBNull.Value : (object)studentId),
                new SqlParameter("@TermId", termId)
              };
      var printReportCard = new PrintReportCardData();
      var records = new List<TermSubjectMarksStMaryThura>();
      var connectionString = _lumen.Database.GetConnectionString();
      var schoolDetails = _lumen.TblCreateSchools
             .Select(x => new
             {
               x.SchoolName,
               x.Address,
               x.CurrentYear,
               x.UploadImage,


             })
             .FirstOrDefault();

      var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
       ? "/Content/Default/default-logo.jpeg"
       : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";
      Console.WriteLine($"SchoolLogo Path: {schoolLogoPath}");


      using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
      {
        var studentData = new StudentData
        {
          BatchID = BatchId,
          StudentID = studentId,
          TermID = termId,
          SectionID = termId,
          SchoolName = schoolDetails?.SchoolName,
          //newAddress = schoolDetails?.Address,
          CurrentYear = schoolDetails?.CurrentYear,
          SchoolLogo = schoolLogoPath
        };

        await connection.OpenAsync();
        using (var command = connection.CreateCommand())
        {
          command.CommandText = commandText;
          command.CommandType = CommandType.Text;
          command.Parameters.AddRange(parameters);

          //commandText.Replace("@Batch_Id", BatchId.ToString());
          //commandText.Replace("@StudentId", studentId.ToString());
          //commandText.Replace("@TermId", termId.ToString());

          using (SqlCommand _command = new SqlCommand(commandText, connection))
          {
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                studentData.StudentName = reader["StudentName"].ToString() ?? "";
                studentData.FatherName = reader["FatherName"].ToString() ?? "";
                studentData.MotherName = reader["MotherName"].ToString() ?? "";
                studentData.ScholarNo = reader["ScholarNo"].ToString() ?? "";
                studentData.RollNo = reader["RollNo"].ToString() ?? "";
                studentData.DateOfBirth = reader["DateOfBirth"].ToString() ?? "";
                studentData.RankInClass = reader["RankInClass"].ToString() ?? "";
                studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
                studentData.ClassName = reader["ClassName"].ToString() ?? "";
                studentData.SectionName = reader["SectionName"].ToString() ?? "";
                studentData.Attendance = 0; //float.Parse(reader["Attendance"]?? 0);
                studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
                studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
                studentData.Remark = reader["Remark"].ToString() ?? "";
                studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "");
                //studentData.ClassID = reader["ClassID"] != DBNull.Value && int.TryParse(reader["ClassID"].ToString(), out int classId)? classId: 0;
                studentData.SchoolLogo = schoolLogoPath;
                studentData.SchoolName = schoolDetails?.SchoolName;
                //studentData.newAddress = schoolDetails?.Address;
                studentData.CurrentYear = schoolDetails?.CurrentYear;
                studentData.BatchID = BatchId;
                studentData.TermID = termId;
                //studentData.SectionID = sectionId;
              }

              if (await reader.NextResultAsync())
              {
                while (await reader.ReadAsync())
                {
                  records.Add(new TermSubjectMarksStMaryThura
                  {
                    SubjectId = Convert.ToInt64(reader["SubjectID"].ToString()),

                    TestID = Convert.ToInt32(reader["TestID"].ToString()),
                    TermName = reader["Term"].ToString() ?? "",
                    SubjectName = reader["Subject"].ToString() ?? "",
                    Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? ""),
                    TestType = reader["TestType"].ToString() ?? "",
                    MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? ""),
                    Grade = (reader["Grade"].ToString() ?? ""),
                    MinimumMarks= decimal.Parse(reader["MinimumMarks"].ToString() ?? ""),
                    IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "")
                  });

                }
              }
            }

          }

          printReportCard = new PrintReportCardData
          {
            StudentData = studentData,

          };
        }
        var grouped_records = (from r in records
                               group r by r.SubjectName into g
                               select g).ToList();

        var GroupedSubjects = new List<GroupedSubjects>();
        foreach (var g in grouped_records)
        {
          var subject = new GroupedSubjects
          {

            SubjectName = g.Key,
            Terms = new List<SubjectTermRecord>(),
            IsOptional = g.Select(x => x.IsOptional).FirstOrDefault(),
          };
          foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
          {
            var PracticalMark = g.Where(x => x.SubjectName == g.Key
            && x.TermName == t.TermName
            && x.TestType.ToUpper() == "PRACTICAL")
                .Select(x => x.Mark).FirstOrDefault();
            var PracticalMaxMark = g.Where(x => x.SubjectName == g.Key
           && x.TermName == t.TermName
           && x.TestType.ToUpper() == "PRACTICAL")
               .Select(x => x.MaximumMarks).FirstOrDefault();

            var totalMark = t.Mark + PracticalMark;
            decimal percentage = Math.Round((int)totalMark / (t.MaximumMarks + PracticalMaxMark) * 100, 2);

            var grade = t.IsOptional ? t.Grade : GetGrade(percentage, studentData.ClassID, termId, BatchId);
            subject.Terms.Add(new SubjectTermRecord
            {
              Name = t.TermName,
              SubjectId=t.SubjectId,
              TheoryMark = t.Mark,
              PracticalMark = PracticalMark,
              MaximumMarks = t.MaximumMarks + PracticalMaxMark,
              TotallMark = t.Mark + PracticalMark,
              Grade = grade,
              IsOptional = t.IsOptional,
            });
          }
          GroupedSubjects.Add(subject);
        }

        printReportCard.GroupedSubjects = GroupedSubjects;

        decimal overallTotalMarks = records.Sum(x => x.Mark);
        decimal overallTotalMaxMarks = records.Sum(x => x.MaximumMarks);
        List<GroupedTerms> grouped_terms_records = new List<GroupedTerms>();
        var grouped_by_term_records = (from r in records
                                       where r.IsOptional != true
                                       group r by new
                                       {
                                         r.TermName,
                                         r.TestType//,
                                        // r.SubjectId
                                       } into g
                                       select g).ToList();
        if (studentData.ClassID == 614 || studentData.ClassID == 615 || studentData.ClassID == 414 || studentData.ClassID == 415 || studentData.ClassID == 416 || studentData.ClassID == 417)
        {
          var excludedSubjectIds = new List<long> { 43, 44, 45, 54, 94 };

          // group by term + testType
           grouped_by_term_records = records
              .Where(r => r.IsOptional != true)
              .GroupBy(r => new { r.TermName, r.TestType })
              .ToList();

          // subject-wise total (Theory + Practical) for lowest subject check
          var subjectWiseTotalsOverall = records
              .Where(r => r.IsOptional != true)
              .GroupBy(r => r.SubjectId)
              .Select(g => new
              {
                SubjectId = g.Key,
                SubjectName=g.Max(x=>x.SubjectName),
                TotalMarks = g.Sum(x => x.Mark),
                TheoryMarks = g.Where(x => x.TestType == "Theory").Sum(x => x.Mark),
                PracticalMarks = g.Where(x => x.TestType == "Practical").Sum(x => x.Mark),
                TheoryMax = g.Where(x => x.TestType == "Theory").Sum(x => x.MaximumMarks),
                PracticalMax = g.Where(x => x.TestType == "Practical").Sum(x => x.MaximumMarks)
              })
              .Where(s => !excludedSubjectIds.Contains(s.SubjectId))
              .ToList();

          var lowestSubject = subjectWiseTotalsOverall.OrderBy(s => s.TotalMarks).ThenByDescending(s => s.SubjectId) .FirstOrDefault();
          var subjectName = subjectWiseTotalsOverall .Where(x => x.SubjectId == lowestSubject?.SubjectId).Select(x => x.SubjectName).FirstOrDefault();

          foreach (var termGroup in grouped_by_term_records)
          {
            // total marks for this term + testType
           // decimal totalObtained = termGroup.Sum(r => r.Mark);
           decimal totalObtained = termGroup.Sum(r => Math.Max(r.Mark, 0));
            decimal totalMax = termGroup.Sum(r => r.MaximumMarks);

            // subtract lowest subject marks for this TestType
            if (lowestSubject != null)
            {
              if (termGroup.Key.TestType == "Theory")
              {
                totalObtained -= lowestSubject.TheoryMarks;
                totalMax -= lowestSubject.TheoryMax;
              }
              else if (termGroup.Key.TestType == "Practical")
              {
                totalObtained -= lowestSubject.PracticalMarks;
                totalMax -= lowestSubject.PracticalMax;
              }
            }
            totalObtained = Math.Max(totalObtained, 0);
            totalMax = Math.Max(totalMax, 0);
            grouped_terms_records.Add(new GroupedTerms
            {
              Term = termGroup.Key.TermName,
              TestType = termGroup.Key.TestType,
              Total = totalObtained,
              MaximumMarks = totalMax
            });
            //overallTotalMarks += totalObtained;
            //overallTotalMaxMarks += totalMax;

          }

           overallTotalMarks = grouped_terms_records.Sum(g => g.Total);
           overallTotalMaxMarks = grouped_terms_records.Sum(g => g.MaximumMarks);

          printReportCard.ObtainedMarks = overallTotalMarks;
          printReportCard.ObtainedPercent = overallTotalMaxMarks > 0
              ? Math.Round((overallTotalMarks / overallTotalMaxMarks) * 100, 2)
              : 0;
          if (!string.IsNullOrEmpty(subjectName))
          {
            var excludedSubject = printReportCard.GroupedSubjects
                .FirstOrDefault(x => x.SubjectName == subjectName);

            if (excludedSubject != null && !excludedSubject.SubjectName.EndsWith(" *"))
            {
              excludedSubject.SubjectName = excludedSubject.SubjectName + " *"; // SubjectName update ho jaega
            }
          }
          //printReportCard.GroupedSubjects.Where(x => x.SubjectName == subjectName);
        }


        else
        {
          foreach (var tr in grouped_by_term_records)
          {
            var totalMarks = tr.Sum(x => x.Mark);
            var maxMarksForGroup = tr.FirstOrDefault()?.MaximumMarks;
            var totalMaxMarksForTermAndTestType = records
                .Where(x => x.TermName == tr.Key.TermName && x.TestType == tr.Key.TestType)
                .Sum(x => x.MaximumMarks);

            decimal _maxMarksForGroup = Convert.ToDecimal(maxMarksForGroup);
            decimal _totalMaxMarksForTermAndTestType = Convert.ToDecimal(totalMaxMarksForTermAndTestType);
            decimal percentage = 0;
            if (_totalMaxMarksForTermAndTestType > 0)
            {
              percentage = Math.Round((totalMarks / _totalMaxMarksForTermAndTestType) * 100, 2);
            }


            var grade = GetGrade(percentage, studentData.ClassID, termId, BatchId);

            grouped_terms_records.Add(new GroupedTerms
            {
              Term = tr.Key.TermName,
              TestType = tr.Key.TestType,
              Total = totalMarks,
              MaximumMarks = maxMarksForGroup ?? 0,
              Percentage = percentage,
              Grade = grade
            });
          }

          printReportCard.ObtainedPercent = 0;
          //  decimal overallTotalMarks = records.Sum(x => x.Mark);
          // decimal overallTotalMaxMarks = records.Sum(x => x.MaximumMarks);

          if (overallTotalMaxMarks > 0) // Avoid division by zero
          {
            printReportCard.ObtainedPercent = Math.Round((overallTotalMarks / overallTotalMaxMarks) * 100, 2);
            printReportCard.ObtainedMarks = overallTotalMarks;
          }
        }
      
     
        printReportCard.GroupedTerms = grouped_terms_records;
        printReportCard.Term = records.Select(x => x.TermName).Distinct().ToList();




        var firstCoSchResult = _lumen.TblCoScholasticResults
            .Where(c => c.ClassId == printReportCard.StudentData.ClassID &&
                        //c.SectionId == printReportCard.StudentData.SectionID &&
                        c.TermId == termId &&
                        c.StudentId == printReportCard.StudentData.StudentID)
            .OrderBy(c => c.Id)
            .FirstOrDefault();
        List<CoscholasticAreaDatas> coscholasticAreaData = new();

        if (firstCoSchResult != null)
        {
          var coscholasticResult = (from cog in _lumen.TblCoScholasticObtainedGrades
                                    join cr in _lumen.TblCoScholastics on cog.CoscholasticId equals cr.Id into crJoin
                                    from cr in crJoin.DefaultIfEmpty()
                                    where cog.ObtainedCoScholasticId == firstCoSchResult.Id &&
                                          cog.batchId == BatchId
                                    select new
                                    {
                                      CoscholasticID = cog.Id,
                                      Title = cr.Title,
                                      ObtainedGrade = cog.ObtainedGrade,
                                      Term = _lumen.TblTerms
                                            .Where(x => x.TermId == termId)
                                            .Select(x => x.TermName)
                                            .FirstOrDefault()
                                    }).ToList();

          coscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
          {
            Name = item.Title,
            ObtainedGrade = item.ObtainedGrade,
            Term = item.Term
          }).ToList();
        }

        // Step 3: Assign to report card
        printReportCard.CoscholasticAreaData = coscholasticAreaData;

        var gradinglist = _lumen.GradingCriterias
              .Where(x =>
               x.BatchID == BatchId
              && x.ClassID == printReportCard.StudentData.ClassID && x.TermID == termId
              ).Select(x => new GradingCriterias
              {
                MaximumPercentage = x.MaximumPercentage,
                MinimumPercentage = x.MinimumPercentage,
                Grade = x.Grade,
                GradeDescription = x.GradeDescription
              })
              .ToList();
        printReportCard.GradingCriteria = gradinglist;
        string termName = _lumen.TblTerms.Where(x => Convert.ToInt32(x.TermId) == termId).Select(t => t.TermName).FirstOrDefault() ?? "";

        if (GroupedSubjects == null || !GroupedSubjects.Any())
        {
          printReportCard.GroupedSubjects = null;
          printReportCard.Result = "";
        }
        else
        {
          printReportCard.Result = "Pass";
        }
        if (new[] { 205, 206, 614, 615, 416, 417, 414, 415 }.Contains(studentData.ClassID))
        {
          bool failed = false;
          foreach (var subject in printReportCard.GroupedSubjects ?? new List<GroupedSubjects>())
          {
            foreach (var term in subject.Terms)
            {
              if (term.TheoryMark <
                  records.First(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() != "PRACTICAL").MinimumMarks
                  || term.PracticalMark <
                  records.FirstOrDefault(x => x.SubjectName == subject.SubjectName && x.TestType.ToUpper() == "PRACTICAL")?.MinimumMarks)
              {
                failed = true;
                break;
              }
            }
            if (failed) break;
          }

          if (failed)
            printReportCard.Result = ""; // Blank
        }

        if (printReportCard.GroupedSubjects != null && printReportCard.GroupedSubjects.Any(g => g.Terms != null && g.Terms.Any(t => t.Grade == "D" || t.Grade == "E")))
        {
          printReportCard.Result = "";
        }

        return printReportCard;
      }
    }
    else
    {
      var printReportCard = new PrintReportCardData();
      return printReportCard;
    }

  }
  public async Task<List<PrintReportCardData>> GetReportCardDataByClass_oldAsync(int termId, int BatchId, int classId, int sectionId)
  {
    // SQL query to get distinct student IDs based on the provided parameters
    var studentIdsQuery = @"
 SELECT DISTINCT s.StudentId
 FROM Tbl_TestRecords tr
 JOIN Students s ON tr.StudentID = s.StudentId
 WHERE s.IsApplyforTC = 0 and s.Class_Id=@ClassId and Section_Id=@SectionId order by s.StudentId";

    var studentIds = new List<int>();

    // Parameters for the studentIds query
    var studentIdsParameters = new[]
    {
          new SqlParameter("@BatchId", BatchId),
          new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
          new SqlParameter("@TermId", termId),
          new SqlParameter("@SectionId", sectionId)
      };

    var connectionString = _lumen.Database.GetConnectionString();
    var schoolDetails = _lumen.TblCreateSchools
               .Select(x => new
               {
                 x.SchoolName,
                 x.Address,
                 x.CurrentYear,
                 x.UploadImage,


               })
               .FirstOrDefault();

    var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
       ? "/Content/Default/default-logo.jpeg"
       : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";
    Console.WriteLine($"SchoolLogo Path: {schoolLogoPath}");

    var printReportCards = new List<PrintReportCardData>();
    using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
    {
      await connection.OpenAsync();

      // ... (studentIdsQuery execution remains the same) ...
      using (var command = connection.CreateCommand())
      {
        command.CommandText = studentIdsQuery;
        command.CommandType = CommandType.Text;
        command.Parameters.AddRange(studentIdsParameters);

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            studentIds.Add(reader.GetInt32(0)); // Add the StudentId to the list
          }
        }
      }

      // Ensure distinct student IDs
      studentIds = studentIds.Distinct().ToList();
      //Fetch grading criteria
      var gradingCriteriaQuery = @"
            SELECT MaximumPercentage, MinimumPercentage, Grade, GradeDescription
            FROM GradingCriterias
            WHERE BatchID = @BatchId AND ClassID = @ClassId";
      var gradingCriteria = new List<GradingCriterias>();

      // ... (gradingCriteria fetching remains the same) ...
      using (var command = connection.CreateCommand())
      {
        command.CommandText = gradingCriteriaQuery;
        command.CommandType = CommandType.Text;
        command.Parameters.Add(new SqlParameter("@BatchId", BatchId));
        command.Parameters.Add(new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId));

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            gradingCriteria.Add(new GradingCriterias
            {
              MaximumPercentage = reader.GetDecimal(reader.GetOrdinal("MaximumPercentage")),
              MinimumPercentage = reader.GetDecimal(reader.GetOrdinal("MinimumPercentage")),
              Grade = reader["Grade"].ToString(),
              GradeDescription = reader["GradeDescription"].ToString()
            });
          }
        }
      }
      // Execute the stored procedure to fetch the report card data
      var commandText = "EXEC GetReportCardbyClass @Batch_Id, @ClassId, @SectionId, @TermId";

      // Parameters for the report card stored procedure
      var reportCardParameters = new[]
      {
            new SqlParameter("@Batch_Id", BatchId),
            new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
            new SqlParameter("@TermId", termId),
            new SqlParameter("@SectionId", sectionId)
        };

      using (var command = connection.CreateCommand())
      {
        command.CommandText = commandText;
        command.CommandType = CommandType.Text;
        command.Parameters.Clear();
        command.Parameters.AddRange(reportCardParameters);

        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        {
          var studentDataList = new Dictionary<int, StudentData>();

          // Read the student data (only once)
          while (await reader.ReadAsync())
          {
            var studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
            if (!studentDataList.ContainsKey(studentId))
            {
              studentDataList[studentId] = new StudentData
              {
                StudentID = studentId,
                StudentName = reader["StudentName"].ToString() ?? "",
                FatherName = reader["FatherName"].ToString() ?? "",
                MotherName = reader["MotherName"].ToString() ?? "",
                ScholarNo = reader["ScholarNo"].ToString() ?? "",
                RollNo = reader["RollNo"].ToString() ?? "",
                //DateOfBirth = reader["DateOfBirth"].ToString() ?? "",
                DateOfBirth = reader["DateOfBirth"] == DBNull.Value || reader["DateOfBirth"] == null ? "" : TryParseDate(reader["DateOfBirth"].ToString()),

                AcademicYear = reader["AcademicYear"].ToString() ?? "",
                ClassName = reader["ClassName"].ToString() ?? "",
                SectionName = reader["SectionName"].ToString() ?? "",
              //  Attendance = float.Parse(reader["Attendance"].ToString() ?? "0"), // Handle potential nulls
                PromotedClass = reader["PromotedClass"].ToString() ?? "",
                StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "",
                Remark = reader["Remark"].ToString() ?? "",
                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                SchoolLogo = schoolLogoPath,
                SchoolName = schoolDetails?.SchoolName,
                //newAddress = schoolDetails?.Address,
                CurrentYear = schoolDetails?.CurrentYear,
                BatchID = BatchId,
                TermID = termId,
                SectionID = sectionId

              };
            }
          }

          // Now fetch the marks and terms for each student
          if (await reader.NextResultAsync())
          {
            var studentMarksList = new List<TermSubjectMarks>();

            while (await reader.ReadAsync())
            {
              var studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
              var testId = reader.GetInt64(reader.GetOrdinal("TestID"));
              var termName = reader["Term"].ToString() ?? "";
              var subjectName = reader["Subject"].ToString() ?? "";
              var obtainedMarks = decimal.Parse(reader["ObtainedMarks"].ToString() ?? "0");
              var maximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? "0");
              var grade = reader["Grade"].ToString() ?? "";
              var isOptional = bool.Parse(reader["IsOptional"].ToString() ?? "False");

              studentMarksList.Add(new TermSubjectMarks
              {
                Studentid = studentId, // Add StudentId to TermSubjectMarks
                TestID = testId,
                TermName = termName,
                SubjectName = subjectName,
                Mark = obtainedMarks,
                TestType = reader["TestType"].ToString() ?? "",
                MaximumMarks = maximumMarks,
                Grade = grade,
                IsOptional = isOptional
              });
            }

            // Now process the term subject marks for each student
            foreach (var studentId in studentIds)
            {
              if (!studentDataList.ContainsKey(studentId))
              {
                // Optional: log or skip silently
                continue; // or handle accordingly
              }

              var studentData = studentDataList[studentId];
              var studentMarks = studentMarksList.Where(x => x.Studentid == studentId).ToList();

              var groupedSubjects = studentMarks.GroupBy(r => r.SubjectName).Select(g => new GroupedSubjects
              {
                SubjectName = g.Key,
                Terms = g.Where(x => x.TestType.ToUpper() != "PRACTICAL")
                           .Select(t => new SubjectTermRecord
                           {
                             Name = t.TermName,
                             TheoryMark = t.Mark,
                             PracticalMark = studentMarks.FirstOrDefault(p => p.SubjectName == g.Key && p.TermName == t.TermName && p.TestType.ToUpper() == "PRACTICAL")?.Mark ?? 0,
                             MaximumMarks = t.MaximumMarks,
                             TotallMark = t.Mark + (studentMarks.FirstOrDefault(p => p.SubjectName == g.Key && p.TermName == t.TermName && p.TestType.ToUpper() == "PRACTICAL")?.Mark ?? 0),
                             Grade = t.Grade
                           }).ToList()
              }).ToList();
              var groupedTerms = studentMarks.Where(r => r.IsOptional != true)
                                            .GroupBy(r => new { r.TermName, r.TestType })
                                            .Select(tr => new GroupedTerms
                                            {
                                              Term = tr.Key.TermName,
                                              TestType = tr.Key.TestType,
                                              Total = tr.Sum(x => x.Mark),
                                              MaximumMarks = tr.FirstOrDefault()?.MaximumMarks ?? 0,
                                              Percentage = (tr.Sum(x => x.Mark) / (tr.FirstOrDefault()?.MaximumMarks ?? 1) * 100),
                                              Grade = GetGrade((tr.Sum(x => x.Mark) / (tr.FirstOrDefault()?.MaximumMarks ?? 1) * 100), studentData.ClassID,termId,BatchId)
                                            }).ToList();
              var terms = studentMarks.Select(x => x.TermName).Distinct().ToList();
              //var coscholasticResult = (from c in _lumen.TblCoScholasticClasses
              //                          join cr in _lumen.TblCoScholastics on c.CoscholasticId equals cr.Id
              //                          where c.ClassId == studentData.ClassID
              //                          select new
              //                          {
              //                            CoscholasticID = cr.Id,
              //                            Title = cr.Title,
              //                            ObtainedGrade = "A",
              //                            Term = _lumen.TblTerms.Where(x => x.TermId == termId).Select(x => x.TermName).FirstOrDefault()
              //                          }).ToList();
              // var firstCoSchResult = _lumen.TblCoScholasticResults.Where(c => c.ClassId == studentData.ClassID && c.TermId == termId).OrderBy(c => c.Id).FirstOrDefault();
              var firstCoSchResult = _lumen.TblCoScholasticResults
            .Where(c => c.ClassId == studentData.ClassID && c.SectionId==studentData.SectionID&&
                        c.TermId == termId &&c.StudentId== studentData.StudentID)
            .OrderBy(c => c.Id)
            .LastOrDefault();

              List<CoscholasticAreaDatas> coscholasticAreaData = new();

              if (firstCoSchResult != null)
              {
                var coscholasticResult = (from cog in _lumen.TblCoScholasticObtainedGrades
                                          join cr in _lumen.TblCoScholastics on cog.CoscholasticId equals cr.Id into crJoin
                                          from cr in crJoin.DefaultIfEmpty()
                                          where cog.ObtainedCoScholasticId == firstCoSchResult.Id &&
                                                cog.batchId == BatchId
                                          select new
                                          {
                                            CoscholasticID = cog.Id,
                                            Title = cr.Title,
                                            ObtainedGrade = cog.ObtainedGrade,
                                            Term = _lumen.TblTerms
                                                  .Where(x => x.TermId == termId)
                                                  .Select(x => x.TermName)
                                                  .FirstOrDefault()
                                          }).ToList();

                coscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
                {
                  Name = item.Title,
                  ObtainedGrade = item.ObtainedGrade,
                  Term = item.Term
                }).ToList();
              }

              var printReportCard = new PrintReportCardData
              {
                StudentData = studentData,
                GroupedSubjects = groupedSubjects,
                GroupedTerms = groupedTerms,
                Term = terms,
                GradingCriteria = gradingCriteria,
                CoscholasticAreaData= coscholasticAreaData,
                //CoscholasticAreaData = coscholasticAreaData.Select(item => new CoscholasticAreaDatas
                //{
                //  Name = item.Title ?? "",
                //  ObtainedGrade = item.ObtainedGrade,
                //  Term = item.Term ?? ""
                //}).ToList(),
                Result = "Pass"
              };

              printReportCards.Add(printReportCard);
            }
          }
        }
      }
    }

    return printReportCards;
  }

  public async Task<List<PrintReportCardData>> GetReportCardDataByClassAsync(int termId, int BatchId, int classId, int sectionId)
  {
    var studentIdsQuery = @"
    SELECT DISTINCT s.StudentId
    FROM Tbl_TestRecords tr
    JOIN Students s ON tr.StudentID = s.StudentId
    WHERE s.IsApplyforTC = 0 AND tr.ClassID = @ClassId AND SectionID = @SectionId
    ORDER BY s.StudentId";

    var studentIds = new List<int>();
    var studentIdsParameters = new[]
    {
        new SqlParameter("@BatchId", BatchId),
        new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
        new SqlParameter("@TermId", termId),
        new SqlParameter("@SectionId", sectionId)
    };

    var connectionString = _lumen.Database.GetConnectionString();
    var schoolDetails = _lumen.TblCreateSchools
        .Select(x => new
        {
          x.SchoolName,
          x.Address,
          x.CurrentYear,
          x.UploadImage
        }).FirstOrDefault();

    var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
        ? "/Content/Default/default-logo.jpeg"
        : $"/Content/SchoolImages/{Uri.UnescapeDataString(schoolDetails.UploadImage)}";

    var printReportCards = new List<PrintReportCardData>();

    using (var connection = new SqlConnection(connectionString))
    {
      await connection.OpenAsync();

      // Get student IDs
      using (var command = connection.CreateCommand())
      {
        command.CommandText = studentIdsQuery;
        command.CommandType = CommandType.Text;
        command.Parameters.AddRange(studentIdsParameters);

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            studentIds.Add(reader.GetInt32(0));
          }
        }
      }

      studentIds = studentIds.Distinct().ToList();

      // Fetch grading criteria
      var gradingCriteria = new List<GradingCriterias>();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = @"SELECT MaximumPercentage, MinimumPercentage, Grade, GradeDescription
                                    FROM GradingCriterias
                                    WHERE BatchID = @BatchId AND ClassID = @ClassId";
        command.CommandType = CommandType.Text;
        command.Parameters.Add(new SqlParameter("@BatchId", BatchId));
        command.Parameters.Add(new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId));

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            gradingCriteria.Add(new GradingCriterias
            {
              MaximumPercentage = reader.GetDecimal(0),
              MinimumPercentage = reader.GetDecimal(1),
              Grade = reader["Grade"].ToString(),
              GradeDescription = reader["GradeDescription"].ToString()
            });
          }
        }
      }

      // Fetch all report data
      var reportCardParameters = new[]
      {
            new SqlParameter("@Batch_Id", BatchId),
            new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
            new SqlParameter("@TermId", termId),
            new SqlParameter("@SectionId", sectionId)
        };

      using (var command = connection.CreateCommand())
      {
        command.CommandText = "EXEC GetReportCardbyClass @Batch_Id, @ClassId, @SectionId, @TermId";
        command.CommandType = CommandType.Text;
        command.Parameters.AddRange(reportCardParameters);

        using (var reader = await command.ExecuteReaderAsync())
        {
          var studentDataList = new Dictionary<int, StudentData>();
          var studentMarksList = new List<TermSubjectMarks>();

          while (await reader.ReadAsync())
          {
            var studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
            if (!studentDataList.ContainsKey(studentId))
            {
              studentDataList[studentId] = new StudentData
              {
                StudentID = studentId,
                StudentName = reader["StudentName"].ToString() ?? "",
                FatherName = reader["FatherName"].ToString() ?? "",
                MotherName = reader["MotherName"].ToString() ?? "",
                ScholarNo = reader["ScholarNo"].ToString() ?? "",
                RollNo = reader["RollNo"].ToString() ?? "",
                DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? "" : TryParseDate(reader["DateOfBirth"].ToString()),
                AcademicYear = reader["AcademicYear"].ToString() ?? "",
                ClassName = reader["ClassName"].ToString() ?? "",
                SectionName = reader["SectionName"].ToString() ?? "",
                //Attendance = Attendance(BatchId, classId, studentId, sectionId) ,//(reader["Attendance"].ToString()) ?? "",
                PromotedClass = reader["PromotedClass"].ToString() ?? "",
                StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "",
                Remark = reader["Remark"].ToString() ?? "",
                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                SectionID = sectionId,
                SchoolLogo = schoolLogoPath,
                SchoolName = schoolDetails?.SchoolName,
                CurrentYear = schoolDetails?.CurrentYear,
                BatchID = BatchId,
                TermID = termId
              };
            }
          }

          if (await reader.NextResultAsync())
          {
            while (await reader.ReadAsync())
            {
              studentMarksList.Add(new TermSubjectMarks
              {
                Studentid = reader.GetInt64(reader.GetOrdinal("StudentId")),
                TestID = reader.GetInt64(reader.GetOrdinal("TestID")),
                TermName = reader["Term"].ToString() ?? "",
                SubjectName = reader["Subject"].ToString() ?? "",
                Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? ""),
                TestType = reader["TestType"].ToString() ?? "",
                MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? ""),
                Grade = reader["Grade"].ToString() ?? "",
               // TestType = reader["TestType"].ToString() ?? "",
                IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "")
              });
            }

            foreach (var studentId in studentIds)
            {
              if (!studentDataList.TryGetValue(studentId, out var studentData))
                continue;

              var studentMarks = studentMarksList.Where(x => x.Studentid == studentId).ToList();

              var groupedSubjects = studentMarks
                  .GroupBy(r => r.SubjectName)
                  .Select(g => new GroupedSubjects
                  {
                    SubjectName = g.Key,
                    IsOptional = g.FirstOrDefault()?.IsOptional ?? false,
                    Terms = g.Where(x => x.TestType.ToUpper() != "PRACTICAL")
                          .Select(t =>
                          {
                            var practical = g.FirstOrDefault(p =>
                                          p.SubjectName == g.Key &&
                                          p.TermName == t.TermName &&
                                          p.TestType.ToUpper() == "PRACTICAL");

                            var practicalMark = practical?.Mark ?? 0;
                            var practicalMax = practical?.MaximumMarks ?? 0;

                            var totalMark = t.Mark + practicalMark;
                            var maxMark = t.MaximumMarks + practicalMax;

                            var percent = maxMark > 0 ? Math.Round((totalMark / maxMark) * 100, 2) : 0;
                            var finalGrade = t.IsOptional ? t.Grade : GetGrade(percent, studentData.ClassID, termId, BatchId);

                            return new SubjectTermRecord
                            {
                              Name = t.TermName,
                              TheoryMark = t.Mark,
                              PracticalMark = practicalMark,
                              MaximumMarks = maxMark,
                              TotallMark = totalMark,
                              Grade = finalGrade,
                              IsOptional = t.IsOptional
                            };
                          }).ToList()
                  }).ToList();

              var groupedTerms = studentMarks
                  .Where(x => !x.IsOptional)
                  .GroupBy(x => new { x.TermName, x.TestType })
                  .Select(gr =>
                  {
                    var total = gr.Sum(m => m.Mark);
                    var max = gr.Sum(m => m.MaximumMarks);
                    var perc = max > 0 ? Math.Round((total / max) * 100, 2) : 0;

                    return new GroupedTerms
                    {
                      Term = gr.Key.TermName,
                      TestType = gr.Key.TestType,
                      Total = total,
                      MaximumMarks = max,
                      Percentage = perc,
                      Grade = GetGrade(perc, studentData.ClassID, termId, BatchId)
                    };
                  }).ToList();

              var terms = studentMarks.Select(x => x.TermName).Distinct().ToList();

              var coSchResult = _lumen.TblCoScholasticResults
                  .Where(c => c.ClassId == studentData.ClassID &&
                              c.SectionId == studentData.SectionID &&
                              c.TermId == termId &&
                              c.StudentId == studentId)
                  .OrderBy(c => c.Id)
                  .LastOrDefault();

              List<CoscholasticAreaDatas> coData = new();
              string termName = _lumen.TblTerms
                      .Where(x => x.TermId == termId)
                      .Select(x => x.TermName)
                      .FirstOrDefault() ?? "";
              if (coSchResult != null)
              {
                coData = (from cog in _lumen.TblCoScholasticObtainedGrades
                          join cr in _lumen.TblCoScholastics on cog.CoscholasticId equals cr.Id
                          where cog.ObtainedCoScholasticId == coSchResult.Id &&
                                cog.batchId == BatchId
                          select new CoscholasticAreaDatas
                          {
                            Name = cr.Title??"",
                            ObtainedGrade = cog.ObtainedGrade??"",
                            Term = termName ?? ""
                          }).ToList();
              }

              printReportCards.Add(new PrintReportCardData
              {
                StudentData = studentData,
                GroupedSubjects = groupedSubjects,
                GroupedTerms = groupedTerms,
                Term = terms,
                GradingCriteria = gradingCriteria,
                CoscholasticAreaData = coData,
                Result = "Pass"
              });
            }
          }
        }
      }
    }

    return printReportCards;
  }
  private string TryParseDate(string? dateStr)
  {
    if (string.IsNullOrWhiteSpace(dateStr))
      return "";

    string[] formats = { "dd/MM/yyyy", "dd-MMM-yyyy", "d-MMM-yyyy", "d/MM/yyyy" };

    if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
    {
      return dt.ToString("dd/MM/yyyy");
    }

    // Fallback: try general parse
    if (DateTime.TryParse(dateStr, out dt))
    {
      return dt.ToString("dd/MM/yyyy");
    }

    return "";
  }




  //public async Task<List<PrintReportCardData>> GetReportCardDataByClassAsync(int termId, int BatchId, int classId, int sectionId)
  //{
  //  var commandText = "EXEC GetReportCardbyClass @Batch_Id, @ClassId, @SectionId, @TermId";
  //  var parameters = new[]
  //  {
  //      new SqlParameter("@Batch_Id", BatchId),
  //      new SqlParameter("@TermId", termId),
  //      new SqlParameter("@ClassId", classId),
  //      new SqlParameter("@SectionId", sectionId)
  //  };

  //  var reportCardList = new List<PrintReportCardData>();
  //  var connectionString = _lumen.Database.GetConnectionString();
  //  var schoolDetails = _lumen.TblCreateSchools.FirstOrDefault();

  //  var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
  //      ? "/Content/Default/default-logo.jpeg"
  //      : $"/Content/SchoolImages/{Uri.EscapeDataString(schoolDetails.UploadImage)}";

  //  using (var connection = new SqlConnection(connectionString))
  //  {
  //    await connection.OpenAsync();
  //    using (var command = new SqlCommand(commandText, connection))
  //    {
  //      command.Parameters.AddRange(parameters);
  //      using (var reader = await command.ExecuteReaderAsync())
  //      {
  //        while (await reader.ReadAsync())
  //        {
  //          var studentData = new StudentData
  //          {
  //            BatchID = BatchId,
  //            StudentID = int.Parse(reader["StudentID"].ToString() ?? "0"),
  //            TermID = termId,
  //            SectionID = sectionId,
  //            StudentName = reader["StudentName"].ToString() ?? "",
  //            FatherName = reader["FatherName"].ToString() ?? "",
  //            MotherName = reader["MotherName"].ToString() ?? "",
  //            RollNo = reader["RollNo"].ToString() ?? "",
  //            DateOfBirth = reader["DateOfBirth"].ToString() ?? "",
  //            ClassID = classId,
  //            SchoolName = schoolDetails?.SchoolName,
  //            newAddress = schoolDetails?.Address,
  //            CurrentYear = schoolDetails?.CurrentYear,
  //            SchoolLogo = schoolLogoPath
  //          };

  //          var records = new List<TermSubjectMarks>();

  //          if (await reader.NextResultAsync())
  //          {
  //            while (await reader.ReadAsync())
  //            {
  //              records.Add(new TermSubjectMarks
  //              {
  //                TestID = Convert.ToInt32(reader["TestID"]),
  //                TermName = reader["Term"].ToString() ?? "",
  //                SubjectName = reader["Subject"].ToString() ?? "",
  //                Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? "0"),
  //                TestType = reader["TestType"].ToString() ?? "",
  //                MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? "0"),
  //                Grade = reader["Grade"].ToString() ?? "",
  //                IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "false")
  //              });
  //            }
  //          }

  //          var groupedSubjects = records.GroupBy(r => r.SubjectName).Select(group => new GroupedSubjects
  //          {
  //            SubjectName = group.Key,
  //            IsOptional = group.FirstOrDefault()?.IsOptional ?? false,
  //            Terms = group.Select(item => new SubjectTermRecord
  //            {
  //              Name = item.TermName,
  //              TheoryMark = item.Mark,
  //              PracticalMark = group.Where(x => x.SubjectName == item.SubjectName && x.TestType.ToUpper() == "PRACTICAL")
  //                    .Select(x => x.Mark).FirstOrDefault(),
  //              MaximumMarks = item.MaximumMarks,
  //              TotallMark = item.Mark,
  //              Grade = item.Grade,
  //              IsOptional = item.IsOptional
  //            }).ToList()
  //          }).ToList();

  //          var groupedTermsRecords = records.Where(r => !r.IsOptional)
  //              .GroupBy(r => new { r.TermName, r.TestType })
  //              .Select(group => new GroupedTerms
  //              {
  //                Term = group.Key.TermName,
  //                TestType = group.Key.TestType,
  //                Total = group.Sum(x => x.Mark),
  //                MaximumMarks = group.First().MaximumMarks,
  //                Percentage = Math.Round(group.Sum(x => x.Mark) / (group.First().MaximumMarks * group.Count()) * 100, 2),
  //                Grade = GetGrade(group.Sum(x => x.Mark) / group.First().MaximumMarks * 100, classId)
  //              }).ToList();

  //          var coscholasticRecords = (from c in _lumen.TblCoScholasticClasses
  //                                     join cr in _lumen.TblCoScholastics on c.CoscholasticId equals cr.Id
  //                                     where c.ClassId == classId
  //                                     select new CoscholasticAreaDatas
  //                                     {
  //                                       Name = cr.Title ?? "",
  //                                       ObtainedGrade = "A", // Mock data for now
  //                                       Term = _lumen.TblTerms.FirstOrDefault(x => x.TermId == termId)?.TermName ?? ""
  //                                     }).ToList();
  //          var cosch = from c in _lumen.TblCoScholasticClasses
  //                      join cr in _lumen.TblCoScholastics on c.CoscholasticId equals cr.Id
  //                      where c.ClassId == classId
  //                      select cr;

  //          var coscholasticResult = (from c in cosch

  //                                    select new
  //                                    {
  //                                      CoscholasticID = c.Id,
  //                                      Title = c.Title,
  //                                      ObtainedGrade = cog.ObtainedGrade,
  //                                      ObtainedGrade = "A",
  //                                      Term = _lumen.TblTerms.Where(x => x.TermId == termId).Select(x => x.TermName).FirstOrDefault()
  //                                    }).ToList();
  //          printReportCard.CoscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
  //          {
  //            Name = item.Title,
  //            ObtainedGrade = item.ObtainedGrade,
  //            Term = item.Term
  //          }).ToList();

  //          var gradingList = _lumen.GradingCriterias.Where(x => x.BatchID == BatchId && x.ClassID == classId)
  //              .Select(x => new GradingCriterias
  //              {
  //                MaximumPercentage = x.MaximumPercentage,
  //                MinimumPercentage = x.MinimumPercentage,
  //                Grade = x.Grade,
  //                GradeDescription = x.GradeDescription
  //              }).ToList();

  //          var printReportCard = new PrintReportCardData
  //          {
  //            StudentData = studentData,
  //            GroupedSubjects = groupedSubjects,
  //            GroupedTerms = groupedTermsRecords,
  //            Term = records.Select(x => x.TermName).Distinct().ToList(),
  //            CoscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
  //            {
  //              Name = item.Title,
  //              ObtainedGrade = item.ObtainedGrade,
  //              Term = item.Term
  //            }).ToList(),
  //            GradingCriteria = gradingList,
  //            Result = "Pass"
  //          };

  //          reportCardList.Add(printReportCard);
  //        }
  //      }
  //    }
  //  }

  //  return reportCardList;
  //}




  #region New One
  //public async Task<List<PrintReportCardData>> GetReportCardDataByClassAsync(int termId, int BatchId, int classId, int sectionId)
  //{
  //  // SQL query to get distinct student IDs based on the provided parameters
  //  var studentIdsQuery = @"
  //      SELECT DISTINCT s.StudentId
  //      FROM Tbl_TestRecords tr
  //      JOIN Students s ON tr.StudentID = s.StudentId
  //      WHERE s.IsApplyforTC = 0
  //      AND (
  //          (@ClassId = 0 AND @SectionId = 0) -- No filters for class and section
  //          OR (tr.ClassID = @ClassId AND tr.SectionID = @SectionId) -- Filter by class and section
  //      )
  //      AND (
  //          (@BatchId = 0) -- No filter for batch
  //          OR tr.BatchId = @BatchId -- Filter by batch
  //      )
  //      AND (
  //          (@TermId != 10 AND tr.TermID = @TermId) -- Filter by term if term is not 10
  //          OR (@TermId = 10) -- If termId is 10, include all records
  //      )";

  //  var studentIds = new List<int>();

  //  // Parameters for the studentIds query
  //  var studentIdsParameters = new[]
  //  {
  //      new SqlParameter("@BatchId", BatchId),
  //      new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
  //      new SqlParameter("@TermId", termId),
  //      new SqlParameter("@SectionId", sectionId)
  //  };

  //  var connectionString = _lumen.Database.GetConnectionString();
  //  var schoolDetails = _lumen.TblCreateSchools
  //      .Select(x => new
  //      {
  //        x.SchoolName,
  //        x.Address,
  //        x.CurrentYear,
  //        x.UploadImage,
  //      })
  //      .FirstOrDefault();

  //  var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
  //      ? "/Content/Default/default-logo.jpeg"
  //      : $"/Content/SchoolImages/{Uri.EscapeDataString(schoolDetails.UploadImage)}";

  //  var printReportCards = new List<PrintReportCardData>();

  //  using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
  //  {
  //    await connection.OpenAsync();

  //    // Execute the SQL query to get distinct student IDs
  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = studentIdsQuery;
  //      command.CommandType = CommandType.Text;
  //      command.Parameters.AddRange(studentIdsParameters);

  //      using (var reader = await command.ExecuteReaderAsync())
  //      {
  //        while (await reader.ReadAsync())
  //        {
  //          studentIds.Add(reader.GetInt32(0)); // Add the StudentId to the list
  //        }
  //      }
  //    }

  //    // Ensure distinct student IDs
  //    studentIds = studentIds.Distinct().ToList();

  //    // Fetch grading criteria
  //    var gradingCriteriaQuery = @"
  //          SELECT MaximumPercentage, MinimumPercentage, Grade, GradeDescription
  //          FROM GradingCriterias
  //          WHERE BatchID = @BatchId AND ClassID = @ClassId";
  //    var gradingCriteria = new List<GradingCriterias>();

  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = gradingCriteriaQuery;
  //      command.CommandType = CommandType.Text;
  //      command.Parameters.Add(new SqlParameter("@BatchId", BatchId));
  //      command.Parameters.Add(new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId));

  //      using (var reader = await command.ExecuteReaderAsync())
  //      {
  //        while (await reader.ReadAsync())
  //        {
  //          gradingCriteria.Add(new GradingCriterias
  //          {
  //            MaximumPercentage = reader.GetDecimal(reader.GetOrdinal("MaximumPercentage")),
  //            MinimumPercentage = reader.GetDecimal(reader.GetOrdinal("MinimumPercentage")),
  //            Grade = reader["Grade"].ToString(),
  //            GradeDescription = reader["GradeDescription"].ToString()
  //          });
  //        }
  //      }
  //    }

  //    // Fetch report card data for all students at once using the stored procedure
  //    var commandText = "EXEC GetReportCardbyClass @Batch_Id, @ClassId, @SectionId, @TermId";

  //    // Parameters for the report card stored procedure
  //    var reportCardParameters = new[]
  //    {
  //          new SqlParameter("@Batch_Id", BatchId),
  //          new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
  //          new SqlParameter("@TermId", termId),
  //          new SqlParameter("@SectionId", sectionId)
  //      };

  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = commandText;
  //      command.CommandType = CommandType.Text;
  //      command.Parameters.Clear();
  //      command.Parameters.AddRange(reportCardParameters);

  //      using (SqlDataReader reader = await command.ExecuteReaderAsync())
  //      {
  //        var studentDataList = new Dictionary<int, StudentData>();

  //        // Read the student data (only once)
  //        while (await reader.ReadAsync())
  //        {
  //          var studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
  //          if (!studentDataList.ContainsKey(studentId))
  //          {
  //            studentDataList[studentId] = new StudentData
  //            {
  //              StudentID = studentId,
  //              StudentName = reader["StudentName"].ToString() ?? "",
  //              FatherName = reader["FatherName"].ToString() ?? "",
  //              MotherName = reader["MotherName"].ToString() ?? "",
  //              ScholarNo = reader["ScholarNo"].ToString() ?? "",
  //              RollNo = reader["RollNo"].ToString() ?? "",
  //              DateOfBirth = reader["DateOfBirth"].ToString() ?? "",
  //              AcademicYear = reader["AcademicYear"].ToString() ?? "",
  //              ClassName = reader["ClassName"].ToString() ?? "",
  //              SectionName = reader["SectionName"].ToString() ?? "",
  //              Attendance = 0, //float.Parse(reader["Attendance"].ToString()),
  //              PromotedClass = reader["PromotedClass"].ToString() ?? "",
  //              StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "",
  //              Remark = reader["Remark"].ToString() ?? "",
  //              ClassID = reader.GetInt32(reader.GetOrdinal("ClassID"))
  //            };
  //          }
  //        }

  //        // Now fetch the marks and terms for each student
  //        if (await reader.NextResultAsync())
  //        {
  //          var subjectMarksList = new List<TermSubjectMarks>();

  //          while (await reader.ReadAsync())
  //          {
  //            var testId = reader.GetInt64(reader.GetOrdinal("TestID"));
  //            var termName = reader["Term"].ToString() ?? "";
  //            var subjectName = reader["Subject"].ToString() ?? "";
  //            var obtainedMarks = 0; //decimal.Parse(reader["ObtainedMarks"].ToString());
  //            var maximumMarks = 0; //decimal.Parse(reader["MaximumMarks"].ToString());
  //            var grade = reader["Grade"].ToString();
  //            var isOptional = false;//bool.Parse(reader["IsOptional"].ToString());

  //            subjectMarksList.Add(new TermSubjectMarks
  //            {
  //              TestID = testId,
  //              TermName = termName,
  //              SubjectName = subjectName,
  //              Mark = obtainedMarks,
  //              TestType = reader["TestType"].ToString() ?? "",
  //              MaximumMarks = maximumMarks,
  //              Grade = grade ?? "",
  //              IsOptional = isOptional
  //            });
  //          }

  //          // Now process the term subject marks for each student
  //          foreach (var studentId in studentIds)
  //          {
  //            var studentData = studentDataList[studentId];
  //            var studentMarks = subjectMarksList.Where(x => x.Studentid == studentId).ToList();

  //            var groupedSubjects = studentMarks.GroupBy(r => r.SubjectName).Select(g => new GroupedSubjects
  //            {
  //              SubjectName = g.Key,
  //              Terms = g.Select(t => new SubjectTermRecord
  //              {
  //                Name = t.TermName,
  //                TheoryMark = t.Mark,
  //                PracticalMark = t.TestType.ToUpper() == "PRACTICAL" ? t.Mark : 0, // Example, should handle better
  //                MaximumMarks = t.MaximumMarks,
  //                TotallMark = t.Mark,
  //                Grade = t.Grade
  //              }).ToList()
  //            }).ToList();

  //            // Now fetch coscholastic data
  //            var coscholasticResult = (from c in _lumen.TblCoScholasticClasses
  //                                      join cr in _lumen.TblCoScholastics on c.CoscholasticId equals cr.Id
  //                                      where c.ClassId == studentData.ClassID
  //                                      select new
  //                                      {
  //                                        CoscholasticID = cr.Id,
  //                                        Title = cr.Title,
  //                                        ObtainedGrade = "A", // Assuming grade "A" for now
  //                                        Term = _lumen.TblTerms.Where(x => x.TermId == termId).Select(x => x.TermName).FirstOrDefault()
  //                                      }).ToList();

  //            // Prepare report card data
  //            var printReportCard = new PrintReportCardData
  //            {
  //              StudentData = studentData,
  //              GroupedSubjects = groupedSubjects,
  //              GradingCriteria = gradingCriteria,
  //              CoscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
  //              {
  //                Name = item.Title ?? "",
  //                ObtainedGrade = item.ObtainedGrade,
  //                Term = item.Term ?? ""
  //              }).ToList(),
  //              Result = "Pass"
  //            };

  //            printReportCards.Add(printReportCard);
  //          }
  //        }
  //      }
  //    }
  //  }

  //  return printReportCards;
  //}
  #endregion
  #region old

  //public async Task<List<PrintReportCardData>> GetReportCardDataByClassAsync(int termId, int BatchId, int classId, int sectionId)
  //{
  //  // SQL query to get student IDs based on the provided parameters
  //  var studentIdsQuery = @"
  //      SELECT DISTINCT s.StudentId
  //      FROM Tbl_TestRecords tr
  //      JOIN Students s ON tr.StudentID = s.StudentId
  //      WHERE s.IsApplyforTC = 0
  //      AND (
  //          (@ClassId = 0 AND @SectionId = 0) -- No filters for class and section
  //          OR (tr.ClassID = @ClassId AND tr.SectionID = @SectionId) -- Filter by class and section
  //      )
  //      AND (
  //          (@BatchId = 0) -- No filter for batch
  //          OR tr.BatchId = @BatchId -- Filter by batch
  //      )
  //      AND (
  //          (@TermId != 10 AND tr.TermID = @TermId) -- Filter by term if term is not 10
  //          OR (@TermId = 10) -- If termId is 10, include all records
  //      )";

  //  var studentIds = new List<int>();

  //  // Parameters for the studentIds query
  //  var studentIdsParameters = new[]
  //  {
  //      new SqlParameter("@BatchId", BatchId),
  //      new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
  //      new SqlParameter("@TermId", termId),
  //      new SqlParameter("@SectionId", sectionId)
  //  };

  //  var connectionString = _lumen.Database.GetConnectionString();
  //  var schoolDetails = _lumen.TblCreateSchools
  //      .Select(x => new
  //      {
  //        x.SchoolName,
  //        x.Address,
  //        x.CurrentYear,
  //        x.UploadImage,
  //      })
  //      .FirstOrDefault();

  //  var schoolLogoPath = string.IsNullOrEmpty(schoolDetails?.UploadImage)
  //      ? "/Content/Default/default-logo.jpeg"
  //      : $"/Content/SchoolImages/{Uri.EscapeDataString(schoolDetails.UploadImage)}";

  //  var printReportCards = new List<PrintReportCardData>();

  //  using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
  //  {
  //    await connection.OpenAsync();

  //    // Execute the SQL query to get student IDs
  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = studentIdsQuery;
  //      command.CommandType = CommandType.Text;
  //      command.Parameters.AddRange(studentIdsParameters);

  //      using (var reader = await command.ExecuteReaderAsync())
  //      {
  //        while (await reader.ReadAsync())
  //        {
  //          studentIds.Add(reader.GetInt32(0)); // Add the StudentId to the list
  //        }
  //      }
  //    }

  //    // Execute the stored procedure to fetch the report card data
  //    var commandText = "EXEC GetReportCardbyClass @Batch_Id, @ClassId, @SectionId, @TermId";

  //    foreach (var studentId in studentIds)
  //    {
  //      var studentData = new StudentData
  //      {
  //        BatchID = BatchId,
  //        StudentID = studentId,
  //        TermID = termId,
  //        SectionID = sectionId,
  //        SchoolName = schoolDetails?.SchoolName,
  //        newAddress = schoolDetails?.Address,
  //        CurrentYear = schoolDetails?.CurrentYear,
  //        SchoolLogo = schoolLogoPath
  //      };

  //      var records = new List<TermSubjectMarks>();

  //      // Parameters for the report card stored procedure
  //      var reportCardParameters = new[]
  //      {
  //              new SqlParameter("@Batch_Id", BatchId),
  //              new SqlParameter("@ClassId", (classId == 0) ? DBNull.Value : (object)classId),
  //              new SqlParameter("@TermId", termId),
  //              new SqlParameter("@SectionId", sectionId)
  //          };

  //      using (var command = connection.CreateCommand())
  //      {
  //        command.CommandText = commandText;
  //        command.CommandType = CommandType.Text;
  //        command.Parameters.Clear();
  //        command.Parameters.AddRange(reportCardParameters);

  //        using (SqlDataReader reader = await command.ExecuteReaderAsync())
  //        {
  //          // Process student data
  //          while (await reader.ReadAsync())
  //          {
  //            studentData.StudentName = reader["StudentName"].ToString() ?? "";
  //            studentData.FatherName = reader["FatherName"].ToString() ?? "";
  //            studentData.MotherName = reader["MotherName"].ToString() ?? "";
  //            studentData.ScholarNo = reader["ScholarNo"].ToString() ?? "";
  //            studentData.RollNo = reader["RollNo"].ToString() ?? "";
  //            studentData.DateOfBirth = reader["DateOfBirth"].ToString() ?? "";
  //            studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
  //            studentData.ClassName = reader["ClassName"].ToString() ?? "";
  //            studentData.SectionName = reader["SectionName"].ToString() ?? "";
  //            studentData.Attendance = float.Parse(reader["Attendance"].ToString() ?? "");
  //            studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
  //            studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
  //            studentData.Remark = reader["Remark"].ToString() ?? "";
  //            studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "");
  //          }

  //          // Process term subject marks
  //          if (await reader.NextResultAsync())
  //          {
  //            while (await reader.ReadAsync())
  //            {
  //              records.Add(new TermSubjectMarks
  //              {
  //                TestID = Convert.ToInt32(reader["TestID"].ToString()),
  //                TermName = reader["Term"].ToString() ?? "",
  //                SubjectName = reader["Subject"].ToString() ?? "",
  //                Mark = decimal.Parse(reader["ObtainedMarks"].ToString() ?? ""),
  //                TestType = reader["TestType"].ToString() ?? "",
  //                MaximumMarks = decimal.Parse(reader["MaximumMarks"].ToString() ?? ""),
  //                Grade = (reader["Grade"].ToString() ?? ""),
  //                IsOptional = bool.Parse(reader["IsOptional"].ToString() ?? "")
  //              });
  //            }
  //          }
  //        }
  //      }

  //      // Process grouped subjects and terms (similar to original code)
  //      var grouped_records = records.GroupBy(r => r.SubjectName).ToList();
  //      var GroupedSubjects = new List<GroupedSubjects>();

  //      foreach (var g in grouped_records)
  //      {
  //        var subject = new GroupedSubjects
  //        {
  //          SubjectName = g.Key,
  //          Terms = new List<SubjectTermRecord>(),
  //          IsOptional = g.Select(x => x.IsOptional).FirstOrDefault(),
  //        };

  //        foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
  //        {
  //          var PracticalMark = g.Where(x => x.SubjectName == g.Key
  //              && x.TermName == t.TermName
  //              && x.TestType.ToUpper() == "PRACTICAL")
  //              .Select(x => x.Mark).FirstOrDefault();
  //          var totalMark = t.Mark + PracticalMark;
  //          decimal percentage = Math.Round((int)totalMark / t.MaximumMarks * 100, 2);

  //          var grade = t.IsOptional ? t.Grade : GetGrade(percentage, studentData.ClassID);
  //          subject.Terms.Add(new SubjectTermRecord
  //          {
  //            Name = t.TermName,
  //            TheoryMark = t.Mark,
  //            PracticalMark = PracticalMark,
  //            MaximumMarks = t.MaximumMarks,
  //            TotallMark = t.Mark + PracticalMark,
  //            Grade = grade,
  //            IsOptional = t.IsOptional,
  //          });
  //        }

  //        GroupedSubjects.Add(subject);
  //      }

  //      var grouped_terms_records = new List<GroupedTerms>();
  //      var grouped_by_term_records = records.Where(r => r.IsOptional != true)
  //          .GroupBy(r => new { r.TermName, r.TestType })
  //          .ToList();

  //      foreach (var tr in grouped_by_term_records)
  //      {
  //        var totalMarks = tr.Sum(x => x.Mark);
  //        var maxMarks = tr.FirstOrDefault()?.MaximumMarks;
  //        int count = records.Count(x => x.TermName == tr.Key.TermName && x.TestType == tr.Key.TestType);
  //        decimal _maxMarks = Convert.ToDecimal(maxMarks);
  //        decimal percentage = Math.Round((int)totalMarks / _maxMarks * 100, 2);
  //        var grade = GetGrade(percentage, studentData.ClassID);

  //        grouped_terms_records.Add(new GroupedTerms
  //        {
  //          Term = tr.Key.TermName,
  //          TestType = tr.Key.TestType,
  //          Total = totalMarks,
  //          MaximumMarks = maxMarks ?? 0,
  //          Percentage = (totalMarks / (_maxMarks * count)) * 100,
  //          Grade = grade
  //        });
  //      }

  //      var cosch = from c in _lumen.TblCoScholasticClasses
  //                  join cr in _lumen.TblCoScholastics on c.CoscholasticId equals cr.Id
  //                  where c.ClassId == studentData.ClassID
  //                  select cr;

  //      var coscholasticResult = cosch.Select(item => new
  //      {
  //        CoscholasticID = item.Id,
  //        Title = item.Title,
  //        ObtainedGrade = "A", // Assuming grade "A" for now
  //        Term = _lumen.TblTerms.Where(x => x.TermId == termId).Select(x => x.TermName).FirstOrDefault()
  //      }).ToList();

  //      var printReportCard = new PrintReportCardData
  //      {
  //        StudentData = studentData,
  //        GroupedSubjects = GroupedSubjects,
  //        GroupedTerms = grouped_terms_records,
  //        Term = records.Select(x => x.TermName).Distinct().ToList(),
  //        CoscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
  //        {
  //          Name = item.Title ?? "",
  //          ObtainedGrade = item.ObtainedGrade,
  //          Term = item.Term ?? ""
  //        }).ToList(),
  //        GradingCriteria = _lumen.GradingCriterias
  //              .Where(x => x.BatchID == BatchId && x.ClassID == studentData.ClassID)
  //              .Select(x => new GradingCriterias
  //              {
  //                MaximumPercentage = x.MaximumPercentage,
  //                MinimumPercentage = x.MinimumPercentage,
  //                Grade = x.Grade,
  //                GradeDescription = x.GradeDescription
  //              })
  //              .ToList(),
  //        Result = "Pass"
  //      };

  //      printReportCards.Add(printReportCard);
  //    }
  //  }

  //  return printReportCards;
  //}
  #endregion









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
  public string GetGrade(decimal percentage, int classid, int termid)
  {
    // Query the database to get the appropriate grade
    var grade = _lumen.GradingCriterias
        .Where(g => percentage >= g.MinimumPercentage && percentage <= g.MaximumPercentage && classid == g.ClassID && termid==g.TermID)
        .Select(g => g.Grade)
        .FirstOrDefault();
    return grade ?? "D";
  }
  public string GetGrade(decimal percentage, int classid, int termid, int BatchId)
  {
    if (termid == 10)
    {
      termid = 4;
    }
    var grade = _lumen.GradingCriterias
        .Where(g => percentage >= g.MinimumPercentage && percentage <= g.MaximumPercentage && classid == g.ClassID && termid == g.TermID && BatchId==g.BatchID)
        .Select(g => g.Grade)
        .FirstOrDefault();
    return grade ?? "D";
  }
  public string Attendance(int BatchId, int ClassId, long StudentID, int? SectionId = 0)
  {
    List<TblStudentAttendance> ActualAttendance = new List<TblStudentAttendance>();

    var batch = _lumen.TblBatches.Where(x => x.BatchId == BatchId).FirstOrDefault();
    //  var batchItems = _context.DataListItems.Where(x => x.DataListId == "9" && x.DataListItemName== batch.Batch_Name).FirstOrDefault();
    var attendanceDate = _lumen.TblTestAssignDates.Where(x => x.BatchId == BatchId && x.ClassId == ClassId).FirstOrDefault();
    var StartDate = DateTime.Now; var ToDate = DateTime.Now;
    if (attendanceDate == null)
    {
      StartDate = DateTime.Now;
      ToDate = DateTime.Now;
    }
    else
    {
      StartDate = Convert.ToDateTime(attendanceDate.StartDate);
      ToDate = Convert.ToDateTime(attendanceDate.ToDate);
    }
    ActualAttendance = _lumen.TblStudentAttendances.Where(x => x.StudentRegisterId == StudentID && x.ClassId == ClassId && x.SectionId == SectionId).ToList().Where(x =>
DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date >= StartDate.Date &&
DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date <= ToDate.Date).ToList();
    ActualAttendance = _lumen.TblStudentAttendances.Where(x => x.StudentRegisterId == StudentID && x.ClassId == ClassId && x.SectionId == SectionId && x.BatchId == BatchId).ToList();



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
    //m double totalAttendedDays = attendedDays + (attendedHalfDays / 2);

    int totalAttendedDays = Convert.ToInt32(attendedDays + (attendedHalfDays / 2));
    string Attendance = totalAttendedDays + "/" + ActualAttendance.Count();
    return Attendance;
  }
  public class ReportCardStudentData
  {
    // Student Information
    public string StudentName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public string ScholarNo { get; set; } = string.Empty;
    public string RollNo { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public float Attendance { get; set; }
    public string PromotedClass { get; set; } = string.Empty;
    public string StaffSignatureLink { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public int ClassID { get; set; }
    public int BatchID { get; set; }
    public int TermID { get; set; }
    public int SectionID {  get; set; }

    public string SchoolName {  get; set; } = string.Empty;
    public string newAddress {  get; set; } = string.Empty;
    public string CurrentYear {  get; set; } = string.Empty;
    public string SchoolLogo {  get; set; } = string.Empty;
    // List of TermSubjectMarks for that student
    public List<TermSubjectMarks> TermSubjectMarks { get; set; } = new List<TermSubjectMarks>();
  }
}

