using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class GradingCriteria
{
  public long CriteriaId { get; set; }

  public decimal? MinimumPercentage { get; set; }

  public decimal? MaximumPercentage { get; set; }

  public string? Grade { get; set; }

  public string? GradeDescription { get; set; }

  public long? BoardId { get; set; }
  public int BatchID{get; set;}
  public int TermID{get; set;}
  public int ClassID{get; set;}


  }
