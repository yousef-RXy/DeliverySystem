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
            MerchantId = dto.MerchantId,
            //DeliveryPersonId = dto.DeliveryPersonId,
            DeliveryPersonId = Guid.Parse("5d93ad37-9238-4f57-98c8-c1018b46507d"),
            PackageSize = dto.Size,
            Weight = dto.Weight,
            Address = dto.Address,
            Status = DeliveryStatus.Requested
        };

        await _repo.AddAsync(request);
    }


}