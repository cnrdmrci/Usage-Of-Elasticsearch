﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.QueryDSL.TermLevelQueries
{
    public class WilcardQuery
    {
        private readonly ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> SearchUserNameWithWildcardQuery<T>(string indexName, string wildcard) where T : class
        {
            wildcard = "bro*n";
            wildcard = "bro?n";
            var response = await _elasticClient.SearchAsync<T>(p => p
                .Index(indexName)
                .Query(q => q
                    .Wildcard(w => w
                        .Field("userName")
                        .Value(wildcard))
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<User>> SearchLastNameWithPrefixQuery(string indexName, string wildcard)
        {
            var response = await _elasticClient.SearchAsync<User>(p => p
                .Index(indexName)
                .Query(q => q
                    .Wildcard(w => w
                        .Field(f => f.UserName)
                        .Value(wildcard))
                )
            );
            return response.Documents.ToList();
        }
    }
}
