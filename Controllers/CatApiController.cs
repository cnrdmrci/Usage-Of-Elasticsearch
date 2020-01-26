using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usage_Of_Elasticsearch.CatApi;

namespace Usage_Of_Elasticsearch.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatApiController : ControllerBase
    {
        [HttpGet("GetllAllIndexName")]
        public async Task<IActionResult> GetllAllIndexName()
        {
            Indices indices = new Indices();
            var response = await indices.GetAllIndexName();
            
            return Ok(response);
        }
    }
}