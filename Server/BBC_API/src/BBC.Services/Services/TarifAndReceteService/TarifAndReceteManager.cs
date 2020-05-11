using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Base;
using BBC.Services.Services.TarifAndReceteService.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.Services.Services.TarifAndReceteService
{
    public class TarifAndReceteManager : ApplicationBaseServices<User, Role>, ITarifAndReceteService
    {
        private readonly IRepositoryBase<BBCContext, TarifAndRecete, int> _tarRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TarifAndReceteManager(
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository,
            IHostingEnvironment hostingEnvironment
            )
        {
            _tarRepository = tarRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<int> CreateTaR(CreateTarifAndReceteDto input)
        {
            var user = await GetCurrentUserAsync();
            var tar = _mapper.Map<TarifAndRecete>(input);
            foreach (var id in input.Categories)
            {
                tar.TaRCategories.Add(new TaRCategory()
                {
                    CategoryId = id
                });
            }
            tar.UserId = user.Id;
            var newMediaName = await saveFile(input.Content.MainImage);
            if (newMediaName != null)
            {
                tar.Content.MainImage = newMediaName;
            }
            var result = await _tarRepository.InsertAsync(tar);
            return result.Id;
        }
        private async Task<string> saveFile(IFormFile file)
        {

            if (file.Length > 0)
            {
                if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload"))
                {
                    Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\");
                }
                var newName = "\\Upload\\" + Guid.NewGuid() + ".jpeg";
                using (FileStream fileStream = System.IO.File.Create(_hostingEnvironment.WebRootPath + newName))
                {
                    file.CopyTo(fileStream);
                    await fileStream.FlushAsync();
                    return newName;
                }
            }
            return null;
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

        public async Task<List<TarifAndReceteDetailDto>> GetAllTarifAndReceteDetails()
        {
            var tar = await _tarRepository.GetListAsync();
            var result = _mapper.Map<List<TarifAndReceteDetailDto>>(tar);
            return result;
        }

        public async Task<List<EditTarifAndReceteDto>> GetTarifAndRecete(int Id)
        {
            var tar = await _tarRepository.GetQueryable().Where(x => x.UserId == Id).ToListAsync();
            var result = _mapper.Map<List<EditTarifAndReceteDto>>(tar);
            return result;
        }
    }
}
