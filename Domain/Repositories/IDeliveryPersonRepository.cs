using Domain.Entities;

namespace Domain.Repositories;

public interface IDeliveryPersonRepository
{
    Task AddAsync(DeliveryPerson person);
    Task<DeliveryPerson?> GetByIdAsync(Guid id);
    Task<List<DeliveryPerson>> GetAllAsync();
}