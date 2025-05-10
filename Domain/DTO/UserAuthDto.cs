using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO;

public class UserAuthDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
    public required string Username { set; get; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
    public required string Password { set; get; }

    [Required(ErrorMessage = "Role is required")]
    public UserRole Role { set; get; }
}