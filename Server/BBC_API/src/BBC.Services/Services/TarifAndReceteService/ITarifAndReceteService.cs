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
        Task<List<UserTarifAndReceteDto>> GetTarifAndReceteByUserId(int Id);
        Task<int> CreateTaR(CreateTarifAndReceteDto input);
        Task EditTarifAndRecete(UserTarifAndReceteDto input);
        Task<UserTarifAndReceteDto> GetTarifAndReceteForEdit(int tarId);
        Task<List<TarifAndReceteListDto>> GetAllTarifAndRecetes(int page);
        Task<TarifAndReceteDetailDto> GetTarifAndReceteDetails(int tarId);
        Task DeleteTarifAndRecete(int Id);
        Task Comment(CommentDto input);
        Task<List<CommentDto>> GetAllComments(int tarId);
    }
}
