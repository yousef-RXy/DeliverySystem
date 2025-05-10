using Domain.DTO;
using Domain.Repositories;

namespace Application.Services;

public class GetDeliveryPeopleService
{
    private readonly IUserRepository _userRepository;
    public GetDeliveryPeopleService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<DeliveryPersonDto>> Execute()
    {
        return await _userRepository.GetDeliveryPeople();
    }
}
