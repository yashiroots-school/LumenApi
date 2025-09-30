
using LumenApi.Core.Interfaces;
using LumenApi.Web.Models.Params;
using LumenApi.Web;
using LumenApi.UseCases.CommonClasses;

public interface IExamInterface
{
  Task<IApiResponse> GetPublishData(PublishDetail objPublishDetail);
  Task<IApiResponse> PublishUnpublish(PublishDetail objPublishDetail);

  Task<IApiResponse> GetFreezingData(FreezeUnfreezeDTO objFreeze);
  Task<IApiResponse> FreezeUnfreezeData(FreezeUnfreezeDTO objFreeze);
  Task<IApiResponse> getStaffList();
  Task<IApiResponse> GetClassList(int staff_Id);
  Task<IApiResponse> GetSectionList(int staffId, int classId);
  Task<IApiResponse> GetStaffClassList(int staff_Id);
  Task<IApiResponse> GetStaffSectionList(int staffId, int classId);
  Task<IApiResponse> GetTermDropList();
  Task<IApiResponse> GetBatchDropList();
  Task<IApiResponse> StudentByClassSection(int classId, int sectionId, int testId, int termId, int staffId, int batchId);
  Task<IApiResponse> ReportCard(int classId, int sectionId, int termId, int batchId);
  Task<IApiResponse> GradAll(int rolenumbe, int term);
  Task<IApiResponse> SaveFillmarkReport(List<StudentObtainedMark> rowData);
  Task<IApiResponse> SaveFillCoScholasticMarks(List<CoScholasticObtainedModel> rowData);
  Task<IApiResponse> FillCoScholasticAreasRepor(FillCoScholasticAreasReporModel _objmodel);
  Task<IApiResponse> DownloadPrintReportCardData(int studentId, int termId, int BatchId);
  Task<IApiResponse> HoldUnHoldStudentReportCard(int StudentId, int term, int Batch, int classid, string Remark, bool isHold);
  Task<IApiResponse> GetHomeWorkList(int ClassId, int SectionId, int SubjectId = 0);
  Task<IApiResponse> AddHomeWork(TblAssignment tblAssignment);
  Task<IApiResponse> UpdateAssignment(TblAssignment tblAssignment);
  Task<IApiResponse> GetAssignmentById(int id);
  Task<IApiResponse> DeleteAssignment(int id);
  Task<IApiResponse> GetStaffSubjects(int staffId, int classId, int sectionId);
}
