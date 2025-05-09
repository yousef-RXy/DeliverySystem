using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.DTO;

[ApiController]
[Route("api/deliveries")]
public class DeliveryController : ControllerBase
{
    private readonly RequestDeliveryService __requestservice;
    private readonly UpdateDeliveryStatusService _statusService;
    private readonly ViewAssignedDeliveriesService _assignedService;
    public DeliveryController(RequestDeliveryService requestservice, UpdateDeliveryStatusService statusService,
                              ViewAssignedDeliveriesService assignedService) 
    {
        __requestservice = requestservice;
        _statusService = statusService;
        _assignedService = assignedService;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestDelivery([FromBody] DeliveryDto dto)
    {
        await __requestservice.RequestAsync(dto);
        return Ok();
    }
    
    [HttpGet("assigned/{deliveryPersonId}")]
    public async Task<IActionResult> GetAssigned(Guid deliveryPersonId)
    {
        var deliveries = await _assignedService.GetAssignedAsync(deliveryPersonId);
        return Ok(deliveries);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] DeliveryStatusUpdateRequest request)
    {
        await _statusService.UpdateStatusAsync(id, request.Status);
        return NoContent();
    }
}
