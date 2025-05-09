using Domain.Entities;

namespace Domain.Repositories;

public interface IMerchantRepository
{
    Task<Merchant?> GetByUsernameAsync(string username);
    Task AddAsync(Merchant merchant);
}