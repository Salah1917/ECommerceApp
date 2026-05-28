using System.ComponentModel.DataAnnotations;

namespace Shared.DTOS
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; } = null!;
        [Required]
        public string BasketId { get; set; } = null!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; } = null!;
    }
}
