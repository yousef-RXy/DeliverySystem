using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class UpdateDeliveryStatusService
{
    private readonly IDeliveryRequestRepository _repo;

    public UpdateDeliveryStatusService(IDeliveryRequestRepository repo)
    {
        _repo = repo;
    }

    public async Task UpdateStatusAsync(Guid deliveryId, DeliveryStatus status)
    {
        await _repo.UpdateStatusAsync(deliveryId, status);
    }
}
