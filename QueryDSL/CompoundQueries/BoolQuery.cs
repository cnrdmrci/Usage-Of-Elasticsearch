using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.CompoundQueries
{
    public class BoolQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchBoolQuery<T>(string indexName) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Range(r => r
                                .Field("age")
                                .GreaterThan(20)
                                .Relation(RangeRelation.Within)
                            )
                        ).MustNot(mn => mn
                            .Match(mt => mt
                                .Field("userName")
                                .Query("caner")
                            )
                        ).Should(sh => sh
                            .MultiMatch(mm => mm
                                .Query("demirci")
                                .Fields(fds => fds
                                    .Field("FirstName")
                                    .Field("lastName")
                                )
                            )
                        ).Filter(flt => flt
                            .Term(tr => tr
                                .Field("gender")
                                .Value("M")))
                    )
                )
            );
            return response.Documents.ToList();
        }
    }
}