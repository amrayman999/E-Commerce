using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Orders
{
    public class OrderItem : Entity<int>
    {
        public OrderItem() { }
        public OrderItem(ProductInOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public ProductInOrderItem Product {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
