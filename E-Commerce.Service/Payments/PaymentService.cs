using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction.Payments;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.Extensions.Configuration;
using Stripe;


namespace E_Commerce.Service.Payments
{
    public class PaymentService(IBasketRepository _basketRepository, 
        IUnitOfWork _unitOfWork, IConfiguration configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {

            StripeConfiguration.ApiKey = configuration["StripeOptions:SekretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Domain.Entities.Products.Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException(-1);

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingCost = deliveryMethod.Price;
            var amount = (long)basket.Items.Sum(I => I.Price * I.Quantity) + basket.ShippingCost;

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if(basket.PaymentInentId is null)
            {

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options);
                
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(amount * 100),
                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentInentId, options);

            }
            basket.PaymentInentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));
            return _mapper.Map<BasketDto>(basket);

        }
        public async Task UpdateOrderPaymentStatusAsync(string jsonRequest, string stripeHeader)
        {
            var endpointSecret = configuration.GetRequiredSection("StripeOptions")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                      stripeHeader, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailedAsync(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentReceivedAsync(paymentIntent.Id);
                    break;
                // ... handle other event types
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }


        }
        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order.Status = OrderStatus.PaymentSuccess;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order.Status = OrderStatus.PaymentFailed;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
