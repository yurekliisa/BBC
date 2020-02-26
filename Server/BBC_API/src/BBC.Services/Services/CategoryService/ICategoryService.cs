using BBC.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CategoryService
{
    public interface ICategoryService: ITransientDI
    {
        void GetData();
        void InsertData();
    }
}
