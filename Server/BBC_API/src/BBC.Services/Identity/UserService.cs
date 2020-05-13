using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.Base;
using BBC.Services.Services.Common.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity
{
    public class UserService : ApplicationBaseServices<User, Role>, IUserService
    {
        private readonly IRepositoryBase<BBCContext, TarifAndRecete, int> _tarRepository;
        private readonly IRepositoryBase<BBCContext, Popularity, int> _popularityRepository;

        public UserService(
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository,
            IRepositoryBase<BBCContext, Popularity, int> popularityRepository
            )
        {
            _tarRepository = tarRepository;
            _popularityRepository = popularityRepository;
        }

        public UserService()
        {

        }
        public async Task<List<UserListDto>> GetUsers()
        {
            IQueryable<User> roles = await Task.Run(() =>
            {
                return _userManager.Users;
            });
            var result = _mapper.Map<List<UserListDto>>(roles);
            return result;
        }

        public async Task<UserProfileDto> GetUser(int Id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if (user == null)
                return null;

            var result = _mapper.Map<UserProfileDto>(user);
            return result;
        }

        public async Task<IdentityResult> EditUser(string Id, EditUserDto model)
        {
            User user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="User",
                        Description = "Not Found User"
                    }
                });
            }
            user = _mapper.Map<User>(model);

            IdentityResult result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> Delete(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="Role",
                        Description = "Not Found Role"
                    }
                });
            }
            IdentityResult result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<UserReportHeaderWidget> HeaderReport(int userId)
        {
            var totalComment = _popularityRepository.GetQueryable().Count(x => x.UserId == userId);

            //Userin makalelerini  ve populartieisini aldık
            //makalenin populartiesinin countlarını topladık
            var totalReceivedComment = _tarRepository.GetQueryable().Include(x => x.Popularities)
                .Where(x => x.UserId == userId).SelectMany(x => x.Popularities).Count();

            var totalTAR = _tarRepository.GetQueryable().Where(x => x.UserId == userId).Count();

            return new UserReportHeaderWidget()
            {
                TotalComment = totalComment,
                TotalRecivedComment = totalReceivedComment,
                TotalTaR = totalTAR
            };
        }

        public async Task<List<UserReportMonthlyTAR>> MonthlyTaR(int userId)
        {
            var query =await _tarRepository.GetQueryable().Where(x => x.UserId == userId && x.CreateTime.Year == DateTime.Now.Year)
                .GroupBy(x => x.CreateTime.Month)
                .Select(x => new UserReportMonthlyTAR()
                {
                    Month= x.Key,
                    TotalTaR = x.Count()
                }).ToListAsync();

            return query;
        }
    }
}
