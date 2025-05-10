
namespace Domain.DTO;
    public class DeliveryDto
    {
        public string Title { get; set; } = "";
        public required Guid MerchantId { get; set; }
        public required Guid DeliveryPersonId { get; set; }
        public required double Size { get; set; }
        public required double Weight { get; set; }
        public required string Address { get; set; }
    }
