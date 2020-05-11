using BBC.Core.Domain.Identity;
using BBC.Services.Services.Base;
using BBC.Services.Services.HomeService.Dto;
using BBC.Services.Services.TarifAndReceteService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.TarifAndReceteService
{
    public interface ITarifAndReceteService: IApplicationBaseServices<User, Role>
    {
        Task<List<TaRHomeOuputDto>> GetTarifAndRecetes(int page);
        Task<List<TarifAndReceteDetailDto>> GetAllTarifAndReceteDetails();
        Task<List<EditTarifAndReceteDto>> GetTarifAndReceteByUserId(int Id);
        Task<int> CreateTaR(CreateTarifAndReceteDto input);
        Task EditTarifAndRecete(EditTarifAndReceteDto input);
        Task DeleteTarifAndRecete(int Id);
    }
}
