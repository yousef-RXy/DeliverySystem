using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class ViewRequestedDeliveriesService
{
    private readonly IDeliveryRequestRepository _repo;

    public ViewRequestedDeliveriesService(IDeliveryRequestRepository repo)
    {
        _repo = repo;
    }

    public Task<List<DeliveryRequest>> Execute(Guid MerchantId)
    {
        return _repo.GetRequestedAsync(MerchantId);
    }
}