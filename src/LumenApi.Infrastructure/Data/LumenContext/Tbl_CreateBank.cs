using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Infrastructure.Data.LumenContext;
public  class Tbl_CreateBank
{
  [Key]
  public int Bank_Id { get; set; }

  public string Bank_Name { get; set; } = string.Empty;

  public string? Bank_Code { get; set; } = string.Empty;

  public string? Contact_No { get; set; } = string.Empty;

  public string? LandlineNo { get; set; } = string.Empty;

  public string? Contactperson_Name { get; set; } = string.Empty;
}
