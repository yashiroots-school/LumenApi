using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblDeclaration
{
    public long DeclarationId { get; set; }

    public string ScholarNumber { get; set; } = null!;

    public string? Interesterd { get; set; }

    public string? NotInterested { get; set; }

    public string? Relocate { get; set; }

    public string? StudentName { get; set; }

    public string? Agree { get; set; }

    public string? Addedon { get; set; }

    public string? Addeby { get; set; }

    public string? Updatedon { get; set; }

    public string? Updatedby { get; set; }

    public string? Spare1 { get; set; }

    public string? Spare2 { get; set; }

    public string? Spare3 { get; set; }

    public virtual TblStudentDetail ScholarNumberNavigation { get; set; } = null!;
}
