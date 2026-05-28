using DomainLayer.Contracts;
using DomainLayer.Models.Basket;
using DomainLayer.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Stripe;
using Product = DomainLayer.Models.Product;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket is null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingPrice = shippingPrice;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepo.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
