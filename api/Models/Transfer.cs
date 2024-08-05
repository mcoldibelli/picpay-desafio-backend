using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Transfers")]
public class Transfer
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Value is required")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Payer is required")]
    public int PayerId { get; set; }

    [ForeignKey(nameof(PayerId))]
    public User Payer { get; set; }

    [Required(ErrorMessage = "Payee is required")]
    public int PayeeId { get; set; }

    [ForeignKey(nameof(PayeeId))]
    public User Payee { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;
}
