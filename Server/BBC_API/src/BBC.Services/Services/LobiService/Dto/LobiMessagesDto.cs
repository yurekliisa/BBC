using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.LobiService.Dto
{
    public class LobiMessagesDto
    {
        public int lobiId { get; set; }
        public int senderUserId { get; set; }
        public string senderUserName { get; set; }
        public string Message { get; set; }
        public bool isOwner { get; set; }
        public DateTime senderTime { get; set; }
    }
}
