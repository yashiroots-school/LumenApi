using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Web.Models.Params;
public class PublishDetail
{
  public int ClassId { get; set; }
  public int SectionId { get; set; }
  public int TermId { get; set; }
  public int BatchID { get; set; }
  public bool IsPublish { get; set; }=false;
  public string? PublishBy { get; set; }
}
