using Domain.DTO;
using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<List<DeliveryPersonDto>> GetDeliveryPeople();
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User merchant);
}