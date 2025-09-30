using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Web.Models.Params;
public class FreezeUnfreezeDTO
{
  public int? FreezeId { get; set; }
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public int TermId { get; set; }
  public bool IsFreeze { get; set; }
  public int BatchId { get; set; }
}
