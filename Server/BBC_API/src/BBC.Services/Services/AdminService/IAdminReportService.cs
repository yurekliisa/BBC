using BBC.Core.Dependency;
using BBC.Services.Services.AdminService.Dto;
using BBC.Services.Services.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.AdminService
{
    public interface IAdminReportService: IBaseService, ITransientDI
    {
        Task<AdminReportWidget> Report();
    }
}
