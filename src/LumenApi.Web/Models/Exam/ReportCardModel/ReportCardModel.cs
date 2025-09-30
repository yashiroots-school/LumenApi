namespace LumenApi.Web.Models.Exam.ReportCardModel;

public class ReportCardModel
{
  public int StudentId { get; set; } 
  public string? Name { get; set; } 
  public int Class_Id { get; set; } 
  public string? ClassName { get; set; } 
  public int Section_id { get; set; } 
  public string? SectionName { get; set; } 
  public int Batch_id { get; set; } 
  public string? BatchName { get; set; } 
  public int TermID { get; set; } 
  public bool? IsHold { get; set; }
}
