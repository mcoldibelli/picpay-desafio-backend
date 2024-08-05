using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Transfer;

public class CreateTransferRequestDto
{
    [Required(ErrorMessage = "Value is required")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Payer is required")]
    public int PayerId { get; set; }

    [Required(ErrorMessage = "Payee is required")]
    public int PayeeId { get; set; }
}
