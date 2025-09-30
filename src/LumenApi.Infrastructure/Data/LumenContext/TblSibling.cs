using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblSibling
{
    public int SiblingsId { get; set; }

    public int StudentId { get; set; }

    public string? Studentname { get; set; }

    public int ClassId { get; set; }

    public string? Confirmation { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public int FamilyDetailsId { get; set; }
}
