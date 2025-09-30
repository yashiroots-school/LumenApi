using System.Data.Entity;
//using System.Data.SqlClient;
using System.Linq;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Infrastructure.Data.LumenContext;
using LumenApi.Web.Models.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static LumenApi.Web.Services.ExamService;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using DinkToPdf;
using DinkToPdf.Contracts;
using Org.BouncyCastle.Utilities;
using LumenApi.UseCases.CommonClasses;
using System.Data;
using Dapper;
using LumenApi.Web.Models.Exam.ReportCardModel;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
namespace LumenApi.Web.Services;

using LumenApi.Web.Models.StudentView;
using LumenApi.Web.ViewModels;

public class ExamService(Lumen090923Context lumen, IConverter converter) : IExamInterface
{
  private readonly Lumen090923Context _lumen = lumen;
  private readonly IConverter _converter = converter;
  public async Task<IApiResponse> GetPublishData(PublishDetail objPublishDetail)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      await Task.Run(() =>
      {
        var publishItem = _lumen.Tbl_PublishDetail
                 .Where(x => x.ClassId == objPublishDetail.ClassId
                  && x.SectionId == objPublishDetail.SectionId
                  && x.TermId == objPublishDetail.TermId
                  && x.BatchId == objPublishDetail.BatchID)?.OrderByDescending(x => x.PublishId)
                  .FirstOrDefault();

        if (publishItem != null)
        {



          res.Data = publishItem;
          res.Msg = "data Found";
          res.ResponseCode = "200";
        }
        else
        {

          res.Data = new List<PublishDetail>();
          res.Msg = "No data Found";
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

  public async Task<IApiResponse> PublishUnpublish(PublishDetail objPublishDetail)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      await Task.Run(() =>
        {
          if (objPublishDetail.TermId == 10)
          {

            res.Data = new List<PublishDetail>();
            res.Msg = "publish for all term not allow";
            res.ResponseCode = "200";

          }

          var publishItem = _lumen.Tbl_PublishDetail
                     .Where(x => x.ClassId == objPublishDetail.ClassId
                      && x.SectionId == objPublishDetail.SectionId
                      && x.TermId == objPublishDetail.TermId
                      && x.BatchId == objPublishDetail.BatchID)?.OrderByDescending(x => x.PublishId)
                      .FirstOrDefault();

          if (publishItem != null)
          {
            publishItem.IsPublish = objPublishDetail.IsPublish;
            publishItem.ModifiedDate = DateTime.Now;
            publishItem.PublishBy = objPublishDetail.PublishBy;
            _lumen.SaveChanges();

            res.Data = publishItem;
            res.Msg = "publish data update successfully";
            res.ResponseCode = "200";
          }
          else
          {
            Tbl_PublishDetail objpublish = new Tbl_PublishDetail();
            objpublish.TermId = objPublishDetail.TermId;
            objpublish.SectionId = objPublishDetail.SectionId;
            objpublish.BatchId = objPublishDetail.BatchID;
            objpublish.ClassId = objPublishDetail.ClassId;
            objpublish.IsPublish = true;
            objpublish.PublishDate = DateTime.Now;
            objpublish.PublishBy = objPublishDetail.PublishBy;
            _lumen.Tbl_PublishDetail.Add(objpublish);
            _lumen.SaveChanges();

            res.Data = objpublish;
            res.Msg = "publish data Save successfully";
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

  public async Task<IApiResponse> FreezeUnfreezeData(FreezeUnfreezeDTO freezeUnfreezeDTO)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      await Task.Run(() =>
      {

        if (freezeUnfreezeDTO.IsFreeze)
        {
          var FreezeItem = _lumen.Tbl_FreezeData
              .Where(x => x.ClassId == freezeUnfreezeDTO.ClassId
                       && x.TermId == freezeUnfreezeDTO.TermId
                       && x.SectionId == freezeUnfreezeDTO.SectionId
                       && x.BatchId == freezeUnfreezeDTO.BatchId)
              .FirstOrDefault();

          if (FreezeItem != null)
          {
            FreezeItem.IsFreeze = freezeUnfreezeDTO.IsFreeze;
            _lumen.SaveChanges();
            res.Msg = "Freezed successfully";
            res.ResponseCode = "200";
          }
          else
          {
            if (freezeUnfreezeDTO.TermId != 0
              && freezeUnfreezeDTO.BatchId != 0
              && freezeUnfreezeDTO.ClassId == 0
              && freezeUnfreezeDTO.SectionId == 0)
            {
              var getDataList = _lumen.TblDataLists?.ToList();

              var getClassData = getDataList?.Where(x => x.DataListName?.ToLower() == "class")
              .Select(x => x.DataListId.ToString())
              .FirstOrDefault();

              var getSectionData = getDataList?.Where(x => x.DataListName?.ToLower() == "section")
             .Select(x => x.DataListId.ToString())
             .FirstOrDefault();

              var getDataListItems = _lumen.TblDataListItems.ToList();


              var Classes = getDataListItems
                  .Where(e => e.DataListId == getClassData)
                  .ToList();


              var Sections = getDataListItems
                  .Where(e => e.DataListId == getSectionData)
                  .ToList();


              foreach (var classItem in Classes)
              {

                foreach (var sectionItem in Sections)
                {
                  var newFreeze = new Tbl_FreezeData
                  {
                    TermId = freezeUnfreezeDTO.TermId,
                    ClassId = classItem.DataListItemId,
                    SectionId = sectionItem.DataListItemId,
                    BatchId = freezeUnfreezeDTO.BatchId,
                    IsFreeze = freezeUnfreezeDTO.IsFreeze
                  };

                  _lumen.Tbl_FreezeData.Add(newFreeze);
                }
              }

              _lumen.SaveChanges();

              res.Msg = "Freezed successfully";
              res.ResponseCode = "200";
            }

            else
            {
              // Create a new Freeze entity and add it to the context
              var newFreeze = new Tbl_FreezeData
              {
                TermId = freezeUnfreezeDTO.TermId,
                ClassId = freezeUnfreezeDTO.ClassId,
                SectionId = freezeUnfreezeDTO.SectionId,
                BatchId = freezeUnfreezeDTO.BatchId,
                IsFreeze = freezeUnfreezeDTO.IsFreeze
              };

              _lumen.Tbl_FreezeData.Add(newFreeze);
              _lumen.SaveChanges();
              res.Msg = "Freezed successfully";
              res.ResponseCode = "200";
            }
          }
        }
        else
        {
          var FreezeItem = _lumen.Tbl_FreezeData
              .Where(x => x.FreezeId == freezeUnfreezeDTO.FreezeId)
              .FirstOrDefault();
          if (FreezeItem != null)
          {
            FreezeItem.IsFreeze = false;
            _lumen.SaveChanges();
            res.Msg = "Unfreezed successfully";
            res.ResponseCode = "200";
          }
          else
          {
            res.Msg = "Incorrect Freeze data Id";
            res.ResponseCode = "200";
          }
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

  public async Task<IApiResponse> GetFreezingData(FreezeUnfreezeDTO freezeUnfreezeDTO)
  {
    IApiResponse res = new ApiResponse();
    try
    {

      await Task.Run(() =>
      {
        var FreezeItem = _lumen.Tbl_FreezeData
                            .Where(x => x.ClassId == freezeUnfreezeDTO.ClassId
                             && x.SectionId == freezeUnfreezeDTO.SectionId
                             && x.TermId == freezeUnfreezeDTO.TermId
                             && x.BatchId == freezeUnfreezeDTO.BatchId)
                             .FirstOrDefault();

        if (FreezeItem != null)
        {
          res.Data = FreezeItem;
          res.Msg = "Get Freezing data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new FreezeUnfreezeDTO();
          res.Msg = "data Found";
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
  public async Task<IApiResponse> GetStaffSubjects(int staffId, int classId, int sectionId)
  {
    IApiResponse res = new ApiResponse();
    List<StaffclassectionSubject> subjects = new List<StaffclassectionSubject>();
    int batchId = _lumen.TblBatches.Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission == true).Select(x => x.BatchId).FirstOrDefault();
    try
    {
      var connectionString = _lumen.Database.GetConnectionString();

      using (SqlConnection conn = new SqlConnection(connectionString))
      using (SqlCommand cmd = new SqlCommand("GetStaffSubjectsForHomeWork", conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StaffId", staffId);
        cmd.Parameters.AddWithValue("@ClassId", classId);
        cmd.Parameters.AddWithValue("@SectionId", sectionId);
        cmd.Parameters.AddWithValue("@BatchId", batchId);

        await conn.OpenAsync();

        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            var subject = new StaffclassectionSubject
            {
              StaffId = reader.GetInt32(reader.GetOrdinal("StaffId")),
              ClassId = reader.GetInt32(reader.GetOrdinal("Class_Id")),
              SectionId = reader.GetInt32(reader.GetOrdinal("Section_Id")),
              BatchId = reader.GetInt32(reader.GetOrdinal("Batch_Id")),
              SubjectID = reader.GetInt32(reader.GetOrdinal("Subject_ID")),
              SubjectName = reader.GetString(reader.GetOrdinal("Subject_Name"))
            };

            subjects.Add(subject);
          }
        }
      }

      res.Data = subjects;
      res.Msg = "Data fetched successfully";
      res.ResponseCode = "200";
    }
    catch (SqlException sqlEx)
    {
     
      res.Msg = "SQL Error: " + sqlEx.Message;
      res.ResponseCode = "500";
    }
    catch (Exception ex)
    {
    
      res.Msg = "Error: " + ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }


  public async Task<IApiResponse> getStaffList()
  {
    IApiResponse res = new ApiResponse();
    try
    {
      await Task.Run(() =>
      {
        var Result = _lumen.StafsDetails.Where(s => s.IsDeleted != true).ToList();
        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
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
  public async Task<IApiResponse> GetClassList(int staff_id)
  {
    IApiResponse res = new ApiResponse();
    ;
    try
    {
      await Task.Run(() =>
      {

        var currentBatch=_lumen.TblBatches.Where(X=>X.IsActiveForAdmission==true && X.IsActiveForPayments==true).Select(x=>x.BatchId).FirstOrDefault();
        var Result = (from s in _lumen.Subjects
                      join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                      join d in _lumen.TblDataListItems on s.SectionId equals d.DataListItemId
                      join ss in _lumen.TblSubjectsSetups on s.SubjectId equals ss.SubjectId
                      where s.StaffId == staff_id && s.BatchId==currentBatch
                      select new
                      {
                        DataListItemId = c.DataListItemId,
                        DataListItemName = c.DataListItemName,
                        DataListId = c.DataListId,  // Select DataListId
                        DataListName = c.DataListName,  // Select DataListName
                        Status = c.Status,  // Select Status
                        Section=d.DataListItemName,
                        SubjectName=ss.SubjectName,
                        SubjectId=s.SubjectId,
                        sectionId=s.SectionId,
                        IsClassTeacher = s.ClassTeacher
                      }).Distinct();
        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
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
  public async Task<IApiResponse> GetStaffClassList(int staff_id)
  {
    IApiResponse res = new ApiResponse();
    ;
    try
    {
      await Task.Run(() =>
      {

        var currentBatch = _lumen.TblBatches.Where(X => X.IsActiveForAdmission == true && X.IsActiveForPayments == true).Select(x => x.BatchId).FirstOrDefault();
        //var Result = (from s in _lumen.Subjects
        //                              join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
        //                              where s.StaffId == staff_id && s.BatchId == currentBatch && s.ClassTeacher==true
        //              group new { s, c } by new { c.DataListItemId, c.DataListItemName, s.ClassTeacher } into g
        //                              select new
        //                              {
        //                                DataListItemId = g.Key.DataListItemId,
        //                                DataListItemName = g.Key.DataListItemName,
        //                                IsClassTeacher = g.Key.ClassTeacher
        //                              }).ToList();
        var Result = (from s in _lumen.Subjects
                      join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                      where (staff_id == 0 || s.StaffId == staff_id)
                            && s.BatchId == currentBatch
                            && s.ClassTeacher == true
                      group new { s, c } by new { c.DataListItemId, c.DataListItemName, s.ClassTeacher } into g
                      select new
                      {
                        DataListItemId = g.Key.DataListItemId,
                        DataListItemName = g.Key.DataListItemName,
                        IsClassTeacher = g.Key.ClassTeacher
                      }).ToList();

        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
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
  public async Task<IApiResponse> GetStaffSectionList(int staffId, int classId)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
      {
        var currentBatch = _lumen.TblBatches.Where(X => X.IsActiveForAdmission == true && X.IsActiveForPayments == true).Select(x => x.BatchId).FirstOrDefault();

        //var Result = (from s in _lumen.Subjects
        //              join c in _lumen.TblDataListItems on s.SectionId equals c.DataListItemId
        //              where s.StaffId == staffId && s.ClassId == classId && s.BatchId == currentBatch && s.ClassTeacher==true
        //              group new { s, c } by new { s.SectionId, c.DataListItemName } into g
        //              select new
        //              {
        //                SectionId = g.Key.SectionId,
        //                SectionName = g.Key.DataListItemName
        //              }).ToList();
        var Result = (from s in _lumen.Subjects
                      join c in _lumen.TblDataListItems on s.SectionId equals c.DataListItemId
                      where (staffId == 0 || s.StaffId == staffId)
                            && s.ClassId == classId
                            && s.BatchId == currentBatch
                            && s.ClassTeacher == true
                      group new { s, c } by new { s.SectionId, c.DataListItemName } into g
                      select new
                      {
                        SectionId = g.Key.SectionId,
                        SectionName = g.Key.DataListItemName
                      }).ToList();
        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "400";

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
  public async Task<IApiResponse> GetSectionList(int staffId, int classId)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
      {


        var Result = (from s in _lumen.Subjects

                      join c in _lumen.TblDataListItems on s.SectionId equals c.DataListItemId
                      where s.StaffId == staffId && s.ClassId == classId
                      select c).Distinct();
        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "400";

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
  public async Task<IApiResponse> GetHomeWorkList(int ClassId, int SectionId,int SubjectId=0)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
      {


        var Result = (from s in _lumen.TblAssignments
                      join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                      join se in _lumen.TblDataListItems on s.SectionId equals se.DataListItemId
                      join st in _lumen.StafsDetails on s.Staff_Id equals st.StafId
                      join sub in _lumen.TblSubjectsSetups on s.SubjectId equals sub.SubjectId
                      where (ClassId == 0 || s.ClassId == ClassId) &&
                               (SectionId == 0 || s.SectionId == SectionId) &&
                               (SubjectId == 0 || s.SubjectId == SubjectId)
                      select new
                      {
                        AssignmentId=s.AssignmentId,
                        AssignMentName=s.NewAssignment,
                        Class=c.DataListItemName,
                        AssignMentDate=s.AssignmentDate,
                        SubmittedDate = s.SubmittedDate,
                        CreatedDate=s.CreatedDate,
                        Section = se.DataListItemName,
                        sectionId = s.SectionId,
                        TeacherName=st.Name,
                        SubjectId=s.SubjectId,
                        SubjectName=sub.SubjectName,
                      }).Distinct();
        if (Result != null)
        {
          res.Data = Result;
          res.Msg = "Get data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          //res.Data = new StafsDetail();
          res.Msg = "data not Found";
          res.ResponseCode = "400";

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
  public async Task<IApiResponse> AddHomeWork(TblAssignment tblAssignment)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      tblAssignment.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");
      if (tblAssignment.BatchId == 0)
      {
        tblAssignment.BatchId = _lumen.TblBatches.Where(x => x.IsActiveForPayments == true && x.IsActiveForAdmission == true).Select(x => x.BatchId).FirstOrDefault();
      }
      await _lumen.TblAssignments.AddAsync(tblAssignment);
      int changes = await _lumen.SaveChangesAsync();

      if (changes > 0)
      {
          //res.Data = Result;
          res.Msg = "Saved successfully";
          res.ResponseCode = "200";
      }
        else
        {
        //res.Data = new StafsDetail();
        res.Msg = "Not Saved";
          res.ResponseCode = "400";

        }
      
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> UpdateAssignment(TblAssignment tbl_Assignment)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      var existingAssignment = await _lumen.TblAssignments
          .FirstOrDefaultAsync(x => x.AssignmentId== tbl_Assignment.AssignmentId);

      if (existingAssignment != null)
      {
        _lumen.Entry(existingAssignment).CurrentValues.SetValues(tbl_Assignment);
        await _lumen.SaveChangesAsync();

        //res.Data = tbl_Assignment;
        res.Msg = "Assignment updated successfully";
        res.ResponseCode = "200";
      }
      else
      {
        //res.Data = null;
        res.Msg = "Assignment not found";
        res.ResponseCode = "404";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }

  public async Task<IApiResponse> GetAssignmentById(int id)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
      {
        var data = (from s in _lumen.TblAssignments
                    join c in _lumen.TblDataListItems on s.ClassId equals c.DataListItemId
                    join se in _lumen.TblDataListItems on s.SectionId equals se.DataListItemId
                    join st in _lumen.StafsDetails on s.Staff_Id equals st.StafId
                    join sub in _lumen.TblSubjectsSetups on s.SubjectId equals sub.SubjectId
                    where s.AssignmentId == id
                    select new
                    {
                      AssignmentId = s.AssignmentId,
                      AssignMentName = s.NewAssignment,
                      Class = c.DataListItemName,
                      AssignMentDate = s.AssignmentDate,
                      SubmittedDate = s.SubmittedDate,
                      CreatedDate = s.CreatedDate,
                      Section = se.DataListItemName,
                      sectionId = s.SectionId,
                      TeacherName = st.Name,
                      SubjectId = s.SubjectId,
                      SubjectName = sub.SubjectName,
                    }).Distinct();

        if (data != null)
        {
          res.Data = data;
          res.Msg = "Assignment found successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Msg = "Assignment not found";
          res.ResponseCode = "404";
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


  public async Task<IApiResponse> DeleteAssignment(int id)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var data = _lumen.TblAssignments.FirstOrDefault(x => x.AssignmentId == id);
      if (data != null)
      {
        _lumen.TblAssignments.Remove(data);
        _lumen.SaveChanges();
        res.Msg = "Assignment deleted successfully";
        res.ResponseCode = "200";
      }
      else
      {
        res.Msg = "Assignment not found";
        res.ResponseCode = "404";
      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return await Task.FromResult(res);
  }

  public async Task<IApiResponse> GetTermDropList()
  {
    IApiResponse res = new ApiResponse();
    try
    {
      await Task.Run(() =>
      {
        var _Result = _lumen.TblTerms.ToList();
        //res.Data = _Result;

        if (_Result != null)
        {
          res.Data = _Result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "400";

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

  public async Task<IApiResponse> GetBatchDropList()
  {
    IApiResponse res = new ApiResponse();
    try
    {
      await Task.Run(() =>
      {
        // var _Result = _lumen.TblTerms.ToList();
        var BatchList = _lumen.TblBatches.Select(x => new BatchListModel
        {
          BatchId = x.BatchId,
          BatchName = x.BatchName
        }).OrderBy(x => x.BatchName).ToList();

        if (BatchList != null)
        {
          res.Data = BatchList;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "400";

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

  public async Task<IApiResponse> StudentByClassSection(int classId, int sectionId, int testId, int termId, int staffId, int batchId)
  {
    IApiResponse res = new ApiResponse();
    List<ListStudent> listStudents = new List<ListStudent>();
    List<TblTests> Tests;
    try
    {
      await Task.Run(() =>
      {
        // var _Result = _lumen.TblTerms.ToList();
        bool IsClassTeacher = _lumen.Subjects.Any(x => x.ClassId == classId && x.BatchId == batchId && x.StaffId == staffId && x.SectionId == sectionId && x.ClassTeacher == true);
        if (IsClassTeacher)
        {
          Tests = _lumen.TblTests.Where(x => x.ClassId == classId && x.TermId == termId).ToList();
          foreach (var item in Tests)
          {
            var termName = _lumen.TblTerms.Where(x => x.TermId == item.TermId).Select(x => x.TermName).FirstOrDefault();
            item.TestName = item.TestName + "(" + item.TestType + ", " + termName + ")";
          }
          TblTests remark = new TblTests()
          {
            TestId = 0,
          };
          Tests.Add(remark);
        }
        else
        {
          var staffsubjectids = _lumen.Subjects
                                .Where(x => (x.StaffId == staffId && x.ClassId == classId) || x.BatchId == batchId)
                                .Select(x => x.SubjectId)
                                .ToList();

          //var tests = _lumen.TblTests
          //                    .Where(x => staffsubjectids.Contains((int)x.SubjectId) && x.TermId == termId && x.ClassId == classId)
          //                    .ToList();
          var parameters_ = string.Join(",", staffsubjectids.Select((id, index) => $"@p{index}"));
          var _sql = $"SELECT * FROM Tbl_Tests  WHERE SubjectId IN ({parameters_}) And TermID ={termId}And ClassID = {classId}";

          var tests = _lumen.TblTests
      .FromSqlRaw(_sql, staffsubjectids.Select((id, index) => new Microsoft.Data.SqlClient.SqlParameter($"@p{index}", id)).ToArray())
      .ToList();

          var subjects = _lumen.Subjects
                   .Where(x => x.StaffId == staffId && x.ClassId == classId && x.SectionId == sectionId).ToList();
          Tests = subjects.Where(x => x.BatchId == batchId).SelectMany(subject => _lumen.TblTests.Where(test => test.SubjectId == subject.SubjectId
     && test.TermId == termId && test.ClassId == classId))
                             .Distinct()
                             .ToList();
          foreach (var item in Tests)
          {
            var termName = _lumen.TblTerms.Where(x => x.TermId == item.TermId).Select(x => x.TermName).FirstOrDefault();
            item.TestName = item.TestName + "(" + item.TestType + ", " + termName + ")";
          }


        }
        List<Student> studentlist = new List<Student>();

        var SubjectIds = Tests.Select(x => x.SubjectId).ToList();
        //var electiveSubjectId = _lumen.TblClassSubjects
        //.Where(x => SubjectIds.Contains(x.SubjectId)
        //&& x.ClassId == classId && x.IsElective == true)
        //.ToList();
        // var electiveSubjectId = _lumen.TblClassSubjects
        // .Where(x => SubjectIds.Contains(x.SubjectId) &&
        //x.ClassId == classId && x.IsElective == true).ToList();
        //   var electiveSubjectId = _lumen.TblClassSubjects
        //.FromSqlRaw("SELECT * FROM TblClassSubjects WHERE SubjectId IN ({0}) AND ClassId = {1} AND IsElective = 1",
        //    string.Join(", ", SubjectIds), classId)
        //.ToList();
        var parameters = string.Join(",", SubjectIds.Select((id, index) => $"@p{index}"));
        var sql = $"SELECT * FROM Tbl_ClassSubject WHERE SubjectId IN ({parameters}) AND ClassId = {classId} AND IsElective = 1";

        var electiveSubjectId = _lumen.TblClassSubjects
    .FromSqlRaw(sql, SubjectIds.Select((id, index) => new Microsoft.Data.SqlClient.SqlParameter($"@p{index}", id)).ToArray())
    .ToList();

        //var classIdParameter = new System.Data.SqlClient.SqlParameter("@classId", classId);


        //var electiveSubjectId = _lumen.TblClassSubjects
        //    .FromSqlRaw(sql, SubjectIds.Append(classIdParameter).ToArray())
        //    .ToList();

        //var classIdParameter = new System.Data.SqlClient.SqlParameter("@classId", classId);

        //var electiveSubjectId = _lumen.TblClassSubjects
        //    .FromSqlRaw(sql, (long)SubjectIds.Concat(new[] { classIdParameter }).ToArray())
        //    .ToList();
        // var electiveSubjectId = _lumen.TblClassSubjects
        //.FromSqlInterpolated($"SELECT * FROM Tbl_ClassSubject WHERE SubjectId IN ({Convert.ToInt64(string.Join(",", SubjectIds))}) AND ClassId = {classId} AND IsElective = 1")
        //.ToList();
        //var electiveSubjectId = _lumen.TblClassSubjects.Where(s => SubjectIds.Contains(s.SubjectId)
        //&& s.ClassId == classId).ToList();


        var Freezeitem = _lumen.Tbl_FreezeData.Where(x => x.ClassId == classId && x.SectionId == sectionId
                                       && x.TermId == termId).FirstOrDefault();


        studentlist = _lumen.Students.Where(x => x.ClassId == classId
    && x.SectionId == sectionId && x.BatchId == batchId && x.IsApplyforTc == false).OrderBy(x => x.Name).ToList();
        var stdInfo = new List<TblTestRecord>();
        List<long> fk = new List<long>();
        if (studentlist.Count == 0)
        {
          stdInfo = _lumen.TblTestRecords.Where(x => x.BatchId == batchId && x.ClassId == classId
  && x.SectionId == sectionId && x.TermId == termId).ToList();

          foreach (var item in stdInfo)
          {
            var studentElectiveSubjectIds = _lumen.TblStudentElectiveRecord.Where(x => x.StudentId == item.StudentId).Select(x => x.ElectiveSubjectId).ToList();
            var IsAlreadyExist1 = _lumen.TblTestRecords.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.TermId == termId).ToList();
            var RecordFKID = IsAlreadyExist1.Where(x1 => x1.StudentId == item.StudentId).Select(x2 => x2.RecordId).FirstOrDefault();
            fk.Add(RecordFKID);
            var studentObtainedData = _lumen.TblTestObtainedMarks.Where(x => x.RecordIdfk == RecordFKID).ToList();

            List<StudentTestObtMarks> studentTestObtMarksList = new List<StudentTestObtMarks>();

            foreach (var data in Tests)
            {
              if (data.TestId == 0)
              {
                var RemarkData = _lumen.TblRemarks.Where(x => x.StudentId == item.StudentId && x.TermId == termId && x.BatchId == batchId).Select(x => x.Remark).FirstOrDefault();
                StudentTestObtMarks studentTestObtMarks = new StudentTestObtMarks()
                {
                  TestID = 0,
                  TestName = "Teacher Remark",
                  ObtainedMarks = 0,
                  MaximumdMarks = 0,
                  Remark = RemarkData ?? ""
                };
                studentTestObtMarksList.Add(studentTestObtMarks);
              }
              else
              {
                StudentTestObtMarks studentTestObtMarksData = new StudentTestObtMarks()
                {
                  TestID = data.TestId,
                  ObtainedMarks = studentObtainedData?.FirstOrDefault(x => x.TestId == data.TestId)?.ObtainedMarks ?? 0,
                  MaximumdMarks = data.MaximumMarks,
                  TestName = _lumen.TblTests.Where(x => x.TestId == data.TestId).Select(x => x.TestName).FirstOrDefault(),
                  IsElective = electiveSubjectId.Any(x => x.SubjectId == data.SubjectId) && !studentElectiveSubjectIds.Contains(data.SubjectId),
                  IsOptional = (bool)_lumen.TblTests.Where(x => x.TestId == data.TestId).Select(x => x.IsOptional).FirstOrDefault()
                };
                studentTestObtMarksList.Add(studentTestObtMarksData);
              }
            }
            ListStudent listStudent = new ListStudent()
            {
              StudentId = item.StudentId,
              StudentName = _lumen.Students.Where(x => x.StudentId == item.StudentId).Select(x => x.Name).FirstOrDefault(),
              BatchId = Convert.ToInt32(item.BatchId),
              IsFreeze = Freezeitem != null ? Freezeitem.IsFreeze : false

            };
            listStudent.studentTestObtMarks = studentTestObtMarksList;

            listStudents.Add(listStudent);

          }

        }
        else
        {
          foreach (var item in studentlist)
          {
            var studentElectiveSubjectIds = _lumen.TblStudentElectiveRecord.Where(x => x.StudentId == item.StudentId).Select(x => x.ElectiveSubjectId).ToList();
            var IsAlreadyExist1 = _lumen.TblTestRecords.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.TermId == termId && x.BatchId == batchId).ToList();
            var RecordFKID = IsAlreadyExist1.Where(x1 => x1.StudentId == item.StudentId).Select(x2 => x2.RecordId).FirstOrDefault();
            fk.Add(RecordFKID);
            var studentObtainedData = _lumen.TblTestObtainedMarks.Where(x => x.RecordIdfk == RecordFKID).ToList();

            List<StudentTestObtMarks> studentTestObtMarksList = new List<StudentTestObtMarks>();

            foreach (var data in Tests)
            {
              if (data.TestId == 0)
              {
                var RemarkData = _lumen.TblRemarks.Where(x => x.StudentId == item.StudentId && x.TermId == termId && x.BatchId == batchId).Select(x => x.Remark).FirstOrDefault();
                StudentTestObtMarks studentTestObtMarks = new StudentTestObtMarks()
                {
                  TestID = 0,
                  TestName = "Teacher Remark",
                  ObtainedMarks = 0,
                  MaximumdMarks = 0,
                  Remark = RemarkData ?? ""

                };
                studentTestObtMarksList.Add(studentTestObtMarks);
              }
              else
              {
                StudentTestObtMarks studentTestObtMarksData = new StudentTestObtMarks()
                {
                  TestID = data.TestId,
                  ObtainedMarks = studentObtainedData?.FirstOrDefault(x => x.TestId == data.TestId)?.ObtainedMarks ?? 0,
                  MaximumdMarks = data.MaximumMarks,
                  TestName = _lumen.TblTests.Where(x => x.TestId == data.TestId).Select(x => x.TestName).FirstOrDefault(),
                  IsElective = electiveSubjectId.Any(x => x.SubjectId == data.SubjectId) && !studentElectiveSubjectIds.Contains(data.SubjectId),
                  // IsOptional = (bool)_lumen.TblTests.Where(x => x.TestId == data.TestId).Select(x => x.IsOptional).FirstOrDefault()
                  IsOptional = (bool)_lumen.TblTests.Where(x => x.TestId == data.TestId).Select(x => x.IsOptional).FirstOrDefault()
                };
                studentTestObtMarksList.Add(studentTestObtMarksData);
              }
            }
            ListStudent listStudent = new ListStudent()
            {
              StudentId = item.StudentId,
              StudentName = item.Name,
              BatchId = item.BatchId,
              IsFreeze = Freezeitem != null ? Freezeitem.IsFreeze : false

            };
            listStudent.studentTestObtMarks = studentTestObtMarksList;

            listStudents.Add(listStudent);

          }
        }


        var result = new
        {
          IsUpdate = false,
          data = listStudents.OrderBy(x => x.StudentName),
          HeaderData = Tests
        };
        if (result != null)
        {
          res.Data = result;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "400";

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

  public async Task<IApiResponse> ReportCard(int classId, int sectionId, int termId, int batchId)
  {
    IApiResponse res = new ApiResponse();
    List<ReportCardModel> listStudents = new List<ReportCardModel>();
    try
    {
      DynamicParameters objparams = new DynamicParameters();
      objparams.Add("ClassID", classId);
      objparams.Add("SectionID", sectionId);
      objparams.Add("TermID", termId);
      objparams.Add("BatchID", batchId);
      listStudents = await DBFactory.SelectCommand_SP(listStudents, "sp_GetReportCardData", objparams);
      if (listStudents.Count > 0)
      {
        res.Data = listStudents;
        res.Msg = "Get  data successfully";
        res.ResponseCode = "200";
      }
      else
      {
        res.Data = "";
        res.Msg = "data not found";
        res.ResponseCode = "400";
      }

      //IQueryable<Student> query = _lumen.Students.Where(x => !x.IsApplyforTc);

      //await Task.Run(() =>
      //{

      //  if (classId != 0 && sectionId != 0)
      //  {
      //    query = query.Where(x => x.ClassId == classId && x.SectionId == sectionId);
      //  }

      //  if (batchId != 0)
      //  {
      //    query = query.Where(x => x.BatchId == batchId);
      //  }
      //  var studentlist = new List<Student>();
      //  if (termId != 10)
      //  {
      //    studentlist = (from student in query
      //                   join testRecord in _lumen.TblTestRecords
      //                   on student.StudentId equals testRecord.StudentId
      //                   where testRecord.TermId == termId
      //                   select student)
      //               .Distinct()
      //               .ToList();

      //  }
      //  else
      //  {
      //    studentlist = (from student in query
      //                   join testRecord in _lumen.TblTestRecords
      //                   on student.StudentId equals testRecord.StudentId
      //                   select student)
      //               .Distinct()
      //               .ToList();

      //  }

      //  foreach (var item in studentlist)
      //  {
      //    item.Section = _lumen.TblDataListItems.Where(x => x.DataListItemId == sectionId).Select(x => x.DataListItemName).FirstOrDefault();
      //    ListStudent listStudent = new ListStudent()
      //    {
      //      StudentId = item.StudentId,
      //      ClassName = _lumen.TblDataListItems.Where(x => x.DataListItemId == item.ClassId).Select(x => x.DataListItemName).FirstOrDefault(),
      //      SectionName = item.Section,
      //      StudentName = item.Name,
      //      //BatchName = item.BatchName,  //---x-rnik--
      //      //BatchName = _lumen.TblBatches.Where(x => x.BatchId == item.BatchId).FirstOrDefault().BatchName,
      //      BatchName = _lumen.TblBatches.Where(x => x.BatchId == item.BatchId).Select(s => s.BatchName).FirstOrDefault(),
      //      ObtainedMarks = 0
      //    };
      //    listStudents.Add(listStudent);
      //  }
      //  //res = listStudents.OrderBy(s=>s.StudentName).ToList();
      //  if (listStudents.Count > 0)
      //  {
      //    res.Data = listStudents.OrderBy(x => x.StudentName);
      //    res.Msg = "Get  data successfully";
      //    res.ResponseCode = "200";
      //  }
      //  else
      //  {
      //    res.Data = new StafsDetail();
      //    res.Msg = "data Found";
      //    res.ResponseCode = "400";
      //  }
      //});
      //   return res;
      //return Json(listStudents.OrderBy(x => x.StudentName), JsonRequestBehavior.AllowGet);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }

  public async Task<IApiResponse> GradAll(int classnumber, int term)
  {
    int Batch_Id = 0;
    // HttpContext context = new HttpContext();
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
     {
       var school = _lumen.TblCreateSchools.FirstOrDefault();
       var filename = System.IO.Path.GetFileName(school!.UploadImage);
       var base64Image = ConvertImageToBase64("WebsiteImages/SchoolImage/" + filename);
       Batch_Id = term;
       var School_logo = base64Image;
       var SchoolNewName = school.SchoolName;
       var current_Year = _lumen.TblBatches.Where(x => x.BatchId == Batch_Id).Select(x => x.BatchName).FirstOrDefault()!.Split('-')[0];
       var newAddress = school.Address;
       //var data = new PdfDocument();
       string htmlContent = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Document</title>

    <link href=""https://fonts.googleapis.com/css2?family=Lora:wght@400;500;600;700&family=Neuton:wght@300;400;700&display=swap""
          rel=""stylesheet"" />
    <link rel=""stylesheet""
          href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" />
    <link rel=""stylesheet"" href=""~/Content/WebsiteCss/css/style.css"" />
    <style>

        #printT {
            border: 1px solid #ccc;
            padding: 20px;
            margin: 20px;
            width: 7.5in; /* Adjusted width to fit within A4 width */
            height: 10.7in; /* Adjusted height to fit within A4 height */
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            /*page-break-after: always;*/ /* Force a new page after each card */
        }
        /* Styles for the loader overlay */
        #loader-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black */
            display: none;
            justify-content: center;
            align-items: center;
            z-index: 1000; /* Make sure the overlay is on top */
        }

        .loader {
            border: 4px solid #f3f3f3; /* Light grey */
            border-top: 4px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 2s linear infinite;
        }

        @@keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        @@media print {
            #printT {
                page-break-after: auto; /* or avoid specifying page-break-after */
            }
        }
    </style>

</head>
<body>
    <div id=""loader-overlay"">
        <div class=""loader""></div>
    </div>
    <button id=""printButton"">Print</button>
    @*&nbsp;&nbsp;<button id=""downloadButton"">Download</button>*@

    <div class=""d-flex justify-content-center align-items-center h-100"" id=""JsonPrint"">


        <div class=""d-flex flex-column"" id=""printColumns"">
            <div class=""border border-dark mt-3"" id=""printT"">
                <!-- Header section  -->
                <section class=""d-flex justify-content-between align-items-center "">
                    <div class=""schoollogo1"" style=""width: 21%"">
                        <img src=""data:image/jpeg;base64,@ViewBag.School_logo""
                             alt=""notfound""
                             style=""width: 120px; height: 115px; margin-bottom: 11px;"" />
                    </div>
                    <div class=""headerContent text-center "" style=""width:58%"">
                        <div class=""ft11 mb-0"" style=""font-size: 1.5rem; font-weight: bold;""><b>@ViewBag.SchoolNewName</b></div>

                        <p class=""ft13 mb-0"">
                            @ViewBag.newAddress
                        </p>


                        <div class=""ft13""><b>Academic Session : @ViewBag.current_Year</b></div>
                        <div class=""ft13""><b>Report Card</b></div>
                    </div>
                    <div class=""schoollogo1 "" style=""width:21%"">
                        <img src=""~/Content/Images/logo3.png""
                             alt=""notfound""
                             style=""width: 120px; height: 103px;margin-left: 13px;"" />
                        <span class=""ft14"">CISCE Affiliation No. : MP034</span>
                    </div>
                </section>
                <hr class=""m-0"" />

                <!-- students details  -->
                <section class=""ft15 ps-1 my-1"">
                    <div class=""row"">
                        <div class=""col-4"" style=""width: 60%"">
                            <span style=""margin-right: 5px""><b> Student's Name</b></span>
                            <span class=""text-uppercase"">
                                : <span style=""border-bottom: 1px dashed"" id=""StudentName""></span>
                            </span>
                        </div>

                        <div class=""col-4"" style=""width: 40%"">
                            <span><b>Class & Section</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""ClassSection""></span></span>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-4"" style=""width: 60%"">
                            <span style=""margin-right: 11px""><b>Father's Name</b> </span>
                            <span class=""text-uppercase""> : <span style=""border-bottom: 1px dashed"" id=""FatherName""></span> </span>
                        </div>

                        <div class=""col-4"" style=""width: 40%"">
                            <span style=""margin-right: 16px""><b>Date of Birth</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""DateOfBirth""></span> </span>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-8"" style=""width: 60%"">
                            <span style=""margin-right: 6px""><b>Mother's Name</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""MotherName""></span></span>
                        </div>
                        <div class=""col-4"" style=""width: 40%"">
                            <span style=""margin-right: 27px""><b>Attendance</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""Attendance""></span></span>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-4"" style=""width: 60%"">
                            <span style=""margin-right: 44px""><b>Roll No.</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""RollNo""></span></span>
                        </div>
                        <div class=""col-4"" style=""width: 40% "">
                            <span style=""margin-right: 26px""><b>Scholar No.</b></span>
                            <span class=""text-uppercase"">: <span style=""border-bottom: 1px dashed"" id=""ScholarNo""></span></span>
                        </div>
                    </div>
                </section>

                <!-- marks section  -->
                <table class=""text-center my-2 ft15"" id=""MarksTable"">
                    <tr>
                        <th style=""width: 180px;"">Scholastic Areas:</th>
                        <th colspan=""3"" scope=""colgroup"">Unit Test</th>
                        <th colspan=""4"" scope=""colgroup"">TERM-I</th>
                        <th colspan=""4"" scope=""colgroup"">TERM-II</th>
                        <th colspan=""4"" class=""preBoardResult"" scope=""colgroup"">PreBoard-I</th>
                        <th colspan=""4"" class=""preBoardResult"" scope=""colgroup"">PreBoard-II</th>
                        <th colspan=""2"" scope=""colgroup"">Total</th>
                    </tr>
                    <tr>
                        <th scope=""col"">Subjects</th>

                        <th scope=""col"">UT I <span id=""UT1MaxMarkTotal""></span></th>
                        <th scope=""col"">UT II <span id=""UT2MaxMarkTotal""></span></th>
                        <th scope=""col"">Marks Obt.  <span id=""UTTotalMaxMarkTotal""></span></th>

                        <th scope=""col"">Theory <span class=""Term1TheoryMaxMarkTotal""></span></th>
                        <th scope=""col"">Practical <span id=""Term1PracticalMaxMarkTotal""></span></th>
                        <th scope=""col"">Marks Obt. <span id=""Term1TotalMaxMarkTotal""></span></th>
                        <th scope=""col"">Grade</th>

                        <th scope=""col"">Theory<span id=""Term2TheoryMaxMarkTotal""></span></th>
                        <th scope=""col"">Practical <span id=""Term2PracticalMaxMarkTotal""></span></th>
                        <th scope=""col"">Marks Obt. <span id=""Term2TotalMaxMarkTotal""></span></th>
                        <th scope=""col"">Grade</th>

                        <th class=""preBoardResult"" scope=""col"">Theory<span id=""PreBoard1TheoryMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Practical <span id=""PreBoard1PracticalMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Marks Obt. <span id=""PreBoard1TotalMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Grade</th>

                        <th class=""preBoardResult"" scope=""col"">Theory<span id=""PreBoard2TheoryMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Practical <span id=""PreBoard2PracticalMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Marks Obt. <span id=""PreBoard2TotalMaxMarkTotal""></span></th>
                        <th class=""preBoardResult"" scope=""col"">Grade</th>

                        <th scope=""col"">Marks</th>
                        <th scope=""col"">Grade</th>
                    </tr>
                </table>

                <!-- UT marks section  -->
                <table class=""text-center my-2 ft15"" id=""UnitRecord"">
                    <tr>
                        <th style=""width: 180px;"">Scholastic Areas:</th>
                        <th colspan=""2"" scope=""colgroup"">Unit Test</th>
                    </tr>
                    <tr>
                        <th scope=""col"">Subjects</th>
                        <th scope=""col"">UT <span id=""UnitName""></span> <span id=""UTMaxMarkTotal""></span> </th>
                        <th scope=""col"">Grade</th>
                    </tr>
                </table>
                <!-- Terms marks section  -->
                <table class=""text-center my-2 ft15"" id=""TermRecord"">
                    <tr>
                        <th style=""width: 180px;"">Scholastic Areas:</th>
                        <th colspan=""4"" scope=""colgroup"">TERM-<span id=""TermName""></span></th>
                    </tr>
                    <tr>
                        <th scope=""col"">Subjects</th>
                        <th scope=""col"">Theory <span class=""Term1TheoryMaxMarkTotal""></span></th>
                        <th scope=""col"">Practical <span id=""Term1TPracticalMaxMarkTotal""></span></th>
                        <th scope=""col"">Marks Obt. <span id=""Term1TAllTotalMaxMarkTotal""></span></th>
                        <th scope=""col"">Grade</th>


                    </tr>
                </table>
                <!-- Pre Board marks section  -->
                <table class=""text-center my-2 ft15"" id=""PreBoardRecord"">
                    <tr>
                        <th style=""width: 180px;"">Scholastic Areas:</th>
                        <th colspan=""4"" scope=""colgroup"">Pre Board-<span id=""PreBoardTermName""></span></th>
                    </tr>
                    <tr>
                        <th scope=""col"">Subjects</th>
                        <th scope=""col"">Theory <span class=""PreBoardTheoryMaxMarkTotal""></span></th>
                        <th scope=""col"">Practical <span id=""PreBoardTPracticalMaxMarkTotal""></span></th>
                        <th scope=""col"">Marks Obt. <span id=""PreBoardTAllTotalMaxMarkTotal""></span></th>
                        <th scope=""col"">Grade</th>


                    </tr>
                </table>

                <!-- Grading scale  -->
                <table class=""text-center ft15"" id=""coScholasticTable"">
                    <tr>
                        <th colspan=""6"" scope=""colgroup"">
                            Co-Scholastic Areas [on a 4-point (A-NA) grading scale]
                        </th>
                    </tr>

                    <tr>
                        <th style=""width: 300px;"">Co-Scholastic Areas</th>
                        <th class=""headerTermColumn headerUT1Column"" style=""width: 65px;"">UT-I</th>
                        <th class=""headerTermColumn headerUT2Column"" style=""width:65px;"">UT-II</th>
                        <th class=""headerTermColumn headerTerm1Column"" style=""width: 65px;"">TERM-I</th>
                        <th class=""headerTermColumn headerTerm2Column"" style=""width:65px;"">TERM-II</th>
                        <th class=""headerTermColumn headerPreBoard1Column preBoardResult"" style=""width:65px;"">Pre Board-I</th>
                        <th class=""headerTermColumn headerPreBoard2Column preBoardResult"" style=""width:65px;"">Pre Board-II</th>


                        <th style=""width: 300px;"">Co-Scholastic Areas</th>
                        <th class=""headerTermColumn headerUT1Column"" style=""width: 65px;"">UT-I</th>
                        <th class=""headerTermColumn headerUT2Column"" style=""width:65px;"">UT-II</th>
                        <th class=""headerTermColumn headerTerm1Column"" style=""width:65px;"">TERM-I</th>
                        <th class=""headerTermColumn headerTerm2Column"" style=""width:65px;"">TERM-II</th>
                        <th class=""headerTermColumn headerPreBoard1Column preBoardResult"" style=""width:65px;"">Pre Board-I</th>
                        <th class=""headerTermColumn headerPreBoard2Column preBoardResult"" style=""width:65px;"">Pre Board-II</th>

                    </tr>


                </table>




                <!-- Result section  -->
                <div class=""d-flex justify-content-between align-items-center px-1"">
                    <div class=""ft15"">
                        <span><b>Result :</b></span>
                        <span id=""result""></span>
                    </div>
                    <div class=""ft15"">
                        <span><b>Overall Grade :</b></span>
                        <span id=""overallGrade""></span>
                    </div>
                </div>
                <div class=""ft15 px-1"" id=""teacherRemark"">
                    <span><b>Class Teacher's Remarks : </b></span>
                    <span><span id=""teacherRemarkText""></span>  <span id=""promotedClass""></span> </span>
                </div>
                <div class=""ft15 px-1"" id=""UnitTestRemark"">
                    <span><b>Class Teacher's Remarks : </b></span>
                    <span id=""UnitTestRemarkText"">. </span>
                </div>
                <!-- signature section  -->
                <div class=""d-flex justify-content-between align-items-baseline px-3"">
                    <div class=""signature"">
                        <img src="""" alt="""" />
                        <span class=""ft15""><b>Parent / Guardian</b></span>
                    </div>
                    <div class=""signature"">
                        <img src="""" id=""teachersignature"" style=""width:115px; height:60px;"" alt="""" />
                        <span class=""ft15""><b>Class Teacher</b></span>
                    </div>
                    <div class=""signature"">
                        <img src=""~/Content/Images/principle.png"" style=""width:115px; height:60px;"" alt="""" />
                        <span class=""ft15""><b>Principal</b></span>
                    </div>
                </div>

                <div class=""ft15 px-1 mt-2"">
                    <span><b>Grading scale for Scholastic Areas : </b></span>
                    <span>Grades are awarded on a 6-point grading scale as follows -</span>
                </div>

                <!-- marks range section  -->
                <table class=""text-center ft15"" id=""GradingCriteria"">
                    <tbody></tbody>
                    @*<tr>
                        <th>Range (%)</th>
                        <td><span id=""dgrade""></span></td>


                        @foreach (var item in ViewBag.gradingCriteria)
                        {
                            <td>@item.MinimumPercentage - @item.MaximumPercentage</td>
                        }
                        <td>32.5 - 49.4</td>
                            <td>49.5 - 64.4</td>
                            <td>64.5 - 74.4</td>
                            <td>74.5 - 89.4</td>
                            <td>89.5 - 100</td>
                    </tr>
                    <tr>
                        <th>Grade</th>
                        @foreach (var item in ViewBag.gradingCriteria)
                        {
                            <td>@item.Grade</td>
                        }
                        <td>D (Failed)</td>
                        <td>C</td>
                        <td>B</td>
                        <td>B+</td>
                        <td>A</td>
                        <td>A+</td>
                    </tr>*@

                    @*<tr>
                        <td colspan=""7"" scope=""colgroup"" class=""text-start ps-1 ps-1"">
                            <div class=""ft15"">
                                4.
                                N.B. There will be no second attempt for English Language and
                                Literature. Second attempt is allowed for one subject provided
                                the score is below 35.
                            </div>
                            <div class=""ft15"">Minimum mark for passing is 35.</div>
                            <div class=""ft15"">** It Indicates Improvement Exam.</div>
                        </td>
                    </tr>*@
                </table>

                <!-- End  -->
            </div>
        </div>
    </div>

    <script src=""https://code.jquery.com/jquery-3.7.0.slim.js"" integrity=""sha256-7GO+jepT9gJe9LB4XFf8snVOjX3iYNb0FHYr5LI1N5c="" crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js""></script>
    <script src=""https://html2canvas.hertzen.com/dist/html2canvas.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.3/html2pdf.bundle.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js""></script>
    <script src=""~/Scripts/DevelopmentJS/PrintReport.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"" integrity=""sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>

    <script>
        // Function to handle the printing
        function printDiv() {
            const printContent = document.getElementById(""JsonPrint"");
            const originalContent = document.body.innerHTML;

            document.body.innerHTML = printContent.outerHTML;
            window.print();

            document.body.innerHTML = originalContent;
        }

        // Attach click event to the 'Print' button
        const printButton = document.getElementById(""printButton"");
        printButton.addEventListener(""click"", printDiv);

    </script>
</body>

<!-- Add the 'Print' button -->



</html>";
       var globalSettings = new GlobalSettings
       {
         ColorMode = ColorMode.Color,
         Orientation = Orientation.Portrait,
         PaperSize = PaperKind.A4,
         Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
       };

       var objectSettings = new ObjectSettings
       {
         PagesCount = true,
         HtmlContent = htmlContent,
         WebSettings = { DefaultEncoding = "utf-8" },
         HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
         FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
       };

       var pdf = new HtmlToPdfDocument()
       {
         GlobalSettings = globalSettings,
         Objects = { objectSettings }
       };

       byte[] pdfBytes = _converter.Convert(pdf);
       File.WriteAllBytes("ConvertedDocument.pdf", pdfBytes);
       string base64String = Convert.ToBase64String(pdfBytes);
       //var _file = new System.IO.FileStream();
       //var result = new File(pdfBytes, "application/pdf", "ConvertedDocument.pdf");//_lumen.GradingCriterias.ToList();
       var result = base64String;
       if (result != null)
       {
         res.Data = result;
         res.Msg = "Get  data successfully";
         res.ResponseCode = "200";
       }
       else
       {
         res.Data = new StafsDetail();
         res.Msg = "data Found";
         res.ResponseCode = "400";

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


  public async Task<IApiResponse> FillCoScholasticAreasRepor(FillCoScholasticAreasReporModel _objmodel)
  {
    IApiResponse res = new ApiResponse();
    List<CoScholasticListStudent> listStudents = new List<CoScholasticListStudent>();
    try
    {
      await Task.Run(() =>
      {
        var getBoardID = _lumen.TblCreateSchools.Select(x => x.BoardId).FirstOrDefault();
        var IsAlreadyExist = _lumen.TblCoScholasticResults.Where(x => x.ClassId == _objmodel.classId && x.SectionId == _objmodel.sectionId && x.TermId == _objmodel.termId).ToList();
        if (IsAlreadyExist.Count() > 0)
        {
          List<TblCoScholastic> coScholastic;
          CoScholasticStudentGrid((long)getBoardID!, _objmodel.termId, _objmodel.classId, _objmodel.sectionId, _objmodel.batchId, out listStudents, out coScholastic, IsAlreadyExist);
          var result = new
          {
            IsUpdate = false,
            data = listStudents.OrderBy(x => x.StudentName),
            HeaderData = coScholastic
          };
          if (result != null)
          {
            res.Data = result;
            res.Msg = "Get  data successfully";
            res.ResponseCode = "200";
          }
          else
          {
            res.Data = new StafsDetail();
            res.Msg = "data Found";
            res.ResponseCode = "400";

          }
          //return result;
        }
        else
        {


          List<TblCoScholastic> coScholastic;
          CoScholasticStudentGrid((long)getBoardID!, _objmodel.termId, _objmodel.classId, _objmodel.sectionId, _objmodel.batchId, out listStudents, out coScholastic, new List<TblCoScholasticResult>());
          var result = new
          {
            IsUpdate = false,
            data = listStudents.OrderBy(x => x.StudentName),
            HeaderData = coScholastic
          };

          if (result != null)
          {
            res.Data = result;
            res.Msg = "Get  data successfully";
            res.ResponseCode = "200";
          }
          else
          {
            res.Data = new StafsDetail();
            res.Msg = "data Found";
            res.ResponseCode = "400";

          }
          //return result;
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
  public void CoScholasticStudentGrid(long getBoardID, int Termid, int classId, int sectionId, int batchid, out List<CoScholasticListStudent> listStudents, out List<TblCoScholastic> coScholastic, List<TblCoScholasticResult> IsAlreadyExist)
  {


    IsAlreadyExist = _lumen.TblCoScholasticResults.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.TermId == Termid).ToList();

    listStudents = new List<CoScholasticListStudent>();
    var coScholasticClassList = _lumen.TblCoScholasticClasses
                              .Where(x => x.BoardId == getBoardID && x.ClassId == classId)
                              .ToList();

    coScholastic = coScholasticClassList.Select(item => new TblCoScholastic
    {
      Id = _lumen.TblCoScholastics.FirstOrDefault(x => x.Id == item.CoscholasticId)?.Id,
      Title = _lumen.TblCoScholastics.FirstOrDefault(x => x.Id == item.CoscholasticId)?.Title
      // Title = _context.tbl_CoScholastic.FirstOrDefault(x => x.Id == item.Id)?.Title
    }).ToList();

    if (IsAlreadyExist.Count() > 0)
    {
      var _studentlist = _lumen.Students.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.IsApplyforTc == false && x.BatchId == batchid).OrderBy(x => x.Name).ToList();

      var stdList = _lumen.TblCoScholasticResults.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.TermId == Termid).ToList();
      //var stdList = from cos  in _context.tbl_CoScholastic_Result join
      //              st  in _studentlist on cos.StudentID equals st.StudentId where cos.ClassID = classId  cos.SectionId == sectionId
      //              && cos.TermID=termId && st.Batch_Id==batchId

      //var stdList = (from cos in _context.tbl_CoScholastic_Result
      //              join
      //             st in _studentlist on cos.StudentID equals  st.StudentId select new
      //             {
      //                 cos.Id,cos.StudentID,cos.CoScholasticID,cos.BoardID,cos.SectionId,cos.ClassID,
      //                 cos.TermID,st.Batch_Id

      //             })
      //              .Where(x => x.ClassID == classId && x.SectionId == sectionId && x.TermID == termId && x.Batch_Id==batchId).ToList();
      if (_studentlist.Count == 0)
      {
        foreach (var item in IsAlreadyExist)
        {
          item.SectionId = item.SectionId;//_context.DataListItems.Where(x => x.DataListItemId == sectionId).Select(x => x.DataListItemName).FirstOrDefault();
          CoScholasticListStudent listStudent = new CoScholasticListStudent()
          {
            StudentId = item.StudentId,
            //StudentName = _context.Students.Where(x => x.StudentId == item.StudentID && x.Batch_Id == batchId && x.Section_Id == sectionId).Select(x => x.Name).FirstOrDefault()
            StudentName = _lumen.Students
    .Where(x => x.StudentId == (item.StudentId != null ? Convert.ToInt32(item.StudentId) : 0))
    .Select(x => x.Name)
    .FirstOrDefault() ?? string.Empty

          };
          listStudents.Add(listStudent);
          List<CoscholastiStuentObtData> coscholastiStuentObtDataList = new List<CoscholastiStuentObtData>();
          List<TblCoScholasticObtainedGrade> commonItems = new List<TblCoScholasticObtainedGrade>();
          string? currentObtainedGrade = "";

          if (IsAlreadyExist.Count() > 0)
          {
            var ObtainedCoScholasticID = IsAlreadyExist.Where(x1 => x1.StudentId == item.StudentId).Select(x2 => x2.Id).FirstOrDefault();
            var coScholasticClassListNew = _lumen.TblCoScholasticClasses
                            .Where(x => x.BoardId == getBoardID && x.ClassId == classId)
                            .ToList();
            var studentObtainedData = _lumen.TblCoScholasticObtainedGrades.Where(x => x.ObtainedCoScholasticId == ObtainedCoScholasticID && x.batchId == batchid).ToList();
            commonItems = (from studentData in studentObtainedData
                           join classData in coScholasticClassListNew
                           on studentData.CoscholasticId equals classData.CoscholasticId
                           select studentData).ToList();

          }

          foreach (var data in coScholastic)
          {
            if (IsAlreadyExist.Count() > 0)
            {
              currentObtainedGrade = commonItems.Where(a => a.CoscholasticId == data.Id).Select(a => a.ObtainedGrade).FirstOrDefault();
            }

            CoscholastiStuentObtData coscholastiStuentObtData = new CoscholastiStuentObtData()
            {
              CoscholasticID = data.Id ?? 0,
              ObtainedGrade = currentObtainedGrade ?? string.Empty,
              CoscholasticName = data.Title ?? string.Empty
            };
            coscholastiStuentObtDataList.Add(coscholastiStuentObtData);
          }
          listStudent.coscholastiStuentObtDatas = coscholastiStuentObtDataList;
        }

      }

      foreach (var item in _studentlist)
      {
        item.SectionId = item.SectionId;//_context.DataListItems.Where(x => x.DataListItemId == sectionId).Select(x => x.DataListItemName).FirstOrDefault();
        CoScholasticListStudent listStudent = new CoScholasticListStudent()
        {
          StudentId = item.StudentId,
          StudentName = _lumen.Students.Where(x => x.StudentId == item.StudentId && x.BatchId == batchid && x.SectionId == sectionId && x.ClassId == classId).Select(x => x.Name).FirstOrDefault() ?? string.Empty,
        };
        listStudents.Add(listStudent);
        List<CoscholastiStuentObtData> coscholastiStuentObtDataList = new List<CoscholastiStuentObtData>();
        List<TblCoScholasticObtainedGrade> commonItems = new List<TblCoScholasticObtainedGrade>();
        string currentObtainedGrade = "";

        if (IsAlreadyExist.Count() > 0)
        {
          var ObtainedCoScholasticID = IsAlreadyExist.Where(x1 => x1.StudentId == item.StudentId).Select(x2 => x2.Id).FirstOrDefault();
          var coScholasticClassListNew = _lumen.TblCoScholasticClasses
                          .Where(x => x.BoardId == getBoardID && x.ClassId == classId)
                          .ToList();
          var studentObtainedData = _lumen.TblCoScholasticObtainedGrades.Where(x => x.ObtainedCoScholasticId == ObtainedCoScholasticID && x.batchId == batchid).ToList();
          commonItems = (from studentData in studentObtainedData
                         join classData in coScholasticClassListNew
                         on studentData.CoscholasticId equals classData.CoscholasticId
                         select studentData).ToList();
        }

        foreach (var data in coScholastic)
        {
          if (IsAlreadyExist.Count() > 0)
          {
            currentObtainedGrade = commonItems.Where(a => a.CoscholasticId == data.Id).Select(a => a.ObtainedGrade).FirstOrDefault() ?? string.Empty;
          }

          CoscholastiStuentObtData coscholastiStuentObtData = new CoscholastiStuentObtData()
          {
            CoscholasticID = data.Id ?? 0,
            ObtainedGrade = currentObtainedGrade,
            CoscholasticName = data.Title ?? string.Empty
          };
          coscholastiStuentObtDataList.Add(coscholastiStuentObtData);
        }
        listStudent.coscholastiStuentObtDatas = coscholastiStuentObtDataList;





      }
    }
    else
    {
      var studentlist = _lumen.Students.Where(x => x.ClassId == classId && x.SectionId == sectionId && x.IsApplyforTc == false && x.BatchId == batchid).OrderBy(x => x.Name).ToList();
      foreach (var item in studentlist)
      {
        item.Section = _lumen.TblDataListItems.Where(x => x.DataListItemId == sectionId).Select(x => x.DataListItemName).FirstOrDefault();
        CoScholasticListStudent listStudent = new CoScholasticListStudent()
        {
          StudentId = item.StudentId,
          StudentName = item.Name
        };
        listStudents.Add(listStudent);
        List<CoscholastiStuentObtData> coscholastiStuentObtDataList = new List<CoscholastiStuentObtData>();
        List<TblCoScholasticObtainedGrade> commonItems = new List<TblCoScholasticObtainedGrade>();
        string currentObtainedGrade = "";

        if (IsAlreadyExist.Count() > 0)
        {
          var ObtainedCoScholasticID = IsAlreadyExist.Where(x1 => x1.StudentId == item.StudentId).Select(x2 => x2.Id).FirstOrDefault();
          var coScholasticClassListNew = _lumen.TblCoScholasticClasses
                          .Where(x => x.BoardId == getBoardID && x.ClassId == classId)
                          .ToList();
          var studentObtainedData = _lumen.TblCoScholasticObtainedGrades.Where(x => x.ObtainedCoScholasticId == ObtainedCoScholasticID).ToList();
          commonItems = (from studentData in studentObtainedData
                         join classData in coScholasticClassListNew
                         on studentData.CoscholasticId equals classData.CoscholasticId
                         select studentData).ToList();
        }

        foreach (var data in coScholastic)
        {
          if (IsAlreadyExist.Count() > 0)
          {
            currentObtainedGrade = commonItems.Where(a => a.CoscholasticId == data.Id).Select(a => a.ObtainedGrade).FirstOrDefault() ?? string.Empty;
          }

          CoscholastiStuentObtData coscholastiStuentObtData = new CoscholastiStuentObtData()
          {
            CoscholasticID = data.Id ?? 0,
            ObtainedGrade = currentObtainedGrade,
            CoscholasticName = data.Title ?? string.Empty
          };
          coscholastiStuentObtDataList.Add(coscholastiStuentObtData);
        }
        listStudent.coscholastiStuentObtDatas = coscholastiStuentObtDataList;
      }
    }

  }

  public string ConvertImageToBase64(string imagePath)
  {
    byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
    string base64Image = Convert.ToBase64String(imageArray);
    return base64Image;
  }

public async Task<IApiResponse> HoldUnHoldStudentReportCard(int StudentId, int term, int Batch, int classid, string Remark, bool isHold)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      // First, check if a record already exists with the given criteria
      var existingRecord = _lumen.Tbl_HoldDetail
 .Where(x => x.StudentId == StudentId && x.TermId == term && x.BatchId == Batch && x.ClassId == classid)
 .FirstOrDefault();
      if (existingRecord != null)
      {
        existingRecord.IsHold = existingRecord.IsHold ? false : true;
       await _lumen.SaveChangesAsync();
      }
      else
      {
        // If no record is found, insert a new record
        var newHoldDetail = new Tbl_HoldDetail
        {
          StudentId = StudentId,
          TermId = term,
          BatchId = Batch,
          ClassId = classid,
          Remark = Remark,
          IsHold = isHold,
          HoldDate = DateTime.Now,
          // Add appropriate date-time values
        };
        _lumen.Tbl_HoldDetail.Add(newHoldDetail);
      await  _lumen.SaveChangesAsync();
      }
      res.Msg = "Result Hold successfully";
      res.ResponseCode = "200";
      // return Json(new { success = true, message = "Operation completed successfully." }, JsonRequestBehavior.AllowGet);
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
      //return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
    }

    return res;
  }
  #region Fill Mark
  //public async Task<IApiResponse> SaveFillmarkReport(List<StudentObtainedMark> rowData)
  //{
  //  IApiResponse res = new ApiResponse();
  //  List<ListStudent> listStudents = new List<ListStudent>();
  //  try
  //  {

  //    await Task.Run(() =>
  //    {
  //      IQueryable<Student> query = _lumen.Students.Where(x => !x.IsApplyforTc);
  //      var getBoardID = _lumen.TblCreateSchools.Select(x => x.BoardId).FirstOrDefault();

  //      TblTestObtainedMark _obj = new TblTestObtainedMark();
  //      foreach (var item in rowData)
  //      {
  //        var existingRecord = _lumen.TblTestRecords.FirstOrDefault(tr => tr.TermId == item.TermID && tr.StudentId == item.StudentID && tr.ClassId == item.ClassID && tr.SectionId == item.SectionId && tr.BoardId == getBoardID && tr.BatchId == item.BatchId);
  //        if (existingRecord != null)
  //        {
  //          bool IsClassTeacher = _lumen.Subjects.Any(x => x.ClassId == item.ClassID && x.StaffId == item.staffId && x.SectionId == item.SectionId && x.ClassTeacher == true);
  //          if (IsClassTeacher)
  //          {
  //            var existingRemark = _lumen.TblRemarks.FirstOrDefault(r => r.TermId == item.TermID && r.StudentId == item.StudentID && r.BoardId == getBoardID && r.BatchId == item.BatchId);

  //            if (existingRemark != null)
  //            {
  //              existingRemark.Remark = item.Remark;
  //            }
  //            else
  //            {
  //              var newRemark = new TblRemark
  //              {
  //                TermId = item.TermID,
  //                StudentId = item.StudentID,
  //                BoardId = getBoardID,
  //                Remark = item.Remark,
  //                BatchId = item.BatchId
  //              };

  //              _lumen.TblRemarks.Add(newRemark);
  //            }
  //            _lumen.SaveChanges();
  //          }
  //          new TblTestObtainedMarks();
  //          foreach (var Dt in item.ObtainedMarkData!)
  //          {
  //            var existingData = _lumen.TblTestObtainedMarks.FirstOrDefault(data => data.RecordIdfk == existingRecord.RecordId && data.TestId == Dt.TestId);
  //            if (existingData != null)
  //            {
  //              existingData.ObtainedMarks = Dt.ObtainedMarks;
  //              existingData.TestId = Dt.TestId;
  //              var result = _lumen.TblTestObtainedMarks.Update(existingData);
  //            }
  //            else
  //            {
  //              Dt.RecordIdfk = existingRecord.RecordId;
  //              _obj.TestId = Dt.TestId;
  //              _obj.ObtainedMarks = Dt.ObtainedMarks;
  //              _obj.RecordIdfk = Dt.RecordIdfk;
  //              _obj.Id = Dt.Id;
  //              var result = _lumen.TblTestObtainedMarks.Add(_obj);
  //            }
  //          }
  //          existingRecord.BatchId = item.BatchId;
  //          _lumen.SaveChangesAsync();

  //        }
  //        else
  //        {
  //          item.BoardID = (long)getBoardID!;
  //          bool IsClassTeacher = _lumen.Subjects.Any(x => x.ClassId == item.ClassID && x.StaffId == item.staffId && x.SectionId == item.SectionId && x.ClassTeacher == true);
  //          if (IsClassTeacher)
  //          {
  //            var existingRemark = _lumen.TblRemarks.FirstOrDefault(r => r.TermId == item.TermID && r.StudentId == item.StudentID && r.BoardId == getBoardID);

  //            if (existingRemark != null)
  //            {
  //              existingRemark.Remark = item.Remark;
  //            }
  //            else
  //            {
  //              // Create a new Remark entity and add it to the context
  //              TblRemark tbl_Remark = new TblRemark()
  //              {
  //                TermId = item.TermID,
  //                BoardId = getBoardID,
  //                StudentId = item.StudentID,
  //                Remark = item.Remark
  //              };
  //              _lumen.TblRemarks.Add(tbl_Remark);
  //            }

  //            // Save changes to the context

  //          }
  //          TblTestRecord testRecords = new TblTestRecord()
  //          {
  //            BoardId = getBoardID,
  //            TermId = item.TermID,
  //            StudentId = item.StudentID,
  //            ClassId = item.ClassID,
  //            SectionId = item.SectionId,
  //            BatchId = item.BatchId,
  //            TestId = 0
  //          };
  //          _lumen.TblTestRecords.Add(testRecords);
  //          _lumen.SaveChangesAsync();
  //          long latestId = testRecords.RecordId;
  //          foreach (var Dt in item.ObtainedMarkData!)
  //          {
  //            Dt.RecordIdfk = latestId;
  //            _obj.TestId = Dt.TestId;
  //            _obj.ObtainedMarks = Dt.ObtainedMarks;
  //            _obj.RecordIdfk = Dt.RecordIdfk;
  //            _obj.Id = Dt.Id;
  //            _lumen.TblTestObtainedMarks.Add(_obj);
  //          }
  //        }
  //        _lumen.SaveChangesAsync();
  //      }


  //      // Save changes to the database



  //    });
  //    res.Data = true;
  //    res.Msg = "save  data successfully";
  //    res.ResponseCode = "200";
  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  public async Task<IApiResponse> SaveFillmarkReport(List<StudentObtainedMark> rowData)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      long? boardIdNullable = _lumen.TblCreateSchools
                                      .Select(x => x.BoardId)
                                      .FirstOrDefault();
      if (!boardIdNullable.HasValue)
      {
        res.Msg = "Board ID configuration not found.";
        res.ResponseCode = "404";
        res.Data = false;
        return await Task.FromResult(res);
      }
      long getBoardID = boardIdNullable.Value;
      foreach (var item in rowData)
      {
        if (item.ObtainedMarkData == null)
        {
          continue;
        }
        var existingRecord = _lumen.TblTestRecords.FirstOrDefault(tr => 
            tr.TermId == item.TermID &&
            tr.StudentId == item.StudentID &&
            tr.ClassId == item.ClassID &&
            tr.SectionId == item.SectionId &&
            tr.BoardId == getBoardID &&
            tr.BatchId == item.BatchId);
        bool isClassTeacher = _lumen.Subjects.Any(x =>
            x.ClassId == item.ClassID &&
            x.StaffId == item.staffId && 
            x.SectionId == item.SectionId &&
            x.ClassTeacher == true);
        if (isClassTeacher)
        {
          var existingRemark = _lumen.TblRemarks.FirstOrDefault(r => 
              r.TermId == item.TermID &&
              r.StudentId == item.StudentID &&
              r.BoardId == getBoardID &&
              r.BatchId == item.BatchId); 

          if (existingRemark != null)
          {
            if (existingRemark.Remark != item.Remark)
            {
              existingRemark.Remark = item.Remark;
              //existingRemark.Remark = item.Remark;
            }
            _lumen.TblRemarks.Update(existingRemark);
          }
          else if (!string.IsNullOrEmpty(item.Remark))
          {
            var newRemark = new TblRemark 
            {
              TermId = item.TermID,
              StudentId = item.StudentID,
              BoardId = getBoardID,
              Remark = item.Remark,
              BatchId = item.BatchId
            };
            _lumen.TblRemarks.Add(newRemark);
          }
          _lumen.SaveChanges();
        } 
        if (existingRecord != null)
        {
          foreach (var Dt in item.ObtainedMarkData)
          {
            var existingData = _lumen.TblTestObtainedMarks.FirstOrDefault(data =>
                data.RecordIdfk == existingRecord.RecordId && data.TestId == Dt.TestId);

            if (existingData != null)
            {
              if (existingData.ObtainedMarks != Dt.ObtainedMarks)
              {
                existingData.ObtainedMarks = Dt.ObtainedMarks;
                _lumen.TblTestObtainedMarks.Update(existingData);
              }
            }
            else
            {
              var newObtainedMark = new TblTestObtainedMark 
              {
                RecordIdfk = existingRecord.RecordId, 
                TestId = Dt.TestId,
                ObtainedMarks = Dt.ObtainedMarks
              };
              _lumen.TblTestObtainedMarks.Add(newObtainedMark);
            }
          }
          if (existingRecord.BatchId != item.BatchId)
          {
            existingRecord.BatchId = item.BatchId;
          }

        }
        else 
        {
          
          TblTestRecord testRecords = new TblTestRecord()
          {
            BoardId = getBoardID,
            TermId = item.TermID,
            StudentId = item.StudentID,
            ClassId = item.ClassID,
            SectionId = item.SectionId,
            BatchId = item.BatchId,
            TestId = 0 
          };
          _lumen.TblTestRecords.Add(testRecords);

          _lumen.SaveChanges();
          long latestId = testRecords.RecordId;
          foreach (var Dt in item.ObtainedMarkData)
          {
            var newObtainedMark = new TblTestObtainedMark
            {
              RecordIdfk = latestId,
              TestId = Dt.TestId,
              ObtainedMarks = Dt.ObtainedMarks
            };
            _lumen.TblTestObtainedMarks.Add(newObtainedMark);
          }
        } 
        _lumen.SaveChanges(); // Kept synchronous SaveChanges()

        // --- End Processing Single Item ---
      } // End foreach loop

      // All items processed and saved individually
      res.Data = true;
          res.Msg = "save  data successfully";
         res.ResponseCode = "200";
      return await Task.FromResult(res); // Added await Task.FromResult()
    }
    catch (DbUpdateException dbEx) // Catch specific EF Core update exceptions
    {
      // Log the detailed exception (dbEx.ToString()) and InnerException
      Console.WriteLine($"Database Update Error: {dbEx.ToString()}");
      if (dbEx.InnerException != null) { Console.WriteLine($"Inner Exception: {dbEx.InnerException.ToString()}"); }
      res.Msg = $"An error occurred while saving to the database. {dbEx.InnerException?.Message ?? dbEx.Message}";
      res.ResponseCode = "500";
      res.Data = false;
      return await Task.FromResult(res); // Added await Task.FromResult()
    }
    catch (Exception ex) // Catch any other unexpected exceptions
    {
      // Log the general exception details (ex.ToString())
      Console.WriteLine($"General Error: {ex.ToString()}");
      res.Msg = $"An unexpected error occurred: {ex.Message}";
      res.ResponseCode = "500";
      res.Data = false;
      return await Task.FromResult(res); // Added await Task.FromResult()
    }
  }



  #endregion





  //public async Task<IApiResponse> SaveFillCoScholasticMarks(List<CoScholasticObtainedModel> rowData)
  //{
  //  IApiResponse res = new ApiResponse();

  //  try
  //  {
  //    //var result = (dynamic)null;
  //    await Task.Run(() =>
  //    {
  //      IQueryable<Student> query = _lumen.Students.Where(x => !x.IsApplyforTc);
  //      var getBoardID = _lumen.TblCreateSchools.Select(x => x.BoardId).FirstOrDefault();
  //      TblCoScholasticObtainedGrade _obj = new TblCoScholasticObtainedGrade();
  //      foreach (var item in rowData)
  //      {
  //        var existingRecord = _lumen.TblCoScholasticResults.FirstOrDefault(tr => tr.TermId == item.TermID && tr.StudentId == item.StudentID && tr.BoardId == getBoardID && tr.ClassId == item.ClassID);
  //        if (existingRecord != null)
  //        {
  //          var existingData1 = _lumen.TblCoScholasticObtainedGrades.Select(s => new TblCoScholasticObtainedGrade
  //          {
  //            batchId = s.batchId,
  //            CoscholasticId = s.CoscholasticId,
  //            Id = s.Id,
  //            ObtainedGrade = s.ObtainedGrade,
  //            ObtainedCoScholasticId = s.ObtainedCoScholasticId
  //          }).Where(data => data.ObtainedCoScholasticId == existingRecord.Id).ToList();
  //          for (int i = 0; i < item.CoscholasticData!.Count; i++)
  //          {
  //            var Dt = item.CoscholasticData[i];

  //            if (existingData1.Count > 0)
  //            {
  //              // Update existing data
  //              existingData1[i].CoscholasticId = Dt.CoscholasticID;
  //              existingData1[i].ObtainedGrade = Dt.ObtainedGrade;
  //              _obj.ObtainedCoScholasticId = (long)existingData1[i].ObtainedCoScholasticId!;
  //              _obj.CoscholasticId = (long)existingData1[i].CoscholasticId!;
  //              _obj.ObtainedGrade = existingData1[i].ObtainedGrade;
  //              _obj.batchId = existingData1[i].batchId;
  //              _lumen.Entry(existingData1[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
  //              _lumen.TblCoScholasticObtainedGrades.Add(_obj);
  //              _lumen.SaveChanges();
  //            }
  //            else
  //            {
  //              // Add new CoScholasticData for the existing record
  //              Dt.ObtainedCoScholasticID = existingRecord.Id;
  //              _obj.ObtainedCoScholasticId = (long)existingData1[i].ObtainedCoScholasticId!;
  //              _obj.CoscholasticId = (long)existingData1[i].CoscholasticId!;
  //              _obj.ObtainedGrade = existingData1[i].ObtainedGrade;
  //              _obj.batchId = existingData1[i].batchId;
  //              _lumen.TblCoScholasticObtainedGrades.Add(_obj);
  //              _lumen.SaveChanges();
  //            }

  //          }


  //          _lumen.SaveChanges();
  //          //return Json(new { success = true, errormsg="Data Updated .." });
  //          res.Data = true;
  //          res.Msg = "data save successfully";
  //          res.ResponseCode = "200";
  //        }
  //        else
  //        {
  //          item.BoardID = (long)getBoardID!;

  //          // This item doesn't have a corresponding record in Tbl_TestRecord, so add it as a new record
  //          TblCoScholasticResult tbl_CoScholastic_Result = new TblCoScholasticResult()
  //          {
  //            BoardId = getBoardID,
  //            TermId = item.TermID,
  //            StudentId = item.StudentID,
  //            ClassId = item.ClassID,
  //            SectionId = item.SectionId
  //          };
  //          _lumen.TblCoScholasticResults.Add(tbl_CoScholastic_Result);
  //          _lumen.SaveChanges();
  //          long latestId = tbl_CoScholastic_Result.Id;
  //          foreach (var Dt in item.CoscholasticData!)
  //          {
  //            Dt.ObtainedCoScholasticID = latestId;
  //            _obj.ObtainedCoScholasticId = (long)Dt.ObtainedCoScholasticID!;
  //            _obj.CoscholasticId = (long)Dt.CoscholasticID!;
  //            _obj.ObtainedGrade = Dt.ObtainedGrade;
  //            _obj.batchId = Dt.batchId;
  //            _lumen.TblCoScholasticObtainedGrades.Add(_obj);
  //          }

  //          res.Data = true;
  //          res.Msg = "data save successfully";
  //          res.ResponseCode = "200";
  //        }
  //      }
  //      _lumen.SaveChanges();

  //      //if (listStudents.Count > 0)
  //      //{
  //      res.Data = true;
  //      res.Msg = "data save successfully";
  //      res.ResponseCode = "200";
  //      //      }
  //      //else
  //      //{
  //      //res.Data = new StafsDetail();
  //      //res.Msg = "data Found";
  //      //res.ResponseCode = "400";
  //      //}
  //    });
  //    //   return res;
  //    //return Json(listStudents.OrderBy(x => x.StudentName), JsonRequestBehavior.AllowGet);
  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  #region CoScola
  public async Task<IApiResponse> SaveFillCoScholasticMarks(List<CoScholasticObtainedModel> rowData)
  {
    IApiResponse res = new ApiResponse();

    try
    {
      await Task.Run(() =>
      {
        var getBoardID = _lumen.TblCreateSchools.Select(x => x.BoardId).FirstOrDefault();

        foreach (var studentData in rowData)
        {
          // Find or create the main CoScholastic result record for the student
          var existingRecord = _lumen.TblCoScholasticResults.FirstOrDefault(tr => tr.TermId == studentData.TermID &&
                                                                                   tr.StudentId == studentData.StudentID &&
                                                                                   tr.BoardId == getBoardID &&
                                                                                   tr.ClassId == studentData.ClassID);

          if (existingRecord == null)
          {
            // Create new CoScholastic result record if not found
            existingRecord = new TblCoScholasticResult
            {
              BoardId = getBoardID,
              TermId = studentData.TermID,
              StudentId = studentData.StudentID,
              ClassId = studentData.ClassID,
              SectionId = studentData.SectionId
            };
            _lumen.TblCoScholasticResults.Add(existingRecord);
            _lumen.SaveChanges(); // Save to get the new ID
          }

          long resultId = existingRecord.Id;

          if (studentData.CoscholasticData != null && studentData.CoscholasticData.Any())
          {
            foreach (var subjectData in studentData.CoscholasticData)
            {
              // Check if the subject entry already exists for the student
              var existingGrade = _lumen.TblCoScholasticObtainedGrades.FirstOrDefault(grade =>
                  grade.ObtainedCoScholasticId == resultId &&
                  grade.CoscholasticId == subjectData.CoscholasticID &&
                  grade.batchId == subjectData.batchId);

              if (existingGrade != null)
              {
                // Update existing grade
                existingGrade.ObtainedGrade = subjectData.ObtainedGrade;
                _lumen.Entry(existingGrade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
              }
              else
              {
                // Insert new grade entry
                var newGrade = new TblCoScholasticObtainedGrade
                {
                  ObtainedCoScholasticId = resultId,
                  CoscholasticId = (long)subjectData.CoscholasticID!,
                  ObtainedGrade = subjectData.ObtainedGrade,
                  batchId = subjectData.batchId
                };
                _lumen.TblCoScholasticObtainedGrades.Add(newGrade);
              }
            }
          }
        }

        // Save all changes at once to improve performance
        _lumen.SaveChanges();

        res.Data = true;
        res.Msg = "Data saved successfully";
        res.ResponseCode = "200";
      });
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }

    return res;
  }
  //public async Task<IApiResponse> SaveFillCoScholasticMarks(List<CoScholasticObtainedModel> rowData)
  //{
  //  IApiResponse res = new ApiResponse();
  //  try
  //  {
  //    long boardIdValue = 0;
  //    var school = await _lumen.TblCreateSchools.FirstOrDefaultAsync();
  //    var fetchedBoardID = school?.BoardId;
  //    if (fetchedBoardID.HasValue)
  //    {
  //      boardIdValue = fetchedBoardID.Value;
  //    }

  //    foreach (var item in rowData)
  //    {
  //      var existingRecord = await _lumen.TblCoScholasticResults.FirstOrDefaultAsync(tr =>
  //          tr.TermId == item.TermID &&
  //          tr.StudentId == item.StudentID &&
  //          tr.BoardId == boardIdValue &&
  //          tr.ClassId == item.ClassID);

  //      if (existingRecord != null)
  //      {
  //        var existingData1 = await _lumen.TblCoScholasticObtainedGrades
  //  .Where(data => data.ObtainedCoScholasticId == existingRecord.Id)
  //  .ToListAsync();

  //        if (item.CoscholasticData != null)
  //        {
  //          for (int i = 0; i < item.CoscholasticData.Count; i++)
  //          {
  //            var Dt = item.CoscholasticData[i];

  //            if (existingData1.Count > i && existingData1[i] != null)
  //            {
  //              existingData1[i].CoscholasticId = Dt.CoscholasticID;
  //              existingData1[i].ObtainedGrade = Dt.ObtainedGrade;
  //              existingData1[i].batchId = Dt.batchId;
  //              _lumen.Entry(existingData1[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
  //            }
  //            else if (existingRecord != null)
  //            {
  //              var newObtainedGrade = new TblCoScholasticObtainedGrade
  //              {
  //                ObtainedCoScholasticId = existingRecord.Id,
  //                CoscholasticId = Dt.CoscholasticID,
  //                ObtainedGrade = Dt.ObtainedGrade,
  //                batchId = Dt.batchId
  //              };
  //              _lumen.TblCoScholasticObtainedGrades.Add(newObtainedGrade);
  //            }
  //          }
  //        }
  //      }
  //      else
  //      {
  //        item.BoardID = boardIdValue;

  //        TblCoScholasticResult tbl_CoScholastic_Result = new TblCoScholasticResult()
  //        {
  //          BoardId = boardIdValue,
  //          TermId = item.TermID,
  //          StudentId = item.StudentID,
  //          ClassId = item.ClassID,
  //          SectionId = item.SectionId
  //        };
  //        _lumen.TblCoScholasticResults.Add(tbl_CoScholastic_Result);
  //        await _lumen.SaveChangesAsync();

  //        long latestId = tbl_CoScholastic_Result.Id;
  //        if (item.CoscholasticData != null)
  //        {
  //          foreach (var Dt in item.CoscholasticData)
  //          {
  //            var newObtainedGrade = new TblCoScholasticObtainedGrade
  //            {
  //              ObtainedCoScholasticId = latestId,
  //              CoscholasticId = Dt.CoscholasticID,
  //              ObtainedGrade = Dt.ObtainedGrade,
  //              batchId = Dt.batchId
  //            };
  //            _lumen.TblCoScholasticObtainedGrades.Add(newObtainedGrade);
  //          }
  //        }
  //      }
  //    }

  //    await _lumen.SaveChangesAsync();
  //    res.Data = true;
  //    res.Msg = "save  data successfully";
  //    res.ResponseCode = "200";
  //  }
  //  catch (Exception ex)
  //  {
  //    res.Msg = ex.Message;
  //    res.ResponseCode = "500";
  //  }
  //  return res;
  //}
  #endregion

  //DownloadPrintReportCardData
  public async Task<IApiResponse> DownloadPrintReportCardData(int studentId, int termId, int BatchId)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      //await Task.Run(() =>
      //{

      var commandText = "EXEC Sp_GetStudentReportCardData @Batch_Id, @StudentId, @TermId";

      var parameters = new[]
      {
            new SqlParameter("@Batch_Id", BatchId),
            new SqlParameter("@StudentId", studentId),
            new SqlParameter("@TermId", termId)
          };
      var printReportCard = new PrintReportCardData();
      var records = new List<TermSubjectMarks>();
      var connectionString = _lumen.Database.GetConnectionString();
      using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
      {
        var studentData = new StudentData();

        await connection.OpenAsync();
        using (var command = connection.CreateCommand())
        {
          command.CommandText = commandText;
          command.CommandType = CommandType.Text;
          command.Parameters.AddRange(parameters);

          commandText.Replace("@Batch_Id", BatchId.ToString());
          commandText.Replace("@StudentId", studentId.ToString());
          commandText.Replace("@TermId", termId.ToString());

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
                studentData.AcademicYear = reader["AcademicYear"].ToString() ?? "";
                studentData.ClassName = reader["ClassName"].ToString() ?? "";
                studentData.SectionName = reader["SectionName"].ToString() ?? "";
               // studentData.Attendance = float.Parse(reader["Attendance"].ToString() ?? "");
                studentData.PromotedClass = reader["PromotedClass"].ToString() ?? "";
                studentData.StaffSignatureLink = reader["StaffSignatureLink"].ToString() ?? "";
                studentData.Remark = reader["Remark"].ToString() ?? "";
                studentData.ClassID = int.Parse(reader["ClassID"].ToString() ?? "");
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
            Terms = new List<SubjectTermRecord>()
          };
          foreach (var t in g.Where(x => x.TestType.ToUpper() != "PRACTICAL").ToList())
          {
            var PracticalMark = g.Where(x => x.SubjectName == g.Key
            && x.TermName == t.TermName
            && x.TestType.ToUpper() == "PRACTICAL")
                .Select(x => x.Mark).FirstOrDefault();
            subject.Terms.Add(new SubjectTermRecord
            {
              Name = t.TermName,
              TheoryMark = PracticalMark,
              PracticalMark = t.Mark,
              MaximumMarks = t.MaximumMarks,
              TotallMark = t.Mark + PracticalMark,
              Grade = t.IsOptional ? t.Grade : "",
              IsOptional = t.IsOptional,
            });
          }
          GroupedSubjects.Add(subject);
        }

        printReportCard.GroupedSubjects = GroupedSubjects;
        List<GroupedTerms> grouped_terms_records = new List<GroupedTerms>();
        var grouped_by_term_records = (from r in records
                                       where r.IsOptional != true
                                       group r by new { r.TermName, r.TestType } into g
                                       select g).ToList();

        foreach (var tr in grouped_by_term_records)
        {
          var totalMarks = tr.Sum(x => x.Mark);
          var maxMarks = tr.FirstOrDefault()?.MaximumMarks;
          grouped_terms_records.Add(new GroupedTerms
          {
            Term = tr.Key.TermName,
            TestType = tr.Key.TestType,
            Total = totalMarks,
            MaximumMarks = maxMarks ?? 0,
            Percentage = (totalMarks / (maxMarks ?? 0 * tr.ToList().Count())) * 100,
            Grade = ""
          });
        }

        printReportCard.GroupedTerms = grouped_terms_records;

        //var classCoscholastic = _lumen.TblCoScholasticClasses
        //                                .Where(x => x.ClassId == printReportCard.StudentData.ClassID)
        //                                .Select(x => x.CoscholasticId)
        //                                .ToList();

        //var coscholasticRecords = _lumen.TblCoScholastics
        //                                .Where(record => classCoscholastic.Contains(record.Id))
        //                                .ToList();

        //var coscholasticResult = (from c in coscholasticRecords
        //                          join cr in _lumen.TblCoScholasticResults
        //                          on c.Id equals cr.CoScholasticId into crGroup
        //                          from cr in crGroup.DefaultIfEmpty()
        //                          join cog in _lumen.TblCoScholasticObtainedGrades
        //                          on cr?.Id equals cog.ObtainedCoScholasticId into cogGroup
        //                          from cog in cogGroup.DefaultIfEmpty()
        //                          where cr == null || cog == null ||
        //                              (cr.StudentId == studentId
        //                              && (cr.TermId == 10 || cr.TermId == termId)
        //                              && cog.batchId == BatchId)
        //                          select new
        //                          {
        //                            CoscholasticID = c.Id,
        //                            Title = c.Title,
        //                            ObtainedGrade = cog?.ObtainedGrade,
        //                            Term = _lumen.TblTerms.Where(x => x.TermId == termId).Select(x => x.TermName).FirstOrDefault()
        //                          }).ToList();

        //printReportCard.CoscholasticAreaData = coscholasticResult.Select(item => new CoscholasticAreaDatas
        //{
        //  Name = item.Title,
        //  ObtainedGrade = item.ObtainedGrade,
        //  Term = item.Term
        //}).ToList();


        var gradinglist = _lumen.GradingCriterias
            .Where(x => x.TermID == termId && x.BatchID == BatchId && x.ClassID == printReportCard.StudentData.ClassID)
            .ToList();
        //printReportCard.GradingCriteria = gradinglist;
        string termName = _lumen.TblTerms.Where(x => Convert.ToInt32(x.TermId) == termId).Select(t => t.TermName).FirstOrDefault() ?? "";

        //var gradeCounts = new Dictionary<int, int>
        //{
        //    { 1, subjectDatas.Count(x => x.MarksUT1Grade == "D") },
        //    { 2, subjectDatas.Count(x => x.MarksUT2Grade == "D") },
        //    { 3, subjectDatas.Count(x => x.GradeUT1 == "D") },
        //    { 4, subjectDatas.Count(x => x.GradeUT2 == "D") },
        //    { 7, subjectDatas.Count(x => x.GradePre1 == "D") },
        //    { 8, subjectDatas.Count(x => x.GradePre2 == "D") },
        //    { 10, subjectDatas.Count(x => x.FinalGrade == "D") }
        //};

        //string result = gradeCounts.TryGetValue(termId, out int count) ? (count > 0 ? "" : "Pass") : "Invalid grade";

        printReportCard.Result = "Pass";

        if (printReportCard != null)
        {
          res.Data = printReportCard;
          res.Msg = "Get  data successfully";
          res.ResponseCode = "200";
        }
        else
        {
          res.Data = new StafsDetail();
          res.Msg = "data Found";
          res.ResponseCode = "200";

        }

        //});

      }
    }
    catch (Exception ex)
    {
      res.Msg = ex.Message;
      res.ResponseCode = "500";
    }
    return res;
  }
  public async Task<IApiResponse> GetExamTimeTable(int? BatchId = null, int? TermId = null)
  {
    IApiResponse res = new ApiResponse();
    try
    {
      var conn = _lumen.Database.GetDbConnection();
      await conn.OpenAsync();

      string sql = @"Exec GetExamTImeTable " + TermId + ","+ BatchId + "";

      using (var command = conn.CreateCommand())
      {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        var reader = await command.ExecuteReaderAsync();

        var result = new List<ExamTimeTableView>();
        while (await reader.ReadAsync())
        {
          var staff = new ExamTimeTableView
          {
            ClassId = reader.IsDBNull(reader.GetOrdinal("ClassID")) ? 0 : Convert.ToInt64(reader["ClassID"]),
            ClassName = reader["ClassName"]?.ToString() ?? string.Empty,
            Subject_Id = reader.IsDBNull(reader.GetOrdinal("Subject_ID")) ? 0 : Convert.ToInt32(reader["Subject_ID"]),
            SubjectName = reader["Subject_Name"]?.ToString() ?? string.Empty,
            TestName = reader["TestName"]?.ToString() ?? string.Empty,
            TestType = reader["TestType"]?.ToString() ?? string.Empty,
            date = reader["ExamDate"]?.ToString() ?? string.Empty,
            time = reader["ExamTime"]?.ToString() ?? string.Empty,

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

  #region Model  

  public class ListStudent
  {
    public long StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? ClassName { get; set; }
    public string? SectionName { get; set; }
    public string? TestName { get; set; }
    public string? TestType { get; set; }
    public string? Subject { get; set; }
    public string? Term { get; set; }
    public decimal ObtainedMarks { get; set; }
    public decimal MaximumMarks { get; set; }
    public int BatchId { get; set; }
    public string? BatchName { get; set; }
    public List<StudentTestObtMarks>? studentTestObtMarks { get; set; }
    public bool IsFreeze { get; set; }
  }

  public class CoScholasticListStudent
  {
    public long? StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Term { get; set; } = string.Empty;
    public decimal ObtainedMarks { get; set; }
    public List<CoscholastiStuentObtData>? coscholastiStuentObtDatas { get; set; }
  }

  public class CoscholastiStuentObtData
  {
    public long CoscholasticID { get; set; }
    public string ObtainedGrade { get; set; } = string.Empty;
    public string CoscholasticName { get; set; } = string.Empty;
  }

  public class CoscholasticAreaData
  {
    public string Name { get; set; } = string.Empty;
    public string GradeTerm1 { get; set; } = string.Empty;
    public string GradeTerm2 { get; set; } = string.Empty;
    public string GradePre1 { get; internal set; } = string.Empty;
    public string GradePre2 { get; internal set; } = string.Empty;
  }
  public class StudentTestObtMarks
  {
    public long TestID { get; set; }
    public decimal ObtainedMarks { get; set; }
    public decimal MaximumdMarks { get; set; }
    public string? TestName { get; set; }
    public string? Remark { get; set; }
    public bool IsElective { get; set; }
    public bool IsOptional { get; set; }
  }
  public class StaffclassectionSubject
  {
    public int StaffId { get; set; }
    public int ClassId { get; set; }
    public int SectionId { get; set; }
    public int BatchId { get; set; }
    public int SubjectID { get; set; }
    public string SubjectName { get; set; } = string.Empty;
  }
  
  #endregion
}

