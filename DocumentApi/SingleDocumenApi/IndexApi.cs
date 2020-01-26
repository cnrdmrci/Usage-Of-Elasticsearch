using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi
{
    public class IndexApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();
        public async Task<bool> CreateIndexAsync<T>(int numberOfShards, int numberOfReplicas, string indexName, string aliasName = null) where T : class
        {
            if (!checkIndexExists(indexName))
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = numberOfReplicas;
                indexSettings.NumberOfShards = numberOfShards;

                var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                    .Map<T>(m => m.AutoMap())
                    .InitializeUsing(new IndexState() { Settings = indexSettings });

                if(!String.IsNullOrWhiteSpace(aliasName))
                    createIndexDescriptor.Aliases(a => a.Alias(aliasName));

                CreateIndexResponse response = await elasticClient.Indices.CreateAsync(createIndexDescriptor);
                return response.Acknowledged;
            }

            return true;
        }

        public async Task<bool> CreateIndexAsync(int numberOfShards, int numberOfReplicas, string indexName, string aliasName = null)
        {
            if (!checkIndexExists(indexName))
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = numberOfReplicas;
                indexSettings.NumberOfShards = numberOfShards;

                var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                    .InitializeUsing(new IndexState() { Settings = indexSettings });

                if (!String.IsNullOrWhiteSpace(aliasName))
                    createIndexDescriptor.Aliases(a => a.Alias(aliasName));

                CreateIndexResponse response = await elasticClient.Indices.CreateAsync(createIndexDescriptor);
                return response.Acknowledged;
            }

            return true;
        }

        private bool checkIndexExists(string indexName)
        {
            return elasticClient.Indices.Exists(indexName).Exists;
        }

        public async Task<bool> AddDataAsync<T>(string indexName,T data) where T : class
        {
            IndexResponse indexResponse = await elasticClient.IndexAsync<T>(data, idx => idx.Index(indexName));
            return indexResponse.IsValid;
        }
    }
}
