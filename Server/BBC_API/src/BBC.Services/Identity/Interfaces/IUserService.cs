﻿using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Interfaces
{
    public interface IUserService : IApplicationBaseServices<User, Role>
    {
        Task<List<UserListDto>> GetUsers();
        Task<UserProfileDto> GetUser(int Id);
        Task<IdentityResult> EditUser(int Id, EditUserDto model);
        Task<IdentityResult> Delete(int Id);
        Task<UserReportHeaderWidget> HeaderReport(int userId);
        Task<List<UserReportMonthlyTAR>> MonthlyTaR(int userId);
    }
}
