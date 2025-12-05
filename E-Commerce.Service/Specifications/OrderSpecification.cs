using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(Guid id, string userEmail) : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
        public OrderSpecification(string userEmail) : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }
    }
}
