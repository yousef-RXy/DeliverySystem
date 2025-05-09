using Domain.Entities;

namespace Domain.Repositories;

public interface IDeliveryRequestRepository
{
    Task AddAsync(DeliveryRequest request);
    Task<List<DeliveryRequest>> GetAssignedAsync(Guid deliveryPersonId);
    Task UpdateStatusAsync(Guid deliveryId, DeliveryStatus status);
}