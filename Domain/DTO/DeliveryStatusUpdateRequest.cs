using Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace Domain.DTO;
public class DeliveryStatusUpdateRequest
{
    [Required(ErrorMessage = "Status is required")]
    public DeliveryStatus Status { get; set; }
}