using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.TermLevelQueries
{
    public class TermsQuery
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchFirstNamesWithTermsQuery<T>(string indexName, string[] firstNames) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Terms(m => m
                        .Field("firstName")
                        .Terms(firstNames)
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchFistNamesWithTermsQuery(string indexName, string[] firstNames)
        {
            var response = await elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Terms(t => t
                        .Field(f => f.FirstName)
                        .Terms(firstNames)))
            );

            return response.Documents.ToList();
        }
    }
}
