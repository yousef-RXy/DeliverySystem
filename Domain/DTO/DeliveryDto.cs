
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO;
    public class DeliveryDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Merchant ID is required")]
        public required Guid MerchantId { get; set; }

        [Required(ErrorMessage = "Delivery Person ID is required")]
        public required Guid DeliveryPersonId { get; set; }

        [Required(ErrorMessage = "Package size is required")]
        [Range(0.1, 1000.0, ErrorMessage = "Package size must be between 0.1 and 1000.0")]
        public required double Size { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, 1000.0, ErrorMessage = "Weight must be between 0.1 and 1000.0")]
        public required double Weight { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
        public required string Address { get; set; }
    }
