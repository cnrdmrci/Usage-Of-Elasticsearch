using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.FullTextQueries
{
    public class QueryStringQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchFirstNameQueryStringQuery<T>(string indexName, string firstName) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .QueryString(qs => qs
                        .DefaultField("firstName")
                        .Query("how are you AND (Caner OR Tamer)")
                        .DefaultOperator(Operator.And)
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchFirstNameQueryStringQuery(string indexName, string firstName)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .QueryString(qs => qs
                        .DefaultField(df => df.FirstName)
                        .Query("\"how are you\" AND (Caner OR Tamer)"))
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchFirstAndLastNameQueryStringQuery(string indexName, string firstName)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .QueryString(qs => qs
                        .Fields(fds => fds
                            .Field(fd => fd.UserName)
                            .Field(fd => fd.LastName))
                        .Query("\"how are you\" AND (Caner OR Tamer)"))
                )
            );
            return response.Documents.ToList();
        }
    }
}
