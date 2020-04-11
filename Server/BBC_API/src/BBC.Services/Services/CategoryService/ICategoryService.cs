using BBC.Core.Dependency;
using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.CategoryService
{
    public interface ICategoryService: IBaseService, ITransientDI
    {
        Task<List<CategoryListDto>> GetAllCategories();
        Task CreateCategories(CreateCategoryDto input);
        Task EditCategories(EditCategoryDto input);
        Task DeleteCategory(int id);
        void GetData();
        void InsertData();
        void Exception();
    }
}
