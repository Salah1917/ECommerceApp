namespace DomainLayer.Models.OrderAggregate
{
    public class OrderItem : BaseEntity<int>
    {
        private OrderItem() { }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public ProductItemOrdered Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
