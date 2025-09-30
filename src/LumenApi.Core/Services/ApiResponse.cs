using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenApi.Core.Interfaces;

namespace LumenApi.Core.Services;

  public class ApiResponse : IApiResponse
  {
    public object Data { get; set; } = new object();
    public string Msg { get; set; } = string.Empty;
    public string ResponseCode { get; set; } = string.Empty;
    public object AdditionalData { get; set; } = string.Empty;
  }
//Nitish change

