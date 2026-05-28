using DomainLayer.Models.Basket;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
