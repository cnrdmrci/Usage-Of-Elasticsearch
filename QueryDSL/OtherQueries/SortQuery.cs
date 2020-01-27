using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.OtherQueries
{
    public class SortQuery
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<User> SearchFromSizeQuery(string indexName, string userName)
        {
            var response = await elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchAll()
                )
                .Sort(s => s
                    .Descending(d=>d.UserName)
                )
            );
            return response.Documents.FirstOrDefault();
        }
    }
}
