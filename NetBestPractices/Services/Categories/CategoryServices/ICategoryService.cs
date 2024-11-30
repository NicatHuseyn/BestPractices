using Repositories.Categories;
using Services.Categories.CategoryRequests;
using Services.Categories.CategoryResponses;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Categories.CategoryServices
{
    public interface ICategoryService
    {
        // Crud Operation

        #region Get Methods
        Task<ServiceResult<List<CategoryDto>>> GetAllCategoryListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdCategory(string id);
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(string id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
        #endregion


        #region Set Methods
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request, string id);
        Task<ServiceResult> DeleteAsync(string id);
        #endregion
    }
}
