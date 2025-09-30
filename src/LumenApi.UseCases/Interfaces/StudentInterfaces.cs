using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.Core.Interfaces;
using LumenApi.UseCases.CommonClasses;

namespace LumenApi.UseCases.Interfaces;
internal interface StudentInterfaces
{

}

public interface IStudentInterfaces
{
  Task<IApiResponse> GetStudentDetials(string? UserId);
  Task<IApiResponse> GetStudentDetialsbyClassSection(int classId, int Section);
  Task<IApiResponse> GetStudentResultsByClassSection(int StudentId, long BatchId);
  Task<IApiResponse> GetStudentDetail(string ApplicationNo);
  Task<IApiResponse> GetStusdnetDetails(long BatchId, int classId, int SectionId, long StudentId = 0);
  Task<IApiResponse> GetStudentProfileSummeryBatchWise(long studentId, int batchId);
 Task<IApiResponse> GetStaffDetails(long StaffId = 0);
  Task<IApiResponse> GetStudentProfile(string applicationNo);
}
