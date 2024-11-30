using AutoMapper;
using Repositories.Categories;
using Services.Categories.CategoryRequests;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping.CategoryMapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            #region Category Mappings
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            #endregion
        }
    }
}
