using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CategoryService.Dto
{
    public class CategoryMapping:Profile, IMapperBase
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryListDto, Category>();
        }
    }
}
