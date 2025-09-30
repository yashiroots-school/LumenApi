using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblMenuName
{
    public int MenuId { get; set; }

    public string? MenuName { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CurrentYear { get; set; }

    public string? Ip { get; set; }

    public string? UserId { get; set; }

    public bool IsDeleted { get; set; }

    public int CreateBy { get; set; }

    public string? InsertBy { get; set; }

    public string? BatchName { get; set; }

    public string? UploadImage { get; set; }
}
