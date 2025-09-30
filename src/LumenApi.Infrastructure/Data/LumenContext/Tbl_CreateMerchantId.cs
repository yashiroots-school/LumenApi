using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Infrastructure.Data.LumenContext;
public class Tbl_CreateMerchantId: BaseEntity
{
  [Key]
  public int Merchant_Id { get; set; }

  public int School_Id { get; set; }

  public int Bank_Id { get; set; }

  public int Branch_Id { get; set; }

  public int MerchantName_Id { get; set; }

  public string MerchantMID { get; set; } = string.Empty;

  public string MerchantKey { get; set; } = string.Empty;
  public string? Password { get; set;} = string.Empty;

}
