using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usage_Of_Elasticsearch.Aggregations;

namespace Usage_Of_Elasticsearch.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AggregationController : ControllerBase
    {
        [HttpGet("GetMinAge")]
        public async Task<IActionResult> GetMinAge()
        {
            Aggregation aggregation = new Aggregation();
            var response = await aggregation.GetMinValueAsync<User>(ConstantStrings.IndexName);

            return Ok(response);
        }

        [HttpGet("GetMaxAge")]
        public async Task<IActionResult> GetMaxAge()
        {
            Aggregation aggregation = new Aggregation();
            var response = await aggregation.GetMaxValueAsync<User>(ConstantStrings.IndexName);

            return Ok(response);
        }

        [HttpGet("GetSumAge")]
        public async Task<IActionResult> GetSumAge()
        {
            Aggregation aggregation = new Aggregation();
            var response = await aggregation.GetSumValueAsync<User>(ConstantStrings.IndexName);

            return Ok(response);
        }

        [HttpGet("GetAverageAge")]
        public async Task<IActionResult> GetAverageAge()
        {
            Aggregation aggregation = new Aggregation();
            var response = await aggregation.GetAverageValueAsync<User>(ConstantStrings.IndexName);

            return Ok(response);
        }

        [HttpGet("GetStatsAge")]
        public async Task<IActionResult> GetStatsAge()
        {
            Aggregation aggregation = new Aggregation();
            var response = await aggregation.GetStatsValueAsync<User>(ConstantStrings.IndexName);

            return Ok(response);
        }
    }
}