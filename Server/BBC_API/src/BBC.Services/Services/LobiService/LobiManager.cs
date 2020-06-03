using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Base;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.LobiService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.LobiService
{
    public class LobiManager : ApplicationBaseServices<User, Role>, ILobiService
    {
        private readonly IRepositoryBase<BBCContext, Lobi, int> _lobiRepository;
        private readonly IRepositoryBase<BBCContext, LobiMessages, int> _lobiMessagesRepository;

        public LobiManager(IRepositoryBase<BBCContext, Lobi, int> lobiRepository, IRepositoryBase<BBCContext, LobiMessages, int> lobiMessagesRepository)
        {
            _lobiRepository = lobiRepository;
            _lobiMessagesRepository = lobiMessagesRepository;
        }

        public async Task CreateLobi(CreateLobiDto input)
        {
            var lobi = _mapper.Map<Lobi>(input);
            await _lobiRepository.InsertAsync(lobi);
        }

        public async Task DeleteLobi(int Id)
        {
            await _lobiRepository.DeleteAsync(Id);
        }

        public async Task EditLobi(EditLobiDto input)
        {
            var lobi = await _lobiRepository.FindAsync(input.Id);
            _mapper.Map(lobi, input);
            await _lobiRepository.UpdateAsync(lobi);
        }

        public async Task<List<LobiListDto>> GetAllLobies()
        {
            var lobis = await _lobiRepository.GetListAsync();
            var result = _mapper.Map<List<LobiListDto>>(lobis);
            return result;
        }



        public async Task<EditLobiDto> GetLobi(int Id)
        {
            var lobi = await _lobiRepository.GetAsync(Id);
            var result = _mapper.Map<EditLobiDto>(lobi);
            return result;
        }

        public async Task<List<LobiMessagesDto>> GetAllLobiMessages(int Id)
        {
            var currentUser = await this.GetCurrentUserAsync();
            var result = await _lobiMessagesRepository.GetQueryable()
                .Include(x => x.FromUser)
                .Where(x => x.ToLobiId == Id)
                .Select(x => new LobiMessagesDto()
                {
                    senderUserId = x.FromUserId,
                    senderUserName = x.FromUser.UserName,
                    Message = x.Message,
                    senderTime = x.SendTime,
                    isOwner = x.FromUserId == currentUser.Id ? true : false
                }).ToListAsync();

            return result;
        }

        public async Task JoinUserToLobi(LobiInputDto input)
        {
            var lobi = await _lobiRepository.GetAsync(input.LobiId);
            lobi.LobiUsers.Add(new LobiUser()
            {
                UserId = input.UserId
            });
            await _lobiRepository.UpdateAsync(lobi);
        }

        public async Task LeaveUserToLobi(LobiInputDto input)
        {
            var lobi = await _lobiRepository.GetAsync(input.LobiId);
            lobi.LobiUsers.Remove(new LobiUser()
            {
                LobiId = input.LobiId,
                UserId = input.UserId
            });
            await _lobiRepository.UpdateAsync(lobi);
        }

        public async Task SendUserMessageToLobi(int lobiId, LobiMessagesDto input)
        {
            await _lobiMessagesRepository.InsertAsync(new LobiMessages()
            {
                FromUserId = input.senderUserId,
                Message = input.Message,
                SendTime = input.senderTime,
                ToLobiId = lobiId
            });
        }
    }
}
