using BBC.API.Helper.Attribute;
using BBC.Core.Configuration;
using BBC.Core.Interfaces;
using BBC.Core.Permission;
using BBC.Services.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BBC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    
    public class ValuesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ConfigJWT _configJWT;
        private readonly ICacheManager _cacheManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ValuesController(
            ICategoryService service, 
            ConfigJWT configJWT, 
            ICacheManager cacheManager,
             IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _cacheManager = cacheManager;
            _configJWT = configJWT;
            _service = service;
        }
        [HttpGet]
        //[RequiredPermission(Permissions.Category)]
        public ActionResult<IEnumerable<string>> GetAllData()
        {
            //throw new System.Exception("qweqwe");
            var bbt= this.HttpContext.User.Claims;
            var cqwe = _contextAccessor.HttpContext.User.Claims;
            var bq = _cacheManager.GetAllCache();
            var b = _configJWT.Audience;
           // _service.Exception();
            _service.InsertData();
            _service.GetData();

            return Ok(new string[] { "value1", "value2" });
        }
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}