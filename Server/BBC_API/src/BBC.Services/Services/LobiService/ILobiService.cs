using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.LobiService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.LobiService
{
    public interface ILobiService : IBaseService
    {
        Task<List<LobiListDto>> GetAllLobies();
        Task<EditLobiDto> GetLobi(int Id);
        Task CreateLobi(CreateLobiDto input);
        Task EditLobi(EditLobiDto input);
        Task DeleteLobi(int Id);

        Task<List<LobiMessagesDto>> GetAllLobiMessages(int Id);
        Task<List<UserListDto>> GetLobiUsers(int lobiId);
        Task JoinUserToLobi(LobiInputDto input);
        Task LeaveUserToLobi(LobiInputDto input);

        Task SendUserMessageToLobi(int lobiId, LobiMessagesDto input);
    }
}
