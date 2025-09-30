using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblEmailArchiefe
{
    public int EmailId { get; set; }

    public int StudentId { get; set; }

    public string? ApplicationNumber { get; set; }

    public string? Name { get; set; }

    public string? ParentEmail { get; set; }

    public string? EmailDate { get; set; }

    public string? EmailContent { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }
}
