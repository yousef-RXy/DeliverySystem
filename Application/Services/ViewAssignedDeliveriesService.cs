using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class ViewAssignedDeliveriesService
{
    private readonly IDeliveryRequestRepository _repo;

    public ViewAssignedDeliveriesService(IDeliveryRequestRepository repo)
    {
        _repo = repo;
    }

    public Task<List<DeliveryRequest>> GetAssignedAsync(Guid deliveryPersonId)
    {
        return _repo.GetAssignedAsync(deliveryPersonId);
    }
}