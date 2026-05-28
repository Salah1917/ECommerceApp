using DomainLayer.Contracts;
using DomainLayer.Models.Basket;
using DomainLayer.Models.OrderAggregate;
using ServiceAbstraction;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, DomainLayer.Models.OrderAggregate.Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                var productRepo = _unitOfWork.GetRepository<DomainLayer.Models.Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(deliveryMethodId);

            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var allOrders = await orderRepo.GetAllAsync();
            var existingOrder = allOrders.FirstOrDefault(o => o.PaymentIntentId == basket?.PaymentIntentId);
            if (existingOrder is not null)
            {
                orderRepo.Remove(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(
                buyerEmail: buyerEmail,
                shippingAddress: shippingAddress,
                deliveryMethod: deliveryMethod,
                items: orderItems,
                subtotal: subtotal,
                paymentIntentId: basket?.PaymentIntentId ?? ""
            );

            await orderRepo.AddAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsync();
            return orders.Where(o => o.BuyerEmail == buyerEmail).ToList();
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var order = await _unitOfWork.GetRepository<Order, int>().GetByIdAsync(orderId);
            return order?.BuyerEmail == buyerEmail ? order : null;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => (await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync()).ToList();
    }
}
