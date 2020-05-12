using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CommentDto
    {
        public int TaRId { get; set; }
        public string Comment { get; set; }
        public double Puan{ get; set; }
        public int UserId { get; set; }
        public string UserPhoto { get; set; }
        public string UserName { get; set; }
        public string CommentDate { get; set; }

    }
}
