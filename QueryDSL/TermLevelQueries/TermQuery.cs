using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.TermLevelQueries
{
    public class TermQuery
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<T> SearchUserNameWithTermQuery<T>(string indexName,string userName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Term(m => m
                        .Field("userName")
                        .Value(userName)
                    )
                )
            );
            return response.Documents.FirstOrDefault();
        }

        public async Task<User> SearchUserNameWithTermQuery(string indexName,string userName)
        {
            var response = await elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Term(t => t
                        .Field(f => f.UserName)
                        .Value(userName)))
            );

            return response.Documents.FirstOrDefault();
        }
    }
}
