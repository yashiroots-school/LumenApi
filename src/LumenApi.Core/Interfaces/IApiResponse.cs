using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.Core.Interfaces;
public interface IApiResponse
{
  public object Data {  get; set; } 
  public string Msg { get; set; }
  public string ResponseCode { get; set; }
  public object AdditionalData { get; set; }
}
