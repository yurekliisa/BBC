using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Base;
using BBC.Services.Services.CategoryService.Dto;
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
        private readonly IRepositoryBase<BBCContext, Popularity, int> _popularityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TarifAndReceteManager(
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository,
            IRepositoryBase<BBCContext, Popularity, int> popularityRepository,
            IHostingEnvironment hostingEnvironment
            )
        {
            _tarRepository = tarRepository;
            _hostingEnvironment = hostingEnvironment;
            _popularityRepository = popularityRepository;
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
            try
            {

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
            catch (Exception ex)
            {
                var b = ex.Message;
            }
            return 0;
        }

        public async Task DeleteTarifAndRecete(int Id)
        {
            await _tarRepository.DeleteAsync(Id);
        }

        public async Task EditTarifAndRecete(UserTarifAndReceteDto input)
        {
            var tar = await _tarRepository.FindAsync(input.Id);
            _mapper.Map(tar, input);
            await _tarRepository.UpdateAsync(tar);
        }

        public async Task<List<TarifAndReceteListDto>> GetAllTarifAndRecetes(int page)
        {
            var tar = await _tarRepository.GetListAsync();
            //Include Join yapar
            List<TarifAndReceteListDto> result = new List<TarifAndReceteListDto>();
            List<TarifAndRecete> query = await _tarRepository.GetQueryable()
            .Include(x => x.Content)
            .Include(x => x.Popularities)
            .Include(x => x.User)
            .Include(x => x.TaRCategories)
            .Include("TaRCategories.Category")
            .OrderByDescending(y => y.CreateTime)
            .Skip((page - 1) * 18)
            .Take(18).ToListAsync();
            result = query.Select(y => new TarifAndReceteListDto()
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
            return result;
        }

        public async Task<TarifAndReceteDetailDto> GetTarifAndReceteDetails(int tarId)
        {
            TarifAndReceteDetailDto result = new TarifAndReceteDetailDto();
            try
            {
                var data = await _tarRepository.GetQueryable()
                .Include(x => x.Content)
               .Include(x => x.Popularities)
                .Include(x => x.User)
                .Include(x => x.TaRCategories)
                .Include("TaRCategories.Category")
                .Include("Popularities.User")
                .FirstOrDefaultAsync(x => x.Id == tarId);
                if (data != null)
                {
                    result.Id = data.Id;
                    result.Title = data.Content.Title;
                    result.ShortDescription = data.Content.ShortDescription;
                    result.ContentText = data.Content.ContentText;
                    result.MainImage = data.Content.MainImage;
                    result.UserId = data.UserId;
                    result.UserFullName = data.User.UserName + " " + data.User.SurName;
                    result.UserPhoto = data.User.Photo;
                    result.CommentCount = data.Popularities.Count;
                    result.Puan = data.Popularities.Count > 0 ? (data.Popularities.Sum(x => x.Puan) / data.Popularities.Count()) : 0;
                    result.CommentDtos = data.Popularities.Where(x => !String.IsNullOrEmpty(x.Comment)).Select(x => new CommentDto()
                    {
                        Comment = x.Comment,
                        CommentDate = x.CreationTime.ToString("dd-MM-yyyy"),
                        Puan = x.Puan,
                        UserId = x.UserId,
                        UserName = x.User.UserName,
                        UserPhoto = x.User.Photo,
                        TaRId=(int)x.TarifAndReceteId
                    }).ToList();
                    result.Categories = data.TaRCategories.Select(y => y.Category.Name).ToList();
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return result;
        }
        public async Task<List<UserTarifAndReceteDto>> GetTarifAndReceteByUserId(int Id)
        {
            var query = _tarRepository.GetQueryable()
                .Include(x => x.Content)
                .Include(x => x.Popularities)
                .Include(x => x.User)
                .Include(x => x.TaRCategories)
                .Include("TaRCategories.Category")
                .Where(x => x.UserId == Id)
                .OrderByDescending(y => y.CreateTime).AsQueryable();
            var result = await query.Select(y => new UserTarifAndReceteDto()
            {
                Id = y.Id,
                Content = new ContentDto()
                {
                    ContentText = y.Content.ContentText,
                    MainImage = y.Content.MainImage,
                    ShortDescription = y.Content.ShortDescription,
                    Title = y.Content.Title,
                    CommentCount = y.Popularities.Count,
                    Puan = y.Popularities.Count > 0 ? (y.Popularities.Sum(x => x.Puan) / y.Popularities.Count()) : 0,
                },
                Categories = y.TaRCategories.Select(y => new CategoryListDto()
                {
                    Id = y.CategoryId,
                    Name = y.Category.Name,
                }).ToList(),
            }).ToListAsync();
            return result;
        }


        public async Task Comment(CommentDto input)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
                return;

            await _popularityRepository.InsertAsync(new Popularity()
            {
                Comment = input.Comment,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Puan = input.Puan,
                TarifAndReceteId = input.TaRId,
                UserId = user.Id
            });

            await _popularityRepository.SaveChangesAsync();
        }

        public async Task<List<CommentDto>> GetAllComments(int tarId)
        {
            var comment = await _popularityRepository.GetQueryable().Include(z => z.User).Where(x => x.TarifAndReceteId == tarId && !String.IsNullOrEmpty(x.Comment))
                .Select(y => new CommentDto()
                {
                    Comment = y.Comment,
                    CommentDate = y.CreationTime.ToString("dd-MM-yyyy"),
                    Puan = y.Puan,
                    TaRId = (int)y.TarifAndReceteId,
                    UserId = y.UserId,
                    UserName = y.User.UserName,
                    UserPhoto = y.User.Photo
                }).ToListAsync();
            return comment;
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
