using AutoMapper;
using FinancialManager.Core.DTOs;
using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Core.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CategoryCreateDto, Category>();
        }
    }
}
