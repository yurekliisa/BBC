using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.LobiService;
using BBC.Services.Services.LobiService.Dto;
using Microsoft.AspNetCore.SignalR;

namespace BBC.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILobiService _lobiService;
        public ChatHub(ILobiService lobiService)
        {
            _lobiService = lobiService;
        }

        public async Task SendMessage(LobiMessagesDto input)
        {
            input.senderTime = DateTime.Now;

            await Clients.Group(input.lobiId.ToString()).SendAsync("ReceiveMessage", input);

            await _lobiService.SendUserMessageToLobi(input.lobiId, input);

        }

        public async Task JoinGroup(int lobiId, int userId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, lobiId.ToString());
            await _lobiService.JoinUserToLobi(new LobiInputDto()
            {
                LobiId = lobiId,
                UserId = userId
            });
            await Clients.Group(lobiId.ToString()).SendAsync("NewUser", lobiId);
        }

        public async Task LeaveGroup(int lobiId, int userId)
        {
            await _lobiService.LeaveUserToLobi(new LobiInputDto()
            {
                LobiId = lobiId,
                UserId = userId
            });
            await Clients.Group(lobiId.ToString()).SendAsync("NewUser", lobiId);
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, lobiId.ToString());
        }
    }
}
