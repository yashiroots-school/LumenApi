using System;
using System.Collections.Generic;
using LumenApi.UseCases.CommonClasses;
using LumenApi.Web.Models.Params;

namespace LumenApi.Web;

public partial class TblCoScholasticClass
{
    public long Id { get; set; }

    public long? BoardId { get; set; }

    public long? ClassId { get; set; }

    public long? CoscholasticId { get; set; }

    public string? ClassName { get; set; }
}


