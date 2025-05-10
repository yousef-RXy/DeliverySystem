using Application.utils;
using Domain.Entities;
using Domain.Repositories;
using Domain.DTO;

namespace Application.Services;

public class RegisterUserService
{
    private readonly IUserRepository _repo;
    public RegisterUserService(IUserRepository repo) => _repo = repo;
    public async Task<UserDto?> RegisterAsync(UserAuthDto dto)
    {
        var User = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            PasswordHash = Hash.HashPassword(dto.Password),
            Role = dto.Role.ToString()
        };

        await _repo.AddAsync(User);
        
        return new UserDto
        {
            Id = User.Id,
            Role = User.Role
        };
    }
}