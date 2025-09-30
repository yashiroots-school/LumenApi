using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Infrastructure.Data.LumenContext;
public class Tbl_CreateBranch
{
  [Key]
  public int Branch_ID { get; set; }

  public int Bank_Id { get; set; } 

  public string Bank_Name { get; set; } = string.Empty;

  public string Branch_Name { get; set; } = string.Empty;

  public string? Contact_No { get; set; } = string.Empty;

  public string? Contact_Name { get; set; } = string.Empty;

  public string? Landline_No { get; set; } = string.Empty;
}
