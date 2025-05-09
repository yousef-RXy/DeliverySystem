
namespace Domain.DTO;
    public class DeliveryDto
    {
        public required Guid MerchantId { get; set; }
        public required Guid DeliveryPersonId { get; set; }
        public required string Size { get; set; }
        public required double Weight { get; set; }
        public required string Address { get; set; }
    }
