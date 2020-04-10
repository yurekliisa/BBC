using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.HomeService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.HomeService
{
    public class HomeManager : BaseService, IHomeService
    {
        private readonly IRepositoryBase<BBCContext, Popularity, int> _popularityRepository;
        private readonly IRepositoryBase<BBCContext, TarifAndRecete, int> _tarRepository;
        public HomeManager(
            IRepositoryBase<BBCContext, Popularity, int> popularityRepository,
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository

            )
        {
            _tarRepository = tarRepository;
            _popularityRepository = popularityRepository;

        }

        public async Task<List<SliderOutputDto>> GetSliderImages()
        {
            List<SliderOutputDto> result = new List<SliderOutputDto>();
            try
            {
                var popularties = await _popularityRepository.GetQueryable()
                .Where(y => y.IsDeleted == false)
                .GroupBy(y => y.TaRId).Select(y => new
                {
                    TotalPuan = y.Sum(x => x.Puan)/ y.Count(),
                    Id = y.Key,
                }).OrderByDescending(y => y.TotalPuan).Take(12).ToListAsync();
                foreach (var popularty in popularties)
                {
                    var tar = await _tarRepository.GetQueryable().Include(y => y.Content).FirstOrDefaultAsync(x => x.Id == popularty.Id);
                    if (tar != null)
                        result.Add(new SliderOutputDto()
                        {
                            Id = tar.Id,
                            MainImage = tar.Content.MainImage,
                            Title = tar.Content.Title
                        });
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return result;
        }

        public async Task<List<TaRHomeOuputDto>> GetHomeContent(int page)
        {
            //Include Join yapar
            List<TaRHomeOuputDto> result = new List<TaRHomeOuputDto>();
            try
            {
                var query = await _tarRepository.GetQueryable()
                .Include(x => x.Content)
                .Include(x => x.Popularities)
                .Include(x => x.User)
                .Include(x => x.TaRCategories)
                .Include("TaRCategories.Category")
                //.Where(x=>x.isDeleted != true && x.isActive== true)
                //.OrderBy(y => y.Id)
                OrderByDescending(y=>y.CreationTime)
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
                    Puan = y.Popularities.Count > 0 ? (y.Popularities.Sum(x => x.Puan) / y.Popularities.Count()):0,
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
    }
}
