using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi
{
    public class UpdateByQueryApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<bool> UpdateByQueryWithUserNameAsync<T>(string indexName) where T : class
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("LastName", "LastName updated by query >T");

            var response = await elasticClient.UpdateByQueryAsync<T>(u => u
                .Index(indexName)
                .Query(q => q
                    .Term("userName","user5"))
                .Script(s => s
                    .Source("ctx._source.LastName = params.LastName")
                    .Params(parameters))
                .Refresh()
            );

            return response.IsValid;
        }

        public async Task<bool> UpdateByQueryAsync(string indexName)
        {
            var parameters = new Dictionary<string, object> ();
            parameters.Add("LastName","LastName updated by query1");

            var response = await elasticClient.UpdateByQueryAsync<User>(u => u
                .Index(indexName)
                .Query(q => q
                    .Term(t=>t.UserName, "user6"))
                .Script(s=>s
                    .Source("ctx._source.LastName = params.LastName")
                    .Params(p => p
                        .Add("LastName", "LastName updated by query2")))
                .Refresh()
            );

            return response.IsValid;
        }
    }
}
