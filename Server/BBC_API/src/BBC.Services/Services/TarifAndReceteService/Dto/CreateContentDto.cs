using Microsoft.AspNetCore.Http;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CreateContentDto
    {
        public string Title { get; set; }
        public string ContentText { get; set; }
        public string ShortDescription { get; set; }

        public IFormFile MainImage { get; set; }
    }
}
