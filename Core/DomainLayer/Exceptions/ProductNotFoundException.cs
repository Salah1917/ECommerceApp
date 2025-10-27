using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class ProductNotFoundException(int Id) : NotFoundException($"The product with id {Id} is not found")
    {

    }
}
