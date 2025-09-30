using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblCoScholasticObtainedGrade
{
  public long Id { get; set; }

  public long? ObtainedCoScholasticId { get; set; }

  public string? ObtainedGrade { get; set; }

  public long? CoscholasticId { get; set; }
  public int? batchId { get; set; }
}
public class CoScholasticObtained
{
  public long StudentID { get; set; }
  public long ClassID { get; set; }
  public long SectionId { get; set; }
  public long TermID { get; set; }
  public long BoardID { get; set; }

  public List<TblCoScholasticObtainedGrade>? CoscholasticData { get; set; }
}
