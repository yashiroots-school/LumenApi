using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public  class TblTestObtainedMark
{
    public long Id { get; set; }

    public long? RecordIdfk { get; set; }

    public decimal? ObtainedMarks { get; set; }

    public long TestId { get; set; }
}
public class StudentObtainedMarkModel
{
  public long StudentID { get; set; }
  public long ClassID { get; set; }
  public long SectionId { get; set; }
  public long TermID { get; set; }
  public long BoardID { get; set; }
  public string? Remark { get; set; }
  public int BatchId { get; set; }
  public int staffId { get; set; }
  public List<TblTestObtainedMark>? ObtainedMarkData { get; set; }
}

public class tbl_CoScholasticObtainedGrade
{
  public long Id { get; set; }
  public long ObtainedCoScholasticID { get; set; }
  public long CoscholasticID { get; set; }
  public string? ObtainedGrade { get; set; }
  public int BatchId { get; set; }
}
