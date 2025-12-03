using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction.Orders;
using E_Commerce.Shared.Dtos.Orders;


namespace E_Commerce.Service.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if(basket is null) throw new BasketNotFoundException(request.BasketId);

            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(item.Id);
                if(product is null) throw new ProductNotFoundException(item.Id);
                if(product.Price != item.Price) item.Price = product.Price;
                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);


            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();
            return _mapper.Map<OrderResponse>(order);
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
