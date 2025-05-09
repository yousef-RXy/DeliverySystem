using Application.utils;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class LoginMerchantService
{
    private readonly IMerchantRepository _merchantRepo;

    public LoginMerchantService(IMerchantRepository merchantRepo)
    {
        _merchantRepo = merchantRepo;
    }

    public async Task<Guid?> LoginAsync(string username, string password)
    {
        var merchant = await _merchantRepo.GetByUsernameAsync(username);
        if (merchant == null) return null;

        var PasswordHash = Hash.HashPassword(password);
        if (merchant.PasswordHash != PasswordHash) return null;
        return merchant.Id;
    }
}
