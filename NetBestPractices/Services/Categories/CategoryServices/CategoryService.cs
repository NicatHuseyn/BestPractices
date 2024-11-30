using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Categories;
using Repositories.Products;
using Repositories.UnitOfWork;
using Services.Categories.CategoryRequests;
using Services.Categories.CategoryResponses;
using Services.DTOs;
using System.Net;

namespace Services.Categories.CategoryServices
{
    public class CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        #region Set Methods
        public async Task<ServiceResult<List<CategoryDto>>> GetAllCategoryListAsync()
        {
            var categories = await repository.GetAll().ToListAsync();

            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
            
            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);

        }

        public async Task<ServiceResult<CategoryDto>> GetByIdCategory(string id)
        {
            var category = await repository.GetByIdAsync(Guid.Parse(id));
            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category Not Found");
            }
            var categoryAsDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(string id)
        {
            var category = await repository.GetCategoryWithProductAsync(id);
            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category Not Found", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var categories = await repository.GetCategoryWithProducts().ToListAsync();

            var categoryWithProductsDtos = mapper.Map<List<CategoryWithProductsDto>>(categories);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryWithProductsDtos);
        }
        #endregion

        #region Set Methods
        public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await repository.GetWhere(c => c.Name == request.Name).AnyAsync();

            if (anyCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Fail("Category already exsist", HttpStatusCode.NotFound);
            }

            var category = mapper.Map<Category>(request);
            await repository.AddAsync(category);
            await unitOfWork.SaveAsync();

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new(category.Id.ToString()), $"api/categories/{category.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request, string id)
        {
            var category = await repository.GetByIdAsync(Guid.Parse(id));
            if (category is null)
            {
                return ServiceResult.Fail("Category Not Found", HttpStatusCode.NotFound);
            }

            var isCategoryNameExsist = await repository.GetWhere(c => c.Name == request.Name && c.Id != category.Id).AnyAsync();

            if (isCategoryNameExsist)
            {
                return ServiceResult.Fail("Category already exsist", HttpStatusCode.BadRequest);
            }

            category = mapper.Map(request, category);
            repository.Update(category);
            await unitOfWork.SaveAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(string id)
        {
            var category = await repository.GetByIdAsync(Guid.Parse(id));
            if (category is null)
            {
                return ServiceResult.Fail("Category Not Found", HttpStatusCode.NotFound);
            }

            repository.Delete(category);
            await unitOfWork.SaveAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        #endregion
    }
}
