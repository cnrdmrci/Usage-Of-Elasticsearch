using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Usage_Of_Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private static readonly ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();


    }
}