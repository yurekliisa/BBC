using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CreateTarifAndReceteDto
    {
        public DateTime CreationTime { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int PopularityId { get; set; }
    }
}
