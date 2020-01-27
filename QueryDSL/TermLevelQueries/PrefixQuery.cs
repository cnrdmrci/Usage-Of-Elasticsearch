using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.TermLevelQueries
{
    public class PrefixQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchLastNameWithPrefixQuery<T>(string indexName, string prefix) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Prefix(pr => pr
                        .Field("lastName")
                        .Value(prefix))
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchLastNameWithPrefixQuery(string indexName, string prefix)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Prefix(pr => pr
                        .Field(f => f.LastName)
                        .Value(prefix))
                )
            );
            return response.Documents.ToList();
        }
    }
}
