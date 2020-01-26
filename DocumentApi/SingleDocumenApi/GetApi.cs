using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi
{
    public class GetApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<T> GetDataByIdAsync<T>(string indexName,int id) where T : class
        {
            var response = await elasticClient.GetAsync(new DocumentPath<T>(id), g => g.Index(indexName));
            return response.Source;
        }
        public async Task<User> GetUserDataByUserNameAsync(string indexName, string userName)
        {
            var response = await elasticClient.SearchAsync<User>(p=> p
                .Index(indexName)
                .Query(q=> q
                    .Match(m => m
                        .Field(f=> f.UserName)
                        .Query(userName)
                        )
                )
            );
            var documents = response.Documents;
            return documents.FirstOrDefault();
        }
    }
}
