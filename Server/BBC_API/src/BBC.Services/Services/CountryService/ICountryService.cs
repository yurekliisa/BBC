using BBC.Services.Services.Common.Base;
using BBC.Services.Services.CountryService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBC.Services.Services.CountryService
{
    public interface ICountryService : IBaseService
    {
        Task<List<CountryListDto>> GetAllCountries();
        Task<EditCountryDto> GetCountry(int Id);
        Task CreateCountry(CreateCountryDto input);
        Task EditCountry(EditCountryDto input);
        Task DeleteCountry(int Id);
    }
}
