using E_Commerce.Shared.Dtos.Baskets;


namespace E_Commerce.Service.Abstraction.Payments
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
        Task UpdateOrderPaymentStatusAsync(string jsonRequest, string stripeHeader);

    }
}
