using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class ProductNotFoundException(int id) : 
        NotFoundException($"Product with id {id} was not found.")
    {
    }
}
