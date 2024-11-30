using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;
using Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public IQueryable<Category?> GetCategoryWithProducts()
        {
            return Context.Categories.Include(c => c.Products).AsQueryable();
        }

        public async Task<Category> GetCategoryWithProductAsync(string id)
        {
            var categroy = await Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));

            return categroy;
        }
    }
}
