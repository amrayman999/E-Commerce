using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.UnAuthorized
{
    public class UnAuthorizedException() : Exception("you are not authorized")
    {
    }
}
