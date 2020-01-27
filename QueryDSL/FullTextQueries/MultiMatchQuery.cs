using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.FullTextQueries
{
    public class MultiMatchQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchFirstAndLastNameMultiMatchQuery<T>(string indexName, string key) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(key)
                        .Fields(f => f
                            .Field("firstName")
                            .Field("lastName")
                        )
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchFirstAndLastNameMultiMatchQuery(string indexName, string key)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(key)
                        .Fields(f => f
                            .Field(fl => fl.FirstName)
                            .Field(fl => fl.LastName)
                        )
                    )
                )
            );
            return response.Documents.ToList();
        }
    }
}
