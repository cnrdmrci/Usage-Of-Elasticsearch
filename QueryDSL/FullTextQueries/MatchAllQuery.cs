using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.FullTextQueries
{
    public class MatchAllQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchMatchAllQuery<T>(string indexName) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .MatchAll()
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchMatchAllQuery(string indexName)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .MatchAll()
                )
            );
            return response.Documents.ToList();
        }
    }
}
