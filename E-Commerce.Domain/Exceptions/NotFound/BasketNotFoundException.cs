using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class BasketNotFoundException(string id) : NotFoundException($"Basket with key {id} was not found")
    {
    }
}
