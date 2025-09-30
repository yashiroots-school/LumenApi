using System.ComponentModel.DataAnnotations;

namespace LumenApi.Web.Models.StudentView;

public partial class ExamTimeTableView
{
  

  public long ClassId { get; set; }

  public string? ClassName { get; set; }

  public int ? Subject_Id { get; set; }

  public string? TestName { get; set; }

  public string? SubjectName { get; set; }
  public string? TestType { get; set; }
  public string? date { get; set; }
  public string? time { get; set; }

}
