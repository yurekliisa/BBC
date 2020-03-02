using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.CountryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.CountryService
{
    public class CountryManager : BaseService, ICountryService
    {
        //inject işlemi
        private readonly IRepositoryBase<BBCContext, Country, int> _countryRepository;
        public CountryManager(IRepositoryBase<BBCContext, Country, int> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task CreateCountry(CreateCountryDto input)
        {
            var country = _mapper.Map<Country>(input);
            await _countryRepository.InsertAsync(country);
        }

        public async Task DeleteCountry(int Id)
        {
            await _countryRepository.DeleteAsync(Id);
        }

        public async Task EditCountry(EditCountryDto input)
        {
            var country = await _countryRepository.FindAsync(input.Id);
            _mapper.Map(country, input);
            await _countryRepository.UpdateAsync(country);
        }
            

        public async Task<List<CountryListDto>> GetAllCountries()
        {
            var countries = await _countryRepository.GetListAsync();
            var result = _mapper.Map<List<CountryListDto>>(countries);
            return result;
        }

        public async Task<EditCountryDto> GetCountry(int Id)
        {
            var country = await _countryRepository.GetAsync(Id);
            var result = _mapper.Map<EditCountryDto>(country);
            return result;
        }
    }
}
