using Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.ProductServices
{
    public interface IProductService
    {
        public Task<ServiceResult<List<Product>>> GetTopPriceAsync(int count);
    }
}
