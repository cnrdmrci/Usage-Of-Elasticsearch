using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.Aggregations
{
    public class Aggregation
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<double> GetMinValueAsync<T>(string indexName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q=>q
                    .MatchAll())
                .Size(0)
                .Aggregations(ag => ag
                    .Min("age",s => s.Field("age")))
            );

            return response.Aggregations.Min("age")?.Value.Value ?? -1;
        }
        
        public async Task<double> GetMaxValueAsync<T>(string indexName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q=>q
                    .MatchAll())
                .Size(0)
                .Aggregations(ag => ag
                    .Max("age",s => s.Field("age")))
            );

            return response.Aggregations.Min("age")?.Value.Value ?? -1;
        }

        public async Task<double> GetSumValueAsync<T>(string indexName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchAll())
                .Size(0)
                .Aggregations(ag => ag
                    .Sum("age", s => s.Field("age")))
            );

            return response.Aggregations.Min("age")?.Value.Value ?? -1;
        }

        public async Task<double> GetAverageValueAsync<T>(string indexName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchAll())
                .Size(0)
                .Aggregations(ag => ag
                    .Average("age", s => s.Field("age")))
            );

            return response.Aggregations.Min("age")?.Value.Value ?? -1;
        }

        public async Task<StatsAggregate> GetStatsValueAsync<T>(string indexName) where T : class
        {
            var response = await elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchAll())
                .Size(0)
                .Aggregations(ag => ag
                    .Stats("age", s => s.Field("age")))
            );

            return response.Aggregations.Stats("age");
        }
    }
}
