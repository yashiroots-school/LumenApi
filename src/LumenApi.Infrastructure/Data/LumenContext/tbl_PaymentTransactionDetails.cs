using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LumenApi.Infrastructure.Data.LumenContext;

public partial class tbl_PaymentTransactionDetails
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public long PaymentTransactionId { get; set; }
  [StringLength(1000)]
  public string? TransactionStatus { get; set; } = string.Empty;
  [StringLength(1000)]
  public string TransactionError { get; set; } = string.Empty;
  [StringLength(30)]
  public string TxnDate { get; set; } = string.Empty;
  [StringLength(20)]
  public string Amount { get; set; } = string.Empty;
  [StringLength(100)]
  public string TransactionId { get; set; } = string.Empty;
  [StringLength(100)]
  public string TrackId { get; set; } = string.Empty;
  [StringLength(100)]
  public string ReferenceNo { get; set; } = string.Empty;
  [StringLength(100)]
  public string Pmntmode { get; set; } = string.Empty;
  [StringLength(100)]
  public string Type { get; set; } = string.Empty;
  [StringLength(100)]
  public string Card { get; set; } = string.Empty;
  [StringLength(100)]
  public string CardType { get; set; } = string.Empty;
  [StringLength(100)]
  public string Member { get; set; } = string.Empty;
  [StringLength(100)]
  public string PaymentId { get; set; } = string.Empty;
  public int? StudentId { get; set; }
  public string ApplicationNumber { get; set; } = string.Empty;
  public string FeeAmounts { get; set; } = string.Empty;
  public string FeeIds { get; set; } = string.Empty;
}
