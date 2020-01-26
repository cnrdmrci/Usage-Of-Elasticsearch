using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.CatApi
{
    public class Indices
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<string>> GetAllIndexName()
        {
            var response = await elasticClient.Cat.IndicesAsync();

            var indexList = response.Records.Select(x => x.Index).ToList();
            return indexList;
        }
    }
}
