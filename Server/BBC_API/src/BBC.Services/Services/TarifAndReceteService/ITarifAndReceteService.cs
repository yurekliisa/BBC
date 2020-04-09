using BBC.Core.Domain.Identity;
using BBC.Services.Services.Base;
using BBC.Services.Services.TarifAndReceteService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.TarifAndReceteService
{
    public interface ITarifAndReceteService: IApplicationBaseServices<User, Role>
    {
        Task<int> CreateTaR(CreateTarifAndReceteDto input);
    }
}
