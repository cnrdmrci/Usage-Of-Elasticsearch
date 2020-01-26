using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi
{
    public class UpdateApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<bool> UpdateDataByIdAsync<T>(string indexName, T doc,int id) where T : class
        {
            var response = await elasticClient.UpdateAsync<T>(new DocumentPath<T>(id), s=> s.Index(indexName).Doc(doc));
            return response.IsValid;
        }
    }
}
