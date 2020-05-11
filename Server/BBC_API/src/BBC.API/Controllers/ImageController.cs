using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ImageController : ControllerBase
    //{
    //    private readonly IHostingEnvironment _hostingEnvironment;
    //    public ImageController(
    //        IHostingEnvironment hostingEnvironment
    //        )
    //    {
    //        _hostingEnvironment = hostingEnvironment;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetImageByPath(string path)
    //    {
    //        var image = System.IO.File.OpenRead(_hostingEnvironment.ContentRootPath+path);
    //        return File(image, "image/jpeg");
    //    }
    //}
}