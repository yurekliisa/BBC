using System.Collections.Generic;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class EditTarifAndReceteInputDto
    {
        public int Id { get; set; }
        public List<int> Categories { get; set; }

        public EditTarifAndReceteInputDto()
        {
            Categories = new List<int>();
        }

        public CreateContentDto Content { get; set; }
    }
}
