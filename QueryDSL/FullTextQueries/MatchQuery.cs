﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.FullTextQueries
{
    public class MatchQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchFirstNameMatchQuery<T>(string indexName, string firstName) where T : class
        {
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field("firstName")
                        .Query(firstName))
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchFirstNameMatchQuery(string indexName, string firstName)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FirstName)
                        .Query(firstName))
                )
            );
            return response.Documents.ToList();
        }
    }
}
