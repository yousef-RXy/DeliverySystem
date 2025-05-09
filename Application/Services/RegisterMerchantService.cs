using Application.utils;
using Domain.Entities;
using Domain.Repositories;
using Domain.DTO;

namespace Application.Services;

public class RegisterMerchantService
{
    private readonly IUserRepository _repo;
    public RegisterMerchantService(IUserRepository repo) => _repo = repo;
    public async Task<UserDto?> RegisterAsync(string username, string password)
    {
        var User = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = Hash.HashPassword(password),
            Role = UserRole.Merchant.ToString()
        };

        await _repo.AddAsync(User);
        
        return new UserDto
        {
            Id = User.Id,
            Role = User.Role
        };
    }
}