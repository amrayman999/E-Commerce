using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.BadRequest
{
    public class CreateOrUpdateBasketBadRequest() : BadRequestException("Invalid Operation when create or update basket")
    {
    }
}
