using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.HomeService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                var popularties = await GetPopularity(x=>x.TaRId).Take(12).ToListAsync();
                foreach (var popularty in popularties)
                {
                    var tar = await _tarRepository.GetQueryable().Include(y => y.Content).FirstOrDefaultAsync(x => x.Id == popularty.TaRID);
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

        public async Task<List<PopularTaROutputDto>> GetTaRByPopularCategory()
        {
            var popularties = await GetPopularity(x=>x.TaRId).ToListAsync();
            var result = await GetRandomPopularTaR(popularties);
            return result;
        }

        public async Task<List<PopularTaROutputDto>> GetTaRByForYou()
        {
            var popularties = await GetPopularity(x => x.UserId).ToListAsync();
            var result = await GetRandomPopularTaR(popularties);
            return result;
        }

        private IQueryable<PopularityDto> GetPopularity(Expression<Func<Popularity,int>> predicate)
        {
            var result = _popularityRepository.GetQueryable()
                .Where(y => y.IsDeleted == false)
                .GroupBy(predicate).Select(y => new PopularityDto()
                {
                    TotalPuan = y.Sum(x => x.Puan) / y.Count(),
                    TaRID = y.Key,
                }).OrderByDescending(y => y.TotalPuan);

            return result;
        }

        private async Task<List<PopularTaROutputDto>> GetRandomPopularTaR(List<PopularityDto> popularties)
        {
            int total = 10;
            var result = new List<PopularTaROutputDto>();

            //Populerlerin categorilerini alıp distinct ettik
            var categortyIds = new List<int>();
            foreach (var popularty in popularties)
            {
                categortyIds.AddRange(await _tarRepository.GetQueryable()
                    .Include(x => x.TaRCategories)
                    .SelectMany(y => y.TaRCategories.Select(y => y.CategoryId)).ToListAsync());
            }

            categortyIds = categortyIds.Distinct().ToList();

            if (categortyIds.Count > 10)
                categortyIds = categortyIds.Take(10).ToList();

            /*
             * Test Case
            10/9 = 1 taneden 9 tane resim ve 1 resim eksik
            10/8 = 1 taneden 8 tane resim ve 2 resim eksik
            10/7 = 1 taneden 7 tane resim ve 3 resim eksik
            10/6 = 1 taneden 6 tane resim ve 4 resim eksik
            10/4 = 2 taneden 8 tane resim ve 2 resim eksik
            10/3 = 3 taneden 9 tane resim ve 1 resim eksik
            */
            int take = total / categortyIds.Count;
            int maxTar = total - take * categortyIds.Count;
            foreach (var categoryId in categortyIds)
            {
                int oneTake = 0;
                if (maxTar != 0)
                {
                    oneTake = 1;
                    maxTar--;
                }
                var totalTake = take + oneTake;

                var tar = await _tarRepository.GetQueryable()
                    .Include(x => x.TaRCategories)
                    .Include(x => x.Content)
                    .Where(y => y.TaRCategories.Select(x => x.CategoryId).Contains(categoryId))
                    .OrderBy(x => Guid.NewGuid())
                    .Take(totalTake)
                    .Select(x => new PopularTaROutputDto()
                    {
                        Id = x.Id,
                        Title = x.Content.Title,
                        MainImage = x.Content.MainImage
                    }).ToListAsync();
                result.AddRange(tar);
            }
            result = result.Distinct().ToList();
            return result;
        }
    }
}
