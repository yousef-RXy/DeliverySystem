

using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.DTO;


namespace WebApi.Controllers;

[ApiController]
[Route("api/delivery-people")]
public class DeliveryPerson : ControllerBase
{
    private readonly GetDeliveryPeopleService _service;
    public DeliveryPerson(GetDeliveryPeopleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDeliveryPeople()
    {
        var deliveryPeople = await _service.Execute();
        return Ok(deliveryPeople);
    }
}