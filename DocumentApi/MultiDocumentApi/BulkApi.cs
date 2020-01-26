using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi
{
    public class BulkApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<bool> AddBulkData<T>(string indexName,T[] datas) where T : class
        {
            var response = await elasticClient.BulkAsync(b => b.Index(indexName).IndexMany(datas));
            return response.IsValid;
        }
    }
}
