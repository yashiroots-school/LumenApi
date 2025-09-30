//namespace LumenApi.Web.Models.Params;
namespace LumenApi.UseCases.CommonClasses;
public class TblTestObtainedMarks
{
  public long Id { get; set; }

  public long? RecordIdfk { get; set; }

  public decimal? ObtainedMarks { get; set; }

  public long TestId { get; set; }
}

public class StudentObtainedMark
{
  public long StudentID { get; set; }
  public long ClassID { get; set; }
  public long SectionId { get; set; }
  public long TermID { get; set; }
  public long BoardID { get; set; }
  public string? Remark { get; set; }
  public int BatchId { get; set; }
  public int staffId { get; set; }
  public List<TblTestObtainedMarks>? ObtainedMarkData { get; set; }
}
public class tbl_CoScholasticObtainedGrade
{
  public long Id { get; set; }
  public long ObtainedCoScholasticID { get; set; }
  public long CoscholasticID { get; set; }
  public string? ObtainedGrade { get; set; }
  public int batchId { get; set; }
}

public partial class TblCoScholasticObtainedGrade
{
  public long Id { get; set; }

  public long? ObtainedCoScholasticId { get; set; }

  public string? ObtainedGrade { get; set; }

  public long? CoscholasticId { get; set; }
}
public class CoScholasticObtainedModel
{
  public long StudentID { get; set; }
  public long ClassID { get; set; }
  public long SectionId { get; set; }
  public long TermID { get; set; }
  public long BoardID { get; set; }

  public List<tbl_CoScholasticObtainedGrade>? CoscholasticData { get; set; }
}
