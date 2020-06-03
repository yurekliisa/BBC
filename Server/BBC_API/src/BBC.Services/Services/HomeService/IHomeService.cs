using BBC.Core.Domain.Identity;
using BBC.Services.Services.Base;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.HomeService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.HomeService
{
    public interface IHomeService : IApplicationBaseServices<User, Role>
    {
        Task<List<SliderOutputDto>> GetSliderImages();
        Task<List<TaRHomeOuputDto>> GetHomeContent(int page);

        Task<List<PopularTaROutputDto>> GetTaRByPopularCategory();
        Task<List<PopularTaROutputDto>> GetTaRByForYou();

        Task<List<PopularChefOutputDto>> GetPopularChefs(SelectedTime time = SelectedTime.Daily);
        Task<List<PopularCategory>> GetPopularCategories();

    }
}
