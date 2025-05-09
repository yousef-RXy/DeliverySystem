using Application.utils;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class RegisterMerchantService
{
    private readonly IMerchantRepository _repo;
    public RegisterMerchantService(IMerchantRepository repo) => _repo = repo;
    public async Task<Guid?> RegisterAsync(string username, string password)
    {
        var merchant = new Merchant
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = Hash.HashPassword(password)
        };

        await _repo.AddAsync(merchant);
        
        return merchant.Id;
    }
}