using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;
using Repositories.GenericRepositories;

namespace Repositories.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetTopPriceAsync(int count)
        {
            return await Context.Products.OrderByDescending(x=>x.Price).Take(count).ToListAsync();
        }
    }
}
