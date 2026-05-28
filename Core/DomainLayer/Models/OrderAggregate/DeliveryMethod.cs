namespace DomainLayer.Models.OrderAggregate
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; } = null!;
    }
}
