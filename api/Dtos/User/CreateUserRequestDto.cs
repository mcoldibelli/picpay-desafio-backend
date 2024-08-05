using System.ComponentModel.DataAnnotations;
using api.Models.Enums;

namespace api.Dtos.User;

public class CreateUserRequestDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Document is required")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "Document must be a CNPJ or a CPF format")]
    public string Document { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 12, ErrorMessage = "Password must be between 12 and 100 characters")]
    public string Password { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value")]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "User type is required")]
    public UserType UserType { get; set; }
}
