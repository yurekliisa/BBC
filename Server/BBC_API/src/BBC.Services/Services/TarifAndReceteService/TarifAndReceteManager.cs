using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Base;
using BBC.Services.Services.TarifAndReceteService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.TarifAndReceteService
{
    public class TarifAndReceteManager : ApplicationBaseServices<User,Role>, ITarifAndReceteService
    {
        private readonly IRepositoryBase<BBCContext,TarifAndRecete,int> _tarRepository;
        public TarifAndReceteManager(
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository
            )
        {
            _tarRepository = tarRepository;
        }

        public async Task<int> CreateTaR(CreateTarifAndReceteDto input)
        {
            //var user = await GetCurrentUserAsync();
            var tar = _mapper.Map<TarifAndRecete>(input);
            foreach (var category in input.Categories)
            {
                tar.TaRCategories.Add(new TaRCategory()
                {
                    CategoryId = category.Id
                });
            }
            tar.UserId = 1;//user.Id;
            tar.Content.Medias = _mapper.Map<ICollection<Media>>(input.Content.MediaDtos);
            var result = await _tarRepository.InsertAsync(tar);
            return result.Id;
        }

        public async Task DeleteTarifAndRecete(int Id)
        {
            await _tarRepository.DeleteAsync(Id);
        }

        public async Task EditTarifAndRecete(EditTarifAndReceteDto input)
        {
            var tar = await _tarRepository.FindAsync(input.Id);
            _mapper.Map(tar, input);
            await _tarRepository.UpdateAsync(tar);
        }

        public async Task<List<TarifAndReceteListDto>> GetAllTarifAndRecetes()
        {
            var tar = await _tarRepository.GetListAsync();
            var result = _mapper.Map<List<TarifAndReceteListDto>>(tar);
            return result;
        }

        public async Task<List<TarifAndReceteDetailListDto>> GetAllTarifAndReceteDetails()
        {
            var tar = await _tarRepository.GetListAsync();
            var result = _mapper.Map<List<TarifAndReceteDetailListDto>>(tar);
            return result;
        }

        public async Task<EditTarifAndReceteDto> GetTarifAndRecete(int Id)
        {
            var tar = await _tarRepository.GetAsync(Id);
            var result = _mapper.Map<EditTarifAndReceteDto>(tar);
            return result;
        }
    }
}
