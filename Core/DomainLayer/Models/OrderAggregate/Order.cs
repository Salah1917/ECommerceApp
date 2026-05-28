using DomainLayer.Models.OrderAggregate;

namespace DomainLayer.Models.OrderAggregate
{
    public class Order : BaseEntity<int>
    {
        private Order() { }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItem> items, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;
        public DeliveryMethod? DeliveryMethod { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; }
        public decimal GetTotal() => Subtotal + (DeliveryMethod?.Cost ?? 0);
        public string PaymentIntentId { get; set; } = null!;
    }
}
