using Domain.Entities;

namespace Domain.DTO;

public class UserAuthDto
{
    public required string Username { set; get; }
    public required string Password { set; get; }
    public UserRole Role { set; get; }
}