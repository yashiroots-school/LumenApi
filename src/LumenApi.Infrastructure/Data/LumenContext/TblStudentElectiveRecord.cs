using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblStudentElectiveRecord
{
    public long Id { get; set; }

    public long? StudentId { get; set; }

    public long? ElectiveSubjectId { get; set; }
}
