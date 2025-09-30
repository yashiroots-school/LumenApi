using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Web;
public partial class Tbl_PublishDetail
{
  public int PublishId { get; set; }
  public int TermId { get; set; }
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public bool IsPublish { get; set; }
  public int BatchId { get; set; }
  public string? PublishBy { get; set; } 
  public DateTime PublishDate { get; set; }
  public DateTime? ModifiedDate { get; set; }
}
public partial class Tbl_Assignment
{
  public int Assignment_Id { get; set; }
  public string Class_Name { get; set; } = string.Empty;
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public string Section_Name { get; set; } = string.Empty;
  public int Subject_ID { get; set; }
  public string? Subject_Name { get; set; }
  public string? New_Assignment { get; set; }
  public string? Assignment_Date { get; set; }
  public string? PublishDate { get; set; }
  public string? CreatedDate { get; set; }
}
public class Tbl_HoldDetail
{
  [Key]
  public int HoldId { get; set; }
  public int TermId { get; set; }
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public bool IsHold { get; set; }
  public int BatchId { get; set; }
  public int StudentId { get; set; }
  public string? Remark { get; set; }
  public DateTime HoldDate { get; set; }
  public DateTime? ModifiedDate { get; set; }
}

