using BBC.Services.Services.Common.Base;
using BBC.Services.Services.HomeService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.HomeService
{
    public interface IHomeService:IBaseService
    {
        Task<List<SliderOutputDto>> GetSliderImages();
        Task<List<TaRHomeOuputDto>> GetHomeContent(int page);

    }
}
