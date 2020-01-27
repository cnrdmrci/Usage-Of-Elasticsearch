using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.TermLevelQueries
{
    public class RangeQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchAnyAgeWithRangeQuery<T>(string indexName, int age) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Range(r => r
                        .Field("age")
                        .GreaterThan(age -1)
                        .GreaterThanOrEquals(age)
                        .LessThan(age +1)
                        .LessThanOrEquals(age)
                        .Relation(RangeRelation.Within)
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<T> SearchAnyDateWithRangeQuery<T>(string indexName, DateTime insertDate) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .DateRange(r => r
                        .Field("insertDate")
                        .GreaterThan(insertDate)
                        .GreaterThanOrEquals(insertDate)
                        .LessThan("01/01/2020")
                        .LessThanOrEquals(DateMath.Now)
                        .Format("dd/MM/yyyy||yyyy")
                        .TimeZone("+01:00"))
                    )
                );
            return response.Documents.FirstOrDefault();
        }

        public async Task<User> SearchAnyAgeWithRangeQuery(string indexName, int age)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Range(r => r
                        .Field(f => f.Age)
                        .GreaterThan(age - 1)
                        .GreaterThanOrEquals(age)
                        .LessThan(age + 1)
                        .LessThanOrEquals(age)
                        .Relation(RangeRelation.Within)
                    )
                )
            );

            return response.Documents.FirstOrDefault();
        }
    }
}
