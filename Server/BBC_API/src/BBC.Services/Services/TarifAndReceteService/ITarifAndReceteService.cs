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
<<<<<<< Updated upstream
        Task<List<TaRHomeOuputDto>> GetTarifAndRecetes(int page);
        Task<List<TarifAndReceteDetailDto>> GetAllTarifAndReceteDetails();
        Task<List<UserTarifAndReceteDto>> GetTarifAndReceteByUserId(int Id);
        Task<int> CreateTaR(CreateTarifAndReceteDto input);
        Task EditTarifAndRecete(UserTarifAndReceteDto input);
=======
        Task<int> CreateTaR(CreateTarifAndReceteDto input);
        Task<List<TarifAndReceteListDto>> GetAllTarifAndRecetes();
        Task<TarifAndReceteDetailDto> GetTarifAndReceteDetails(int tarId);
        Task<List<EditTarifAndReceteDto>> GetTarifAndRecete(int Id);
        Task EditTarifAndRecete(EditTarifAndReceteDto input);
>>>>>>> Stashed changes
        Task DeleteTarifAndRecete(int Id);
    }
}
