namespace Domain.Entities;
public class DeliveryRequest
{
    public Guid Id { get; set; }
    public Guid MerchantId { get; set; }
    public string PackageSize { get; set; } = "";
    public double Weight { get; set; }
    public string Address { get; set; } = "";
    public DeliveryStatus Status { get; set; }
    public Guid DeliveryPersonId { get; set; }
}