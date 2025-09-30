using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumenApi.UseCases.CommonClasses;
public class FeeDetails
{
  public List<FeeDetailsDto>? FeeDetailsDto { get; set; }
  public float TotalAmount { get; set; }
}
public class FeeDetailsDto
{
  public int FeeId { get; set; }
  public string? FeeName { get; set; }
  public float FeeValue { get; set; }
  public float PaidAmount { get; set; }
  public float Balance { get; set; }
}
