

//ing System.Data.Entity;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Infrastructure.Data.LumenContext;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web.Models;
using LumenApi.Web.Models.PaymentModels.NTTPaymentGateway;
using LumenApi.Web.Models.StudentView;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static LumenApi.Web.Services.PaymentService;

namespace LumenApi.Web.Services;

public class StudentService(Lumen090923Context lumen) : IStudentInterfaces
{
  
  private readonly Lumen090923Context _lumen = lumen;


  public async Task<IApiResponse> GetStudentDetials(string? UserId)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      //var query = await _lumen.Students.Where(x => x.UserId == UserId).FirstOrDefaultAsync();

      await Task.Run(() =>
      {
        var query = from stuReg in _lumen.StudentsRegistrations
                    join student in _lumen.Students 
                    on stuReg.ApplicationNumber equals student.ApplicationNumber
                    //where stuReg.UserId == UserId
                    where string.IsNullOrEmpty(UserId) || stuReg.UserId == UserId
                    select new
                    {
                      student
                    };

        if (query == null)
        {
          res.Data = query!;
          res.Msg = "Record Not Found.";
          res.ResponseCode = "404";
        }
        else
        {
          res.Data = query;
          res.Msg = "Record fetched successfully.";
          res.ResponseCode = "200";
        }
      });




    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;

  }
  //public async Task<IApiResponse> GetStudentDetialsbyClassSection(int classId,int Section)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {

  //    //var query = await _lumen.Students.Where(x => x.UserId == UserId).FirstOrDefaultAsync();

  //    await Task.Run(() =>
  //    {
  //      var query = from stuReg in _lumen.StudentsRegistrations
  //                  join student in _lumen.Students
  //                  on stuReg.ApplicationNumber equals student.ApplicationNumber
  //                  //where stuReg.UserId == UserId
  //                  where student.ClassId== classId && student.SectionId== Section
  //                  select new
  //                  {
  //                    student
  //                  };

  //      if (query == null)
  //      {
  //        res.Data = query!;
  //        res.Msg = "Record Not Found.";
  //        res.ResponseCode = "404";
  //      }
  //      else
  //      {
  //        res.Data = query;
  //        res.Msg = "Record fetched successfully.";
  //        res.ResponseCode = "200";
  //      }
  //    });




  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;

  //}
  public async Task<IApiResponse> GetStudentDetialsbyClassSection(int classId, int Section)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      //var query = await (from stuReg in _lumen.Students
      //                   join student in _lumen.Students
      //                   on stuReg.ApplicationNumber equals student.ApplicationNumber
      //                   where stuReg.ClassId == classId && stuReg.SectionId == Section
      //                   select new
      var query = await _lumen.Students
      .Where(student => student.ClassId == classId && student.SectionId == Section)
      .Select(student => new
      {


                         
                         
                           student.StudentId,
                           student.ApplicationNumber,
                           student.Uin,
                           student.Date,
                           student.Name,
                           student.Class,
                           student.Section,
                           student.Gender,
                           student.AgeInWords,
                           student.Dob,
                           student.Pob,
                           student.Nationality,
                           student.Religion,
                           student.MotherTongue,
                           student.Category,
                           student.BloodGroup,
                           student.MedicalHistory,
                           student.Hobbies,
                           student.Sports,
                           student.OtherDetails,
                           student.ProfileAvatar,
                           student.AddedDate,
                           student.ModifiedDate,
                           student.Ip,
                           student.UserId,
                           student.IsDeleted,
                           student.CreateBy,
                           student.CurrentYear,
                           student.InsertBy,
                           student.MarkForIdentity,
                           student.OtherLanguages,
                           student.Medium,
                           student.Caste,
                           student.Rte,
                           student.AdharNo,
                           student.AdharFile,
                           student.BatchName,
                           student.IsApplyforTc,
                           student.IsApplyforAdmission,
                           student.IsApprove,
                           student.IsActive,
                           student.IsInsertFromAd,
                           student.IsAdmissionPaid,
                           student.RegNumber,
                           student.ClassId,
                           student.CategoryId,
                           student.BatchId,
                           student.ParentEmail,
                           student.AdmissionFeePaid,
                           student.LastName,
                           student.Transport,
                           student.TransportOptions,
                           student.Mobile,
                           student.City,
                           student.State,
                           student.Pincode,
                           student.BloodGroupId,
                           student.IsPromoted,
                           student.SectionId,
                           student.RollNo,
                           student.ScholarNo,
                           student.AdditionalInformations,
                           student.GuardianDetails,
                           student.PastSchoolingReports,
                           student.StudentRemoteAccesses,
                           student.StudentTcDetails,
                           student.TcFeeDetails
                         }).ToListAsync();

      if (!query.Any())
      {
        res.Data = "";
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = query;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> GetStudentResultsByClassSection(int StudentId, long BatchId)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      //var query = await _lumen.Students.Where(x => x.UserId == UserId).FirstOrDefaultAsync();

      await Task.Run(() =>
      {
        var result = from tr in _lumen.TblTestRecords
                     where tr.BatchId == BatchId
        && tr.StudentId == StudentId
                           && _lumen.Tbl_PublishDetail.Any(pd => pd.ClassId == tr.ClassId && pd.IsPublish == true)
                           && !_lumen.Tbl_HoldDetail.Any(hd => hd.StudentId == tr.StudentId && hd.IsHold == true)
                     select tr;

        var query = result.ToList();
        //    var query =  _lumen.TblTestRecords
        //.Where(x => x.BatchId == BatchId
        //            && x.StudentId == StudentId
        //            && _lumen.Tbl_PublishDetail.Any(pd => pd.ClassId == x.ClassId && pd.IsPublish == true)
        //            && _lumen.Tbl_HoldDetail.Any(hd => hd.StudentId == x.StudentId && hd.IsHold == false))
        //.ToList();

        if (query == null)
        {
          res.Data = query!;
          res.Msg = "Record Not Found.";
          res.ResponseCode = "404";
        }
        else
        {
          res.Data = query;
          res.Msg = "Record fetched successfully.";
          res.ResponseCode = "200";
        }
      });




    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;

  }
  public async Task<IApiResponse> GetStusdnetDetails( long BatchId, int classId,int SectionId, long StudentId=0)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var conn = _lumen.Database.GetDbConnection();
      await conn.OpenAsync();

      string sql = @"Exec usp_StudnetsGridinMobileApp "+classId+","+ SectionId + ","+ BatchId + "," + StudentId + " ";

      using (var command = conn.CreateCommand())
      {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        var reader = await command.ExecuteReaderAsync();

        var result = new List<StudnetsDetailsView>();
        while (await reader.ReadAsync())
        {
          var record = new StudnetsDetailsView
          {
            SerialNumber = reader.IsDBNull(reader.GetOrdinal("SerialNumber")) ? 0 : Convert.ToInt64(reader["SerialNumber"]),
            StudentID = reader.IsDBNull(reader.GetOrdinal("StudentId")) ? 0 : Convert.ToInt64(reader["StudentId"]),
            ScholarNo = reader["ScholarNo"]?.ToString() ?? string.Empty,
            Name = reader["Name"]?.ToString() ?? string.Empty,
            Last_Name = reader["Last_Name"]?.ToString() ?? string.Empty,
            School = reader["School"]?.ToString() ?? string.Empty,
            FatherName = reader["FatherName"]?.ToString() ?? string.Empty,
            MotherName = reader["MotherName"]?.ToString() ?? string.Empty,
            FMobile = reader["FMobile"]?.ToString() ?? string.Empty,
            FResidentialAddress = reader["FResidentialAddress"]?.ToString() ?? string.Empty,
            AdharNo = reader["AdharNo"]?.ToString() ?? string.Empty,
            FEMail = reader["FEMail"]?.ToString() ?? string.Empty,
            Class = reader["Class"]?.ToString() ?? string.Empty,
            Section = reader["Section"]?.ToString() ?? string.Empty,
            CastName = reader["CastName"]?.ToString() ?? string.Empty,
            Category = reader["Category"]?.ToString() ?? string.Empty,
            Religion = reader["Religion"]?.ToString() ?? string.Empty,
            Gender = reader["Gender"]?.ToString() ?? string.Empty,
            DOB = reader["DOB"]?.ToString() ?? string.Empty,
            CurrentYear = reader.IsDBNull(reader.GetOrdinal("CurrentYear")) ? 0 : Convert.ToInt64(reader["CurrentYear"]),
            ParentEmail = reader["ParentEmail"]?.ToString() ?? string.Empty,
            BloodGroup = reader["BloodGroup"]?.ToString() ?? string.Empty,
            City = reader["City"]?.ToString() ?? string.Empty,
            STATE = reader["STATE"]?.ToString() ?? string.Empty,
            Pincode = reader["Pincode"]?.ToString() ?? string.Empty,
            AdmissionDate = reader["AdmissionDate"]?.ToString() ?? string.Empty,
            Promotion_Date = reader["Promotion_Date"]?.ToString() ?? string.Empty,
            SSSMIdNumber = reader["SSSMIdNumber"]?.ToString() ?? string.Empty,
            BankAccount = reader["BankAccount"]?.ToString() ?? string.Empty,
            BankName = reader["BankName"]?.ToString() ?? string.Empty,
            BankACHolder = reader["BankACHolder"]?.ToString() ?? string.Empty,
            BankIFSC = reader["BankIFSC"]?.ToString() ?? string.Empty,
            Subjects = reader["Subjects"]?.ToString() ?? string.Empty,
            OptionalSubjects = reader["OptionalSubjects"]?.ToString() ?? string.Empty,
            ApplicationNumber = reader["ApplicationNumber"]?.ToString() ?? string.Empty,
            ApaarId = reader["ApaarId"]?.ToString() ?? string.Empty,
            PerEduNumber = reader["PerEduNumber"]?.ToString() ?? string.Empty,
            TotalRecords = reader.IsDBNull(reader.GetOrdinal("TotalRecords")) ? 0 : Convert.ToInt64(reader["TotalRecords"]),
            TotalDaysPresent = reader.IsDBNull(reader.GetOrdinal("TotalDaysPresent")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalDaysPresent")),
            PaidAmount = reader.IsDBNull(reader.GetOrdinal("PaidAmount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("PaidAmount")),
            Percentage = reader.IsDBNull(reader.GetOrdinal("Percentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Percentage")),
            ProfilePicture = reader["ProfilePicture"]?.ToString() ?? string.Empty,
            ClassId = reader.IsDBNull(reader.GetOrdinal("Class_Id")) ? 0 : Convert.ToInt32(reader["Class_Id"]),
            SectionId = reader.IsDBNull(reader.GetOrdinal("Section_Id")) ? 0 : Convert.ToInt32(reader["Section_Id"]),
            BatchId = reader.IsDBNull(reader.GetOrdinal("Batch_Id")) ? 0 : Convert.ToInt32(reader["Batch_Id"]),
          };


          result.Add(record);
        }

        if (result.Any())
        {
          res.Data = result;
          res.Msg = "Record fetched successfully.";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = result;
          res.Msg = "Record Not Found.";
          res.ResponseCode = "404";
        }

        await conn.CloseAsync();
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;

  }
  public async Task<IApiResponse> GetStaffDetails(long StaffId = 0)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var conn = _lumen.Database.GetDbConnection();
      await conn.OpenAsync();

      string sql = @"Exec GetStaffForMobile " + StaffId + "";

      using (var command = conn.CreateCommand())
      {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        var reader = await command.ExecuteReaderAsync();

        var result = new List<StaffDetailsModel>();
        while (await reader.ReadAsync())
        {
          var staff = new StaffDetailsModel
          {
            StafId = reader.IsDBNull(reader.GetOrdinal("StafId")) ? 0 : Convert.ToInt64(reader["StafId"]),
            UIN = reader["UIN"]?.ToString() ?? string.Empty,
            Name = reader["Name"]?.ToString() ?? string.Empty,
            Gender = reader["Gender"]?.ToString() ?? string.Empty,
            AgeInWords = reader["AgeInWords"]?.ToString() ?? string.Empty,
            DOB = reader["DOB"]?.ToString() ?? string.Empty,
            POB = reader["POB"]?.ToString() ?? string.Empty,
            Nationality = reader["Nationality"]?.ToString() ?? string.Empty,
            Religion = reader["Religion"]?.ToString() ?? string.Empty,
            Qualification = reader["Qualification"]?.ToString() ?? string.Empty,
            WorkExperience = reader["WorkExperience"]?.ToString() ?? string.Empty,
            MotherTongue = reader["MotherTongue"]?.ToString() ?? string.Empty,
            Contact = reader["Contact"]?.ToString() ?? string.Empty,
            Email = reader["Email"]?.ToString() ?? string.Empty,
            EmpId = reader["EmpId"]?.ToString() ?? string.Empty,
            UserId = reader["UserId"]?.ToString() ?? string.Empty,
            Designation = reader["Designation"]?.ToString() ?? string.Empty,
            FatherOrHusbandName = reader["FatherOrHusbandName"]?.ToString() ?? string.Empty,
            MariedStatus = reader["MariedStatus"]?.ToString() ?? string.Empty,
            MothersName = reader["MothersName"]?.ToString() ?? string.Empty,
            Children = reader["Children"]?.ToString() ?? string.Empty,
            StaffSignatureFile = reader["StaffSignatureFile"]?.ToString() ?? string.Empty,
            Employee_Designation = reader["Employee_Designation"]?.ToString() ?? string.Empty,
            Staff_CategoryName = reader["Staff_CategoryName"]?.ToString() ?? string.Empty,
            UAN = reader["UAN"]?.ToString() ?? string.Empty,
            StaffCategory = reader["StaffCategory"]?.ToString() ?? string.Empty
          };
          result.Add(staff);

        }

        if (result.Any())
        {
          res.Data = result;
          res.Msg = "Record fetched successfully.";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = result;
          res.Msg = "Record Not Found.";
          res.ResponseCode = "404";
        }

        await conn.CloseAsync();
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;

  }
  //public async Task<IApiResponse> GetStudentDetail(string ApplicationNo)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {

  //    //var query = await _lumen.Students.Where(x => x.UserId == UserId).FirstOrDefaultAsync();

  //    await Task.Run(() =>
  //    {
  //      var result = (from s in _lumen.Students
  //                    join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
  //                    join d in _lumen.TblDataListItems on s.SectionId equals d.DataListItemId
  //                    join f in _lumen.FamilyDetails on s.StudentId equals f.StudentRefId
  //                    where s.ApplicationNumber == ApplicationNo
  //                    select new
  //                    {
  //                      Studename = s.Name,
  //                      LastName=s.LastName,
  //                      Class=c.DataListItemName,
  //                      Section = d.DataListItemName,
  //                      RollNo=s.RollNo,
  //                      fatherName=f.FatherName,
  //                      MotheName=f.MotherName,
  //                      ContactNo=s.Mobile,
  //                      FatherEmail=f.Femail,
  //                      Email=s.ParentEmail,

  //                      Attendance = Attendance(s.BatchId, s.ClassId, s.StudentId, s.SectionId)
  //                    });

  //      var query = result.ToList();


  //      if (query == null)
  //      {
  //        res.Data = query!;
  //        res.Msg = "Record Not Found.";
  //        res.ResponseCode = "404";
  //      }
  //      else
  //      {
  //        res.Data = query;
  //        res.Msg = "Record fetched successfully.";
  //        res.ResponseCode = "200";
  //      }
  //    });




  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;

  //}
  public async Task<IApiResponse> GetStudentDetail(string ApplicationNo)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      // Fetch data first (without projection)
      var studentData = await (from s in _lumen.Students
                               join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                               join d in _lumen.TblDataListItems on s.SectionId equals d.DataListItemId
                               join f in _lumen.FamilyDetails on s.ApplicationNumber equals f.ApplicationNumber
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
                                 FatherName = f.FatherName,
                                 MotherName = f.MotherName,
                                 ContactNo = s.Mobile,
                                 FatherEmail = f.Femail,
                                 Email = s.ParentEmail
                               }).ToListAsync(); // Execute the query first

      // Now apply further processing
      var result = studentData.Select(s => new
      {
        s.StudentId,
        s.Name,
        s.LastName,
        s.Class,
        s.Section,
        s.RollNo,
        s.FatherName,
        s.MotherName,
        s.ContactNo,
        s.FatherEmail,
        s.Email,
        Attendance = Attendance(s.BatchId, s.ClassId, s.StudentId, s.SectionId) // Now it works
      }).ToList();

      if (!result.Any())
      {
        res.Data = result;
        res.Msg = "Record Not Found.";
        res.ResponseCode = "404";
      }
      else
      {
        res.Data = result;
        res.Msg = "Record fetched successfully.";
        res.ResponseCode = "200";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> GetStudentProfile(string applicationNo)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      // Step 1: Get student record
      var student = await _lumen.Students
          .Where(x => x.ApplicationNumber == applicationNo)
          .Select(x => new
          {
            x.StudentId,
            x.BatchId,
            x.ClassId,
            x.SectionId
          })
          .FirstOrDefaultAsync();

      // Step 2: Check if student exists
      if (student == null)
      {
        res.Msg = "Student not found.";
        res.ResponseCode = "404";
        return res;
      }

      // Step 3: Call GetStusdnetDetails using student data
      res = await GetStusdnetDetails(
          BatchId: student.BatchId,
          classId: student.ClassId,
          SectionId: student.SectionId??0,
          StudentId: student.StudentId
      );
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }

  public async Task<IApiResponse> GetStudentProfileSummeryBatchWise(long studentId, int batchId)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var conn = _lumen.Database.GetDbConnection();
      await conn.OpenAsync();

      string sql = @"Exec usp_GetStudentProfileSummeryBatchWise "+ batchId + ", "+ studentId + "";

      using (var command = conn.CreateCommand())
      {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        var reader = await command.ExecuteReaderAsync();

        var coScholasticList = new List<CoScholasticResult>();
        var attendanceList = new List<StudentAttendanceSummary>();
        var testPercentageList = new List<StudentTestPercentage>();
        var feedetails=new List<StudentFeeDetails>();
        // 1st result set - Co-Scholastic
        while (await reader.ReadAsync())
        {
          var item = new CoScholasticResult
          {
            StudentID = reader.IsDBNull(reader.GetOrdinal("StudentId")) ? 0 : Convert.ToInt64(reader["StudentId"]),
            TermID = reader.IsDBNull(reader.GetOrdinal("TermId")) ? 0 : Convert.ToInt32(reader["TermId"]),
            BatchId = reader.IsDBNull(reader.GetOrdinal("BatchId")) ? 0 : Convert.ToInt64(reader["BatchId"]),
            Title = reader["Title"]?.ToString() ?? string.Empty,
            ObtainedGrade = reader["ObtainedGrade"]?.ToString() ?? string.Empty,
            Batch_Name= reader["Batch_Name"]?.ToString() ?? string.Empty
          };
          coScholasticList.Add(item);
        }

        // Move to 2nd result set - Attendance
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
          var item = new StudentAttendanceSummary
          {
            TotalDays = reader.IsDBNull(reader.GetOrdinal("TotalDays")) ? 0 : Convert.ToInt32(reader["TotalDays"]),
            PresentDays = reader.IsDBNull(reader.GetOrdinal("PresentDays")) ? 0 : Convert.ToDecimal(reader["PresentDays"]),
            //Percentage = reader.IsDBNull(reader.GetOrdinal("Percentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Percentage")),
            StudentRegisterId = reader.IsDBNull(reader.GetOrdinal("StudentRegisterId")) ? 0 : Convert.ToInt64(reader["StudentRegisterId"]),
            BatchId = reader.IsDBNull(reader.GetOrdinal("BatchId")) ? 0 : Convert.ToInt64(reader["BatchId"]),
            Batch_Name = reader["Batch_Name"]?.ToString() ?? string.Empty
          };
          attendanceList.Add(item);
        }

        // Move to 3rd result set - Test Performance
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
          var item = new StudentTestPercentage
          {
            StudentId = reader.IsDBNull(reader.GetOrdinal("StudentId")) ? 0 : Convert.ToInt64(reader["StudentId"]),
            BatchId = reader.IsDBNull(reader.GetOrdinal("BatchId")) ? 0 : Convert.ToInt64(reader["BatchId"]),
            TermId = reader.IsDBNull(reader.GetOrdinal("TermId")) ? 0 : Convert.ToInt32(reader["TermId"]),
           // SubjectName = reader["SubjectName"]?.ToString() ?? string.Empty,
            Percentage = reader.IsDBNull(reader.GetOrdinal("Percentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Percentage")),
            Batch_Name = reader["Batch_Name"]?.ToString() ?? string.Empty
          };
          testPercentageList.Add(item);
        }
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
          var item = new StudentFeeDetails
          {
            PaidAmount = reader.IsDBNull(reader.GetOrdinal("PaidAmount")) ? 0 : Convert.ToInt64(reader["PaidAmount"]),
            DueAmount = reader.IsDBNull(reader.GetOrdinal("DueAmount")) ? 0 : Convert.ToInt64(reader["DueAmount"]),

          };
          feedetails.Add(item);
        }

        var profile = new StudentProfileSummaryBatchWise
        {
          CoScholasticResults = coScholasticList,
          AttendanceSummaries = attendanceList,
          TestPercentages = testPercentageList,
          feeDetails=feedetails
        };

        res.Data = profile;
        res.Msg = "Profile data fetched successfully.";
        res.ResponseCode = "200";

        await conn.CloseAsync();
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }
  public string Attendance( int BatchId,int ClassId,long StudentID, int? SectionId=0)
  {
    List<TblStudentAttendance> ActualAttendance = new List<TblStudentAttendance>();
 
      var batch = _lumen.TblBatches.Where(x => x.BatchId == BatchId).FirstOrDefault();
      //  var batchItems = _context.DataListItems.Where(x => x.DataListId == "9" && x.DataListItemName== batch.Batch_Name).FirstOrDefault();
      var attendanceDate = _lumen.TblTestAssignDates.Where(x=> x.BatchId == BatchId && x.ClassId == ClassId).FirstOrDefault();
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
    ActualAttendance = _lumen.TblStudentAttendances.Where(x => x.StudentRegisterId == StudentID &&x.ClassId == ClassId && x.SectionId == SectionId).ToList().Where(x =>
DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date >= StartDate.Date &&
DateTime.ParseExact(x.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date <= ToDate.Date).ToList();
      ActualAttendance = _lumen.TblStudentAttendances.Where(x => x.StudentRegisterId == StudentID && x.ClassId == ClassId && x.SectionId == SectionId && x.BatchId==BatchId).ToList();

   

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
}
