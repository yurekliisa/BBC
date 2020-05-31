using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.AdminService.Dto;
using BBC.Services.Services.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.AdminService
{
    public class AdminReportManager : BaseService, IAdminReportService
    {

        private readonly IRepositoryBase<BBCContext, Category, int> _categoryRepository;
        private readonly IRepositoryBase<BBCContext, TarifAndRecete, int> _tarRepository;
        private readonly IRepositoryBase<BBCContext, User, int> _userRepository;
        private readonly IRepositoryBase<BBCContext, Popularity, int> _commentRepository;
        private readonly IRepositoryBase<BBCContext, Lobi, int> _lobyRepository;
        public AdminReportManager
            (IRepositoryBase<BBCContext, Category, int> categoryRepository,
            IRepositoryBase<BBCContext, TarifAndRecete, int> tarRepository,
            IRepositoryBase<BBCContext, User, int> userRepository,
            IRepositoryBase<BBCContext, Popularity, int> commentRepository,
            IRepositoryBase<BBCContext, Lobi, int> lobyRepository) 
        {
            _categoryRepository = categoryRepository;
            _tarRepository = tarRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _lobyRepository = lobyRepository;
        }
        public async Task<AdminReportWidget> Report()
        {
            var result = new AdminReportWidget();
            result.TotalTaR = await _tarRepository.GetCountAsync();
            result.TotalUser = await _userRepository.GetCountAsync();
            result.TotalComment = await _commentRepository.GetCountAsync();
            result.TotalCategory = await _categoryRepository.GetCountAsync();
            result.TotalLoby = await _lobyRepository.GetCountAsync();
            return result;
        }

    }
}
