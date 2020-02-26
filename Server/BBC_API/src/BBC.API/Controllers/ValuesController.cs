using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Core.Configuration;
using BBC.Core.Interfaces;
using BBC.Infrastructure.Data;
using BBC.Services.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ConfigJWT _configJWT;
        private readonly ICacheManager _cacheManager;
        public ValuesController(ICategoryService service, ConfigJWT configJWT, ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _configJWT = configJWT;
            _service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllData()
        {
            var bq = _cacheManager.GetAllCache();
            var b = _configJWT.Audience;
            _service.InsertData();
            _service.GetData();

            return Ok(new string[] { "value1", "value2" });
        }
        [HttpGet("{id}")]
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