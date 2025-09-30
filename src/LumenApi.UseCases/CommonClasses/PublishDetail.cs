using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Web.Models.Params;
public class PublishDetail
{
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public int TermId { get; set; }
  public int BatchID { get; set; }
  public bool IsPublish { get; set; }=false;
  public string? PublishBy { get; set; }
}
public class BatchListModel
{

  public int BatchId { get; set; }

  public string? BatchName { get; set; }


}
public class ReportFillmarks
{
  public int classId { get; set; }
  public int sectionId { get; set; }
  public int testId { get; set; }
  public int termId { get; set; }
  public int staffId { get; set; }
  public int batchId { get; set; }
}


public class ReportCard
{
  public int classId { get; set; }
  public int sectionId { get; set; }  
  public int termId { get; set; }  
  public int batchId { get; set; }
}
public class FillCoScholasticAreasReporModel
{
  public int classId { get; set; }
  public int sectionId { get; set; }
  public int termId { get; set; }
  public int batchId { get; set; }




}
public partial class TblAssignment
{
  public int AssignmentId { get; set; }

  public string? ClassName { get; set; }

  public int ClassId { get; set; }

  public string? SectionName { get; set; }

  public int SectionId { get; set; }

  public string? SubjectName { get; set; }

  public int SubjectId { get; set; }

  public string? NewAssignment { get; set; }

  public string? AssignmentDate { get; set; }

  public string? SubmittedDate { get; set; }

  public string? CreatedDate { get; set; }
  public int BatchId { get;set; }
  public int Staff_Id { get; set; }
}
//public class StudentObtainedMarkModel
//{
//  public long StudentID { get; set; }
//  public long ClassID { get; set; }
//  public long SectionId { get; set; }
//  public long TermID { get; set; }
//  public long BoardID { get; set; }
//  public string? Remark { get; set; }
//  public int BatchId { get; set; }
//  public List<TblTestObtainedMarks>? ObtainedMarkData { get; set; }
//}
//public class tbl_CoScholasticObtainedGrade
//{
//  public long Id { get; set; }
//  public long ObtainedCoScholasticID { get; set; }
//  public long CoscholasticID { get; set; }
//  public string? ObtainedGrade { get; set; }
//}

