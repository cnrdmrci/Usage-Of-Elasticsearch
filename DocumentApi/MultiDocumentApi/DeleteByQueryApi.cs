using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi
{
    public class DeleteByQueryApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<bool> DeleteByQueryAsync<T>(string indexName,string userName) where T : class
        {
            var response = await elasticClient.DeleteByQueryAsync<T>(d => d
                .Index(indexName)
                .Query(q => q
                    .Term("userName",userName)));

            return response.IsValid;
        }
    }
}
