using Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<List<Product>> GetTopPriceAsync(int count);
    }
}
