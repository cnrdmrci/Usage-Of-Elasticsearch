using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi
{
    public class DeleteApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();
        public async Task<bool> DeleteDataByIdAsync<T>(string indexName, int id) where T : class
        {
            var response = await elasticClient.DeleteAsync(new DocumentPath<T>(id), g=> g.Index(indexName));
            return response.IsValid;
        }
    }
}
