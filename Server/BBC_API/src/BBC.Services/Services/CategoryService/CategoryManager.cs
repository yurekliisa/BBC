using AutoMapper;
using BBC.Core.Dependency;
using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Exceptions;
using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CategoryService
{
    public class CategoryManager:ICategoryService,ITransientDI
    {
        private readonly IRepositoryBase<BBCContext, Category, int> _repository;
        private readonly IMapper _mapper;
        public CategoryManager(IRepositoryBase<BBCContext, Category, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void GetData()
        {
            var dd = _repository.GetList();
            var bb = _mapper.Map<List<CategoryDto>>(dd);
        }

        public void InsertData()
        {   
            var insert = _repository.Insert(new Category() { Name = "testetet" });
            _repository.SaveChanges();
        }

        public void Exception()
        {
            throw new ServiceException("trial exception");
        }
    }
}
