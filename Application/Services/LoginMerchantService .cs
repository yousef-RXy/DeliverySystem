using Application.utils;
using Domain.DTO;
using Domain.Repositories;

namespace Application.Services;

public class LoginMerchantService
{
    private readonly IUserRepository _userRepo;

    public LoginMerchantService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<UserDto?> LoginAsync(string username, string password)
    {
        var user = await _userRepo.GetByUsernameAsync(username);
        if (user == null) return null;

        var PasswordHash = Hash.HashPassword(password);
        if (user.PasswordHash != PasswordHash) return null;

        return new UserDto
        {
            Id = user.Id,
            Role = user.Role
        };
    }
}
