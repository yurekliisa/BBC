using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Infrastructure.Data;
using BBC.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("Get")]
        public IEnumerable<string> Get()
        {
            //context.Tt();
            return new string[] { "value1", "value2" };
        }

        // GET: api/student/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/student
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/student/5
        public void Delete(int id)
        {
        }
    }
}