using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.ProductRequests
{
    public record CreateProductRequest(string Name, int Stock, decimal Price, Guid CategoryId);
}
