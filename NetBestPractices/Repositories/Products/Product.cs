using Repositories.Categories;
using Repositories.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}
