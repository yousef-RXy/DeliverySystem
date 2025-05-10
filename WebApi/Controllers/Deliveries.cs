using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.DTO;

[ApiController]
[Route("api/deliveries")]
public class DeliveryController : ControllerBase
{
    private readonly RequestDeliveryService _requestservice;
    private readonly UpdateDeliveryStatusService _statusService;
    private readonly ViewAssignedDeliveriesService _assignedService;
    private readonly ViewRequestedDeliveriesService _viewRequestservice;
    public DeliveryController(RequestDeliveryService requestservice, UpdateDeliveryStatusService statusService,
                              ViewAssignedDeliveriesService assignedService, ViewRequestedDeliveriesService viewRequestservice) 
    {
        _requestservice = requestservice;
        _statusService = statusService;
        _assignedService = assignedService;
        _viewRequestservice = viewRequestservice;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestDelivery([FromBody] DeliveryDto dto)
    {
        await _requestservice.RequestAsync(dto);
        return Ok();
    }
    
    [HttpGet("assigned/{deliveryPersonId}")]
    public async Task<IActionResult> GetAssigned(Guid deliveryPersonId)
    {
        var deliveries = await _assignedService.GetAssignedAsync(deliveryPersonId);
        return Ok(deliveries);
    }
    
    [HttpGet("requested/{merchantId}")]
    public async Task<IActionResult> GetRequested(Guid merchantId)
    {
        var deliveries = await _viewRequestservice.Execute(merchantId);
        return Ok(deliveries);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] DeliveryStatusUpdateRequest request)
    {
        await _statusService.UpdateStatusAsync(id, request.Status);
        return NoContent();
    }
}
