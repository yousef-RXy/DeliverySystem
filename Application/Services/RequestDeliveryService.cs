using Domain.Entities;
using Domain.Repositories;
using Domain.DTO;

namespace Application.Services;

public class RequestDeliveryService
{
    private readonly IDeliveryRequestRepository _repo;
    public RequestDeliveryService(IDeliveryRequestRepository repo) => _repo = repo;

    public async Task RequestAsync(DeliveryDto dto)
    {
        var request = new DeliveryRequest
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            MerchantId = dto.MerchantId,
            DeliveryPersonId = dto.DeliveryPersonId,
            PackageSize = dto.Size,
            Weight = dto.Weight,
            Address = dto.Address,
            Status = DeliveryStatus.Requested
        };

        await _repo.AddAsync(request);
    }


}