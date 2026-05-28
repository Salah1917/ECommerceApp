namespace Shared.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public AddressDto ShippingAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = null!;
    }
}
