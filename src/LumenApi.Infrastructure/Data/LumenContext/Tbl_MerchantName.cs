using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Infrastructure.Data.LumenContext;
public class Tbl_MerchantName:BaseEntity
{
  [Key]
  public int MerchantName_Id { get; set; }

  public string MerchantName { get; set; } = string.Empty;  

  public int School_Id { get; set; }

  public int Bank_Id { get; set; }

  public int Branch_Id { get; set; }
}
