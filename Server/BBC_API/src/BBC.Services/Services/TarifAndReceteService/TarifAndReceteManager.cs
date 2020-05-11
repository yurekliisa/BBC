using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Base;
using BBC.Services.Services.HomeService.Dto;
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

        public async Task<List<TaRHomeOuputDto>> GetTarifAndRecetes(int page)
        {
            //Include Join yapar
            List<TaRHomeOuputDto> result = new List<TaRHomeOuputDto>();
            try
            {
                List<TarifAndRecete> query = await _tarRepository.GetQueryable()
                .Include(x => x.Content)
                .Include(x => x.Popularities)
                .Include(x => x.User)
                .Include(x => x.TaRCategories)
                .Include("TaRCategories.Category")
                //.Where(x=>x.isDeleted != true && x.isActive== true)
                //.OrderBy(y => y.Id)
                .OrderByDescending(y => y.CreateTime)
                .Skip((page - 1) * 18)
                .Take(18).ToListAsync();
                result = query.Select(y => new TaRHomeOuputDto()
                {
                    Id = y.Id,
                    Title = y.Content.Title,
                    ShortDescription = y.Content.ShortDescription,
                    MainImage = y.Content.MainImage,
                    UserId = y.UserId,
                    UserFullName = y.User.UserName + " " + y.User.SurName,
                    UserPhoto = y.User.Photo,
                    Puan = y.Popularities.Count > 0 ? (y.Popularities.Sum(x => x.Puan) / y.Popularities.Count()) : 0,
                    CommentCount = y.Popularities.Count(x => x.Comment != null),
                    Categories = y.TaRCategories.Select(y => y.Category.Name).ToList(),
                }).ToList();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return result;
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

        public async Task<List<TarifAndReceteDetailDto>> GetAllTarifAndReceteDetails()
        {
            List<TarifAndReceteDetailDto> result = new List<TarifAndReceteDetailDto>();
            try
            {
                List<TarifAndRecete> query = await _tarRepository.GetQueryable()
                .Include(x => x.Content)
                .Include(x => x.Popularities)
                .Include(x => x.User)
                .Include(x => x.TaRCategories)
                .Include("TaRCategories.Category")
                //.Where(x=>x.isDeleted != true && x.isActive== true)
                //.OrderBy(y => y.Id)
                .OrderByDescending(y => y.CreateTime).ToListAsync();
                result = query.Select(y => new TarifAndReceteDetailDto()
                {
                    Id = y.Id,
                    Title = y.Content.Title,
                    ShortDescription = y.Content.ShortDescription,
                    MainImage = y.Content.MainImage,
                    UserId = y.UserId,
                    UserFullName = y.User.UserName + " " + y.User.SurName,
                    UserPhoto = y.User.Photo,
                    Puan = y.Popularities.Count > 0 ? (y.Popularities.Sum(x => x.Puan) / y.Popularities.Count()) : 0,
                    CommentCount = y.Popularities.Count(x => x.Comment != null),
                    Categories = y.TaRCategories.Select(y => y.Category.Name).ToList(),
                }).ToList();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return result;
        }

        public async Task<List<EditTarifAndReceteDto>> GetTarifAndReceteByUserId(int Id)
        {
            var tar = await _tarRepository.GetQueryable().Where(x => x.UserId == Id).ToListAsync();
            var result = _mapper.Map<List<EditTarifAndReceteDto>>(tar);
            return result;
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

    }
}
