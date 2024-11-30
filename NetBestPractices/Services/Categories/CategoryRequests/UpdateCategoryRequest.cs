using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Categories.CategoryRequests
{
    public record UpdateCategoryRequest(string Id, string Name);
}
