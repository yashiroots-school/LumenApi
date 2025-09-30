using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Infrastructure.Data.LumenContext;
public  class Tbl_DataList
{
  [Key]
  public int DataListId { get; set; }
  public string DataListName { get; set; } = string.Empty;
}
