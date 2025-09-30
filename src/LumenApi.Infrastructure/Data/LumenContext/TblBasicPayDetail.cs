using System;
using System.Collections.Generic;

namespace LumenApi.Web;

public partial class TblBasicPayDetail
{
    public int BasicAmountId { get; set; }

    public int SchoolCategoryId { get; set; }

    public int BasicPayId { get; set; }

    public string? CategoryName { get; set; }

    public string? BasicpayName { get; set; }

    public float BasicAmount { get; set; }

    public string? CreatedDate { get; set; }
}
